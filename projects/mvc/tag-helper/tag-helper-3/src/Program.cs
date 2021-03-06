using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Hosting;

namespace TagHelperSeries
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }

    public enum AlertType
    {
        Success,
        Warning,
        Info,
        Danger
    }

    [HtmlTargetElement("alert")]
    public class AlertHelper : TagHelper
    {
        public AlertType Type { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.Add("class", $"alert {GetAlertType()}");
        }

        string GetAlertType()
        {
            switch (Type)
            {
                case AlertType.Success: return "alert-success";
                case AlertType.Warning: return "alert-warning";
                case AlertType.Info: return "alert-info";
                case AlertType.Danger: return "alert-danger";
                default: throw new System.ArgumentOutOfRangeException();
            }
        }
    }

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
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