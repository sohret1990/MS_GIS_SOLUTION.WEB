using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MS_GIS_SOLUTION.WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)            
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseIISIntegration().ConfigureKestrel(kestrel=>kestrel.Limits.MaxRequestBodySize = 30000000).UseStartup<Startup>();
                });
    }
}
