using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AWSServerless_webAPI.Helpers;

namespace AWSServerless_webAPI
{
    public class Startup
    {
        public const string AppS3BucketKey = "AppS3Bucket";
        public const string ClientURL = "ClientURL";
        const string MyAllowSpecificOrigins = "CORSPolicy";
        const string HubPathString = "HubPathString";
        const string SignalR_Enabled= "SignalR_Enabled";
        const string SignalR_Endpoint = "Azure:SignalR:ConnectionString";
        bool SR_Enabled;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            string signalREnabled = Configuration[SignalR_Enabled];
            
            Boolean.TryParse(signalREnabled, out SR_Enabled);
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(IDataManager), typeof(DataManager));
            services.AddSingleton(typeof(ITimerManager), typeof(TimerManager));
            services.AddAutoMapper(typeof(Startup));
            var clientURL = Configuration[Startup.ClientURL];
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins(clientURL)
            .AllowCredentials();
                });
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            if (SR_Enabled)
            {
                services.AddSignalR().AddAzureSignalR(Configuration[SignalR_Endpoint]);
            }
            // Add S3 to the ASP.NET Core dependency injection framework.
            services.AddAWSService<Amazon.S3.IAmazonS3>();
            //services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            //allow Cors for any origin
            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseMvc();
            //TODO: Remove this flag,when fixed on production
           

            if (SR_Enabled) 
            {
                app.UseAzureSignalR(route =>
                {
                    route.MapHub<VehicleHub>(Configuration[HubPathString]);
                });
            }
          
        }
    }
}
