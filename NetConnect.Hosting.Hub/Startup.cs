using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetConnect.Hosting.Hub.ConnectionHandlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace NetConnect.Hosting.Hub
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddConnections();

            services.AddSignalR(options =>
            {
                options.KeepAliveInterval = TimeSpan.FromSeconds(5);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseFileServer();

            app.UseConnections(routes =>
            {
                routes.MapConnectionHandler<MessagesConnectionHandler>("/chat");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/hubT");
            });

            app.Use(next => (context) =>
            {
                if (context.Request.Path.StartsWithSegments("/deployment"))
                {
                    var attributes = Assembly.GetAssembly(typeof(Startup)).GetCustomAttributes<AssemblyMetadataAttribute>();

                    context.Response.ContentType = "application/json";
                    using (var textWriter = new StreamWriter(context.Response.Body))
                    using (var writer = new JsonTextWriter(textWriter))
                    {
                        var json = new JObject();
                        var commitHash = string.Empty;

                        foreach (var attribute in attributes)
                        {
                            json.Add(attribute.Key, attribute.Value);

                            if (string.Equals(attribute.Key, "CommitHash"))
                            {
                                commitHash = attribute.Value;
                            }
                        }

                        if (!string.IsNullOrEmpty(commitHash))
                        {
                            json.Add("GitHubUrl", $"https://github.com/aspnet/SignalR/commit/{commitHash}");
                        }

                        json.WriteTo(writer);
                    }
                }
                return Task.CompletedTask;
            });
        }
    }
}
