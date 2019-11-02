using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ShopifySharp;
using ShopInsights.Core;
using ShopInsights.Core.Services.Shopify;
using ShopInsights.Core.Stores;
using ShopInsights.Infrastructure;
using ShopInsights.Web.Stores;

namespace ShopInsights.Web
{
    public class Startup
    {
        private readonly IHostEnvironment _hostEnvironment;

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllers();

            services.AddInfrastructureServices();
            services.AddCoreServices();

            ConfigureOptions(services);
            ConfiguraAppServices(services);
            ConfigureBackgroundServices(services);

            ShopifyService.SetGlobalExecutionPolicy(new SmartRetryExecutionPolicy());
        }

        private void ConfiguraAppServices(IServiceCollection services)
        {
            services.AddTransient<IImportAndSaveNewData, ImportAndSaveNewData>();
            services.AddTransient<IExistingDataImporter, ExistingDataImporter>();
        }
        private void ConfigureBackgroundServices(IServiceCollection services)
        {
            services.AddHostedService<DataInitialization>();
        }

        private void ConfigureOptions(IServiceCollection services)
        {
            services.AddTransient<IValidateOptions<OrderStoreOptions>, OrderStoreOptionsValidator>();

            services.AddOptions<OrderStoreOptions>()
                .Bind(Configuration.GetSection("Shop:OrderStore"));

            services.AddOptions<StoreOptions>()
                .Bind(Configuration.GetSection("Shop:Store"));

            services.AddOptions<ShopifyAuthenticationOptions>()
                .Bind(Configuration.GetSection("Shopify"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
