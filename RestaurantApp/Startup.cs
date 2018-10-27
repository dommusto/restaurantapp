using System;
using System.Collections.Generic;
using Amazon;
using Amazon.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Brighter.MessagingGateway.AWSSQS;
using Paramore.Darker.AspNetCore;
using Paramore.Brighter.ServiceActivator;
using RestaurantApp.Core;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;
using RestaurantApp.Core.Mappers;
using RestaurantApp.Core.QueryHandlers;
using RestaurantApp.EventHandlers;
using RestaurantApp.Hubs;

namespace RestaurantApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSignalR();

            services.AddSingleton<IOrdersRepository>(new OrdersRepository());
            services.AddSingleton<IMenuItemsRepository, MenuItemsRepository>();
            var actualContainer = services.BuildServiceProvider();
            var amAMessageMapperFactory = new SimpleMessageMapperFactory(actualContainer);
            var messageMapperRegistry = new MessageMapperRegistry(amAMessageMapperFactory)
            {
                {typeof(PrepareOrderCommand), typeof(PrepareOrderCommandMessageMapper)},
                {typeof(OrderPreparedEvent), typeof(OrderPreparentEventMapper)},
                {typeof(OrderPickedUpByCookerEvent), typeof(OrderPickedUpByCookerEventMapper)},
            };
            var basicAwsCredentials = new BasicAWSCredentials("AKIAILSOA4BG5TVAGMVQ", "Wch10UHsC+PaONVh1oj5UH9mBh/em8g0zajBO6ql");
            var messagingConfiguration = new MessagingConfiguration(new InMemoryMessageStore(), new SqsMessageProducer(basicAwsCredentials, RegionEndpoint.USWest2), messageMapperRegistry);
            services.AddBrighter(opts =>
            {

                opts.HandlerLifetime = ServiceLifetime.Singleton;
                opts.MessagingConfiguration = messagingConfiguration;
            }).HandlersFromAssemblies(typeof(PrepareOrderCommand).Assembly).HandlersFromAssemblies(typeof(OrderPreparedEventHandler).Assembly);
            services.AddDarker().AddHandlersFromAssemblies(typeof(GetMenuItemsQueryHandler).Assembly);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var container = services.BuildServiceProvider();
            ConfigureDispatcher((IAmACommandProcessor)container.GetService(typeof(IAmACommandProcessor)), messageMapperRegistry, basicAwsCredentials);

        }

        private void ConfigureDispatcher(IAmACommandProcessor commandProcessor, MessageMapperRegistry messageMapperRegistry, BasicAWSCredentials basicAwsCredentials)
        {
            var dispatcher = DispatchBuilder.With()
                .CommandProcessor(commandProcessor)
                .MessageMappers(messageMapperRegistry)
                .DefaultChannelFactory(new InputChannelFactory(new SqsMessageConsumerFactory(basicAwsCredentials, RegionEndpoint.USWest2)))
                .Connections(new List<Connection>()
                {
                    new Connection<OrderPickedUpByCookerEvent>(
                        new ConnectionName("OrderPickedUpByCookerEvent"),
                        new ChannelName("https://sqs.us-west-2.amazonaws.com/058052576087/OrderPickedUpByCookerEvent"),
                        new RoutingKey("OrderPickedUpByCookerEvent")
                    ),
                    new Connection<OrderPreparedEvent>(
                        new ConnectionName("OrderPreparedEvent"),
                        new ChannelName("https://sqs.us-west-2.amazonaws.com/058052576087/OrderPreparedEvent"),
                        new RoutingKey("OrderPreparentEvent")
                        )
                })
                .Build();

            dispatcher.Receive();
        }


        public class SimpleMessageMapperFactory : IAmAMessageMapperFactory
        {
            private readonly IServiceProvider _container;

            public SimpleMessageMapperFactory(IServiceProvider container)
            {
                _container = container;
            }

            public IAmAMessageMapper Create(Type messageMapperType)
            {
                if (messageMapperType == typeof(PrepareOrderCommandMessageMapper))
                {
                    return new PrepareOrderCommandMessageMapper();

                }
                else if(messageMapperType == typeof(OrderPreparentEventMapper))
                {
                    return new OrderPreparentEventMapper();
                }

                return new OrderPickedUpByCookerEventMapper();

                return (IAmAMessageMapper)_container.GetService(messageMapperType);
            }
        }

        public class SimpleHandlerRegistry1 : IAmASubscriberRegistry
        {
            private readonly IAmASubscriberRegistry _subscriberRegistry;

            public SimpleHandlerRegistry1()
            {
                _subscriberRegistry = new SubscriberRegistry();

            }

            public IEnumerable<Type> Get<T>() where T : class, IRequest
            {
                return _subscriberRegistry.Get<T>();
            }

            public void Register<TRequest, TImplementation>() where TRequest : class, IRequest where TImplementation : class, IHandleRequests<TRequest>
            {
                //_container.AddTransient<TImplementation>(); // register the command handler in the unity container
                _subscriberRegistry.Register<TRequest, TImplementation>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<PushHub>("/pushhub");
            });

            app.UseMvc();
        }
    }
}