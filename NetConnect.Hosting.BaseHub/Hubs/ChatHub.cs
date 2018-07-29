using Microsoft.AspNetCore.SignalR;
using NetConnect.Hosting.BaseHub.ChatHub.Models;
using NetConnect.Hosting.BaseHub.Extensions;
using NetConnect.Hosting.BaseHub.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetConnect.Hosting.BaseHub.Hubs
{
    public class ChatHub : Hub
    {
        readonly static Dictionary<string, ChatUsers> userList = new Dictionary<string, ChatUsers>();

        public override async Task OnConnectedAsync()
        {
            await Task.CompletedTask;

            userList.Add(Context.ConnectionId, Context.User.GetChatUsers());

            SendOnlineUsers();
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Task.CompletedTask;

            userList.Remove(Context.ConnectionId);

            SendOnlineUsers();
        }

        public void Send(string name, string message)
        {
            var fullname = Context.User.Identity.IsAuthenticated ? Context.GetUserFullName() : name;

            Clients.All.SendAsync(ChatMethod.ReceiveMessage, fullname, message);
        }

        private void SendOnlineUsers()
        {
            Clients.All.SendAsync(ChatMethod.OnlineUser, userList.MapChatUsers());
        }
    }
}