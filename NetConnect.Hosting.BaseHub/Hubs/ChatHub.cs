using Microsoft.AspNetCore.SignalR;
using NetConnect.Hosting.BaseHub.ChatHub.Models;
using NetConnect.Hosting.BaseHub.Extensions;
using NetConnect.Hosting.BaseHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        //public void Send(string name, string message)
        //{
        //    var fullname = Context.User.Identity.IsAuthenticated ? Context.GetUserFullName() : name;

        //    Clients.All.SendAsync(ChatMethod.ReceiveMessage, fullname, message);
        //}

        public void Send(string name, string message, string userId)
        {
            var fullname = Context.User.Identity.IsAuthenticated ? Context.GetUserFullName() : name;

            var userConnectionIds = userList.MapChatUsers().Where(x => x.UserId == userId).Select(x => x.ConnectionId).ToList();

            Clients.Client(userConnectionIds.FirstOrDefault()).SendAsync(ChatMethod.ReceiveMessage, name, message);
        }

        private void SendOnlineUsers()
        {
            Clients.All.SendAsync(ChatMethod.OnlineUser, userList.MapChatUsers());
        }
    }
}