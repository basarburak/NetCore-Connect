using Microsoft.Extensions.DependencyInjection;

namespace NetConnect.Hosting.Core.Extensions
{
    public static class OpenIdExtensions
    {
        public static IServiceCollection AddNetConnectOpenIdCookie(this IServiceCollection services, string clientId, string Authority, bool saveToken)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
               .AddCookie("Cookies")
               .AddOpenIdConnect("oidc", options =>
               {
                   options.SignInScheme = "Cookies";

                   options.Authority = Authority;
                   options.RequireHttpsMetadata = false;

                   options.ClientId = clientId;
                   options.SaveTokens = saveToken;
               });

            return services;
        }
        public static IServiceCollection AddNetConnectOpenIdCookie(this IServiceCollection services, string clientId, string Authority, bool saveToken, string defaultScheme)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = defaultScheme;
                options.DefaultChallengeScheme = "oidc";
            })
               .AddCookie(defaultScheme)
               .AddOpenIdConnect("oidc", options =>
               {
                   options.SignInScheme = defaultScheme;

                   options.Authority = Authority;
                   options.RequireHttpsMetadata = false;

                   options.ClientId = clientId;
                   options.SaveTokens = saveToken;
               });

            return services;
        }
    }
}