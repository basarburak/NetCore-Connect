using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetConnect.Hosting.Core
{
    public static class ChatHubContext
    {
        public static async void Connect(this HubConnection hubConnection)
        {
            try
            {
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

        public async static void SendMessage(this HubConnection hubConnection, string message)
        {
            await hubConnection.InvokeAsync("Send", "test message");
        }

        public async static void Disconnect(this HubConnection hubConnection)
        {
            await hubConnection.DisposeAsync().ContinueWith(t =>
            {
                if (t.IsFaulted)
                    Console.WriteLine(t.Exception.GetBaseException());
                else
                    Console.WriteLine("Disconnected");
            });
        }

        public static void InvokeMessage(string sender, string message)
        {

        }

    }
}
