using Microsoft.AspNet.Builder;
using Microsoft.Data.Entity;
using Microsoft.Framework.ConfigurationModel;
using Microsoft.Framework.DependencyInjection;

namespace MarioKartStatistics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var configuration = new Configuration();
            configuration.AddJsonFile("config.json");

            services.AddEntityFramework(configuration)
                .AddSqlServer()
                .AddDbContext<MKContext>(o => o.UseSqlServer());

            services.AddMvc();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
