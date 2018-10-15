using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Paramore.Brighter.AspNetCore;
using RestaurantApp.Core;
using RestaurantApp.Core.Commands;
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
            services.AddBrighter(opts=> 
                opts.HandlerLifetime = ServiceLifetime.Singleton).HandlersFromAssemblies(typeof(PrepareOrderCommand).Assembly).HandlersFromAssemblies(typeof(OrderPreparedEventHandler).Assembly);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

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
