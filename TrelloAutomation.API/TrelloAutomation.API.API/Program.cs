using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

#pragma warning disable CS1591
namespace TrelloAutomation.API.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        //.NET 5.0
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
