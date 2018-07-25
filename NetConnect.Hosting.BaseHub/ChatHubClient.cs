using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace NetConnect.Hosting.BaseHub
{
    public class ChatHubClient
    {
        private string uri;
        public ChatHubClient(string uriParam)
        {
            uri = uriParam;
        }
        public async void Connect()
        {
            var hubUrl = uri + "/chat";

            var hubConnection = new HubConnectionBuilder()
            .WithUrl(hubUrl, options =>
            {
                options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
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
        }
        public void InvokeMessage(string sender, string message)
        {

        }
    }
}
