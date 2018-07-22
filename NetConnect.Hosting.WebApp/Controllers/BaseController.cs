using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetConnect.Hosting.WebApp.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public async void Connect(HttpTransportType transportType)
        {
            try
            {
                var hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:44317/" + "chat", options =>
                {
                    options.Transports = transportType;
                })
                .Build();

                var closedTcs = new TaskCompletionSource<object>();

                hubConnection.Closed += e =>
                {
                    closedTcs.SetResult(null);
                    return Task.CompletedTask;
                };

                hubConnection.On<string, string>("Send", (sender, message) => ConnectionState(sender, message));

                await hubConnection.StartAsync();

                await hubConnection.InvokeAsync("Send", "test message");


                await hubConnection.DisposeAsync().ContinueWith(t =>
                {
                    if (t.IsFaulted)
                        Console.WriteLine(t.Exception.GetBaseException());
                    else
                        Console.WriteLine("Disconnected");
                });

            }
            catch (Exception ex)
            {

            }
        }
        public void ConnectionState(string sender, string message)
        {

        }
    }
}
