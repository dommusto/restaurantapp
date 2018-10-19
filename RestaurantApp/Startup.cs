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
using Newtonsoft.Json;
using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Brighter.MessagingGateway.AWSSQS;
using Paramore.Brighter.ServiceActivator;
using RestaurantApp.Core;
using RestaurantApp.Core.CommandHandlers;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;
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

            services.AddSingleton<IOrdersRepository, OrdersRepository>();
            services.AddSingleton<IMenuItemsRepository, MenuItemsRepository>();
            configure(services);
            services.AddBrighter(opts=> 
                opts.HandlerLifetime = ServiceLifetime.Singleton).HandlersFromAssemblies(typeof(PrepareOrderCommand).Assembly).HandlersFromAssemblies(typeof(OrderPreparedEventHandler).Assembly);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }

        private void configure(IServiceCollection services)
        {
            var actualContainer = services.BuildServiceProvider();
            var subscriberRegistry = new SimpleHandlerRegistry(services);
            subscriberRegistry.Register<PrepareOrderCommand, PrepareOrderCommandHandler>();
            var amAMessageMapperFactory = new SimpleMessageMapperFactory(actualContainer);
            var messageMapperRegistry = new MessageMapperRegistry(amAMessageMapperFactory);
            messageMapperRegistry.Add(typeof(PrepareOrderCommand), typeof(PrepareOrderCommandMessageMapper));
            var basicAwsCredentials = new BasicAWSCredentials("AKIAIBZ5VBPX4KOB6E5Q", "fnj52eSwIbz5MXr6qEcjRSr27PBJ/D5bu7bs57CQ");
            var commandProcessor = CommandProcessorBuilder.With()
                .Handlers(new HandlerConfiguration(subscriberRegistry, new SimpleHandlerFactory(actualContainer)))
                .DefaultPolicy()
                .TaskQueues(new MessagingConfiguration(new InMemoryMessageStore(), new SqsMessageProducer(basicAwsCredentials, RegionEndpoint.USWest2), messageMapperRegistry))
                .RequestContextFactory(new InMemoryRequestContextFactory())
                .Build();

            commandProcessor.Post(new PrepareOrderCommand("someorderid"));
            var dispatcher = DispatchBuilder.With()
                .CommandProcessor(commandProcessor)
                .MessageMappers(messageMapperRegistry)
                .DefaultChannelFactory(new InputChannelFactory(new SqsMessageConsumerFactory(basicAwsCredentials, RegionEndpoint.USWest2)))
                .Connections(new List<Connection>()
                {
                    new Connection<PrepareOrderCommand>(
                        new ConnectionName("Connection1"), 
                        new ChannelName("https://sqs.us-west-2.amazonaws.com/058052576087/testqueue"),
                        new RoutingKey("PrepareOrderCommand")
                        )
                })
                .Build();

            dispatcher.Receive();
        }


        public class PrepareOrderCommandMessageMapper : IAmAMessageMapper<PrepareOrderCommand>
        {
            public Message MapToMessage(PrepareOrderCommand request)
            {
                var header = new MessageHeader(messageId: request.Id, topic: "testtopic", messageType: MessageType.MT_EVENT);
                var body = new MessageBody(JsonConvert.SerializeObject(request));
                var message = new Message(header, body);
                return message;
            }

            public PrepareOrderCommand MapToRequest(Message message)
            {
                return JsonConvert.DeserializeObject<PrepareOrderCommand>(message.Body.Value);
            }
        }

        public class SimpleHandlerFactory : IAmAHandlerFactory
        {
            private readonly IServiceProvider _container;

            public SimpleHandlerFactory(IServiceProvider container)
            {
                _container = container;
            }

            public IHandleRequests Create(Type handlerType)
            {
                return new PrepareOrderCommandHandler(new OrdersRepository(), null);
                return _container.GetService(handlerType) as IHandleRequests;
            }

            public void Release(IHandleRequests handler) { }
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
                return new PrepareOrderCommandMessageMapper();
                return (IAmAMessageMapper)_container.GetService(messageMapperType);
            }
        }

        public class SimpleHandlerRegistry : IAmASubscriberRegistry
        {
            private readonly IServiceCollection _container;
            private readonly IAmASubscriberRegistry _subscriberRegistry;

            public SimpleHandlerRegistry(IServiceCollection container)
            {
                _subscriberRegistry = new SubscriberRegistry();
                _container = container;
            }

            public IEnumerable<Type> Get<T>() where T : class, IRequest
            {
                return _subscriberRegistry.Get<T>();
            }

            public void Register<TRequest, TImplementation>() where TRequest : class, IRequest where TImplementation : class, IHandleRequests<TRequest>
            {
                _container.AddTransient<TImplementation>(); // register the command handler in the unity container
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
