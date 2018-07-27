using Microsoft.AspNetCore.SignalR;
using NetConnect.Hosting.BaseHub.ChatHub.Models;
using System;
using System.Threading.Tasks;

namespace NetConnect.Hosting.BaseHub.ChatHub
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync(ChatMethod.ReceiveMessage, Context.GetUserFullName(), ChatHubNotification.ConnectedUser);
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.Others.SendAsync(ChatMethod.ReceiveMessage, Context.GetUserFullName(), ChatHubNotification.DisconnectedUser);
        }

        public void Send(string name, string message)
        {
            var fullname = Context.User.Identity.IsAuthenticated ? Context.GetUserFullName() : name;

            Clients.All.SendAsync(ChatMethod.ReceiveMessage, fullname, message);
        }

        public void SendMessage(ChatMessage chatMessage)
        {

        }
    }
}