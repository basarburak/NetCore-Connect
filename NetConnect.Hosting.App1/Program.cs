using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;

namespace NetConnect.Hosting.App1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "App1";
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
