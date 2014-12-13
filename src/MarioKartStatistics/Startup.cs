﻿using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using System.Diagnostics;

namespace MarioKartStatistics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new Configuration();
            configuration
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            services.AddEntityFramework(configuration)
                .AddSqlServer()
                .AddDbContext<MKContext>(o => o.UseSqlServer());

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            //var configuration = new NLog.Config.LoggingConfiguration();
            //configuration.
            //loggerFactory.AddNLog(new global::NLog.LogFactory();
            loggerFactory.AddProvider(new DiagnosticsLoggerProvider(new SourceSwitch("Microsoft.AspNet", "Verbose") { Level = SourceLevels.All }, new ConsoleTraceListener()));

            app.UseMvc();
        }
    }
}
