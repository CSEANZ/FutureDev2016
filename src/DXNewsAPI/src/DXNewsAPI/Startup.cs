using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DXNewsAPI.Model.Contract;
using DXNewsAPI.Model.Entity;
using DXNewsAPI.Model.Repo;
using DXNewsAPI.Model.Service;
using DXNewsAPI.Model.SetupHelpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.Swagger.Model;

namespace DXNewsAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);

                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        private AuthCallbackHandler _callbackHandler;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            services.Configure<TableStorageSettings>(Configuration.GetSection("ConnectionStrings:TableStorage"));

            services.AddMvc();

            services.AddScoped<IDataService, SampleDataService>();
            services.AddSingleton<ITableStorageRepo, TableStorageRepo>();

            services.AddAutoMapper(cfg =>
            {
                //Map the Id field on the NewsItem to and from the RowKey field on the Table Entity version of it
                cfg.CreateMap<NewsItem, NewsItemTableEntity>()
                  .ForMember(dest => dest.RowKey, opt => opt.MapFrom(src => src.Id));

                cfg.CreateMap<NewsItemTableEntity, NewsItem>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src=>src.RowKey));
            });

            services.AddSwaggerGen(options =>
            {

            });

            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "DXNewsAPI",
                    Description = "For all the news from DX Australia"
                });
            });

            services.AddAuthentication(sharedOptions => sharedOptions.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IServiceProvider serviceProvider, ITableStorageRepo tableStore)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //I am not a fan of this, but I want it done in startup, before and end-user-developer code. 
            tableStore.Init().GetAwaiter().GetResult();

            app.UseApplicationInsightsRequestTelemetry();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseApplicationInsightsExceptionTelemetry();

            app.UseStaticFiles();

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            _callbackHandler = new AuthCallbackHandler();

            app.UseDxAuth(serviceProvider, Configuration, _callbackHandler);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            // Enable middleware to serve swagger-ui assets (HTML, JS, CSS etc.)
            app.UseSwaggerUi();
        }
    }
}
