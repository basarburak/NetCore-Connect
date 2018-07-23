using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using NetConnect.Hosting.Core;
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
                var name = User.Claims.First(x => x.Type == NetConnectClaims.Name).Value;
                var lastname = User.Claims.First(x => x.Type == NetConnectClaims.Lastname).Value;
                var userId = User.Claims.First(x => x.Type == NetConnectClaims.UserId).Value;


                var hubUrl = "https://localhost:44317/chat";

                var hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl, options =>
                {
                    options.Transports = transportType;
                    options.Headers = options.Headers = new Dictionary<string, string>() { { NetConnectClaims.UserId, userId }, { NetConnectClaims.Name, name }, { NetConnectClaims.Lastname, lastname } }; ;
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
        }

        public void InvokeMessage(string sender, string message)
        {

        }
    }
}