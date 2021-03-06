using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

namespace PracticalAspNetCore
{
    public class ApplicationOptions
    {
        public string Name { get; set; }

        public int MaximumLimit { get; set;}
    }

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationOptions>( o =>{
                o.Name = "Options Sample";
                o.MaximumLimit = 10;
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                var options = context.RequestServices.GetService<IOptions<ApplicationOptions>>();

                return context.Response.WriteAsync($"Settings Name : {options.Value.Name}  - Maximum limit : {options.Value.MaximumLimit}");
            });
        }
    }
    
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>()
                );
    }
}