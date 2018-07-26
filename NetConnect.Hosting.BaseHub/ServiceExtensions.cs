using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NetConnect.Hosting.BaseHub
{
    public static class ServiceExtensions
    {
        public static IApplicationBuilder UseChatHub(this IApplicationBuilder app, string chatUrl = "/chat")
        {
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>(chatUrl);
            });

            return app;
        }

        public static IServiceCollection AddChatHub(this IServiceCollection services, bool addRedis = false)
        {
            if (addRedis)
                services.AddSignalR().AddRedis("127.0.0.1:6379");
            else
                services.AddSignalR();

            return services;
        }
    }
}