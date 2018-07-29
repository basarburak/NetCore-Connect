using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NetConnect.Hosting.BaseHub.Extensions
{
    public static class ServiceExtensions
    {
        public static IApplicationBuilder UseChatHub(this IApplicationBuilder app, string chatUrl = "/chat")
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<NetConnect.Hosting.BaseHub.Hubs.ChatHub>(chatUrl);
            });

            return app;
        }

        public static IServiceCollection AddChatHub(this IServiceCollection services, bool addRedis = false, string redisConnectionString = "127.0.0.1:6379")
        {
            if (addRedis)
                services.AddSignalR().AddRedis(redisConnectionString);
            else
                services.AddSignalR();

            return services;
        }
    }
}