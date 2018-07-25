using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace NetConnect.Hosting.BaseHub
{
    public static class ServiceExtensions
    {
        public static IApplicationBuilder UseChatHub(this IApplicationBuilder app)
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chat");
            });

            return app;
        }

        public static IServiceCollection AddChatHubRedis(this IServiceCollection services)
        {
            services.AddSignalR().AddRedis("127.0.0.1:6379");

            return services;
        }
    }
}