using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using System.Diagnostics;
using System;

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
            loggerFactory.AddNLog(new global::NLog.LogFactory());

            var logger = loggerFactory.Create("aaa");
            logger.WriteError("Logged error!!");

            app.UseErrorHandler("/Home/Error");
            app.UseMvc();
        }
    }
}
