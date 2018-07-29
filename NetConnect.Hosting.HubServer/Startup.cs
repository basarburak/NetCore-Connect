using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetConnect.Hosting.BaseHub.Extensions;

namespace NetConnect.Hosting.HubServer
{
    public class Startup
    {
        string policy = "Everything";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddChatHub(false);

            services.AddCors(o =>
            {
                o.AddPolicy(policy, p =>
                {
                    p.AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowAnyOrigin()
                     .AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseCors(policy);

            app.UseChatHub();

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello Hub Server!");
            });
        }
    }
}
