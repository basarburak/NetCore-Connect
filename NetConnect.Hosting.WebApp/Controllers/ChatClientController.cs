using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using NetConnect.Hosting.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetConnect.Hosting.WebApp.Controllers
{
    public class ChatClientController : Controller
    {
        public static HubConnection hubConnection;

        //Connent SignalR
        public async Task<IActionResult> Index()
        {
            try
            {
                var name = User.Claims.First(x => x.Type == NetConnectClaims.Name).Value;
                var lastname = User.Claims.First(x => x.Type == NetConnectClaims.Lastname).Value;
                var userId = User.Claims.First(x => x.Type == NetConnectClaims.UserId).Value;


                var hubUrl = "https://localhost:44317/chat";

                var hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl, options =>
                {
                    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
                    options.Headers = options.Headers = new Dictionary<string, string>() { { NetConnectClaims.UserId, userId }, { NetConnectClaims.Name, name }}; ;
                })
                .Build();

                var closedTcs = new TaskCompletionSource<object>();

                hubConnection.Closed += e =>
                {
                    closedTcs.SetResult(null);
                    return Task.CompletedTask;
                };

                hubConnection.On<string, string>("broadcastMessage", (sender, message) => InvokeMessage(sender, message));

                //Connect
                await hubConnection.StartAsync();

                ////Send Message
                //await hubConnection.InvokeAsync("Send", "test message");

                ////Disconnecct
                //await hubConnection.DisposeAsync().ContinueWith(t =>
                //{
                //    if (t.IsFaulted)
                //        Console.WriteLine(t.Exception.GetBaseException());
                //    else
                //        Console.WriteLine("Disconnected");
                //});

            }
            catch (Exception ex)
            {

            }

            return View();
        }

        public IActionResult InvokeMessage(string sender, string message)
        {
            return View();
        }

    }
}
