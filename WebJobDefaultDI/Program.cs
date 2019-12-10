//===============================================================================
// Microsoft FastTrack for Azure
// .Net Core 3 WebJob and Dependency Injection Samples
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Logging.Console;
using System.IO;

namespace WebJobDefaultDI
{
    class Program
    {
        static void Main(string[] args)
        {
            HostBuilder builder = new HostBuilder();

            // Allows us to read the configuration file from current directory
            // (remember to copy those files to the OutputDirectory in VS)
            builder.UseContentRoot(Directory.GetCurrentDirectory());
            
            builder.ConfigureWebJobs(b =>
            {
                b.AddAzureStorageCoreServices();
                b.AddServiceBus(); // Support service bus triggers and bindings
            });

            // This step allows the environment variables to be read BEFORE the rest of the configuration
            // This is useful in configuring the hosting environment in debug, by setting the 
            // ENVIRONMENT variable in VS
            builder.ConfigureHostConfiguration(config =>
            {
                config.AddEnvironmentVariables();
            });

            // Read the configuration from json file
            builder.ConfigureAppConfiguration((context, config) =>
            {
                IHostingEnvironment env = context.HostingEnvironment;

                config
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                config.AddEnvironmentVariables();
            });

            // Configure logging (you can use the config here, via context.Configuration)
            builder.ConfigureLogging((context, loggingBuilder) =>
            {
                loggingBuilder.AddConfiguration(context.Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();

                // If this key exists in any config, use it to enable App Insights
                var appInsightsKey = context.Configuration["APPINSIGHTS_INSTRUMENTATIONKEY"];
                if (!string.IsNullOrEmpty(appInsightsKey))
                {
                    loggingBuilder.AddApplicationInsights(appInsightsKey);
                }
            });

            // Register dependency injected services
            builder.ConfigureServices(services =>
            {
                services.AddTransient<ISimpleService, SimpleService>();
            });

            builder.UseConsoleLifetime();

            IHost host = builder.Build();
            using (host)
            {
                host.Run(); // Start a continuously running WebJob
            }
        }
    }
}