using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace NetConnect.Hosting.BaseHub
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var fullname = Context.GetUserFullName();

            await Clients.All.SendAsync("broadcastMessage", fullname, "şuan bağlandı");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var fullname = Context.GetUserFullName();

            await Clients.Others.SendAsync("broadcastMessage", fullname, "ayrıldı");
        }

        public void Send(string name, string message)
        {
            var fullname = "";

            if (Context.User.Identity.IsAuthenticated)
            {
                fullname = Context.GetUserFullName();
            }
            else
            {
                fullname = name;
            }

            Clients.All.SendAsync("broadcastMessage", fullname, message);
        }

        public Task SendToOthers(string message)
        {
            return Clients.Others.SendAsync("Send", $"{Context.ConnectionId}: {message}");
        }

        public Task SendToConnection(string connectionId, string message)
        {
            return Clients.Client(connectionId).SendAsync("Send", $"Private message from {Context.ConnectionId}: {message}");
        }

        public Task SendToGroup(string groupName, string message)
        {
            return Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId}@{groupName}: {message}");
        }

        public Task SendToOthersInGroup(string groupName, string message)
        {
            return Clients.OthersInGroup(groupName).SendAsync("Send", $"{Context.ConnectionId}@{groupName}: {message}");
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Clients.Group(groupName).SendAsync("Send", $"{Context.ConnectionId} left {groupName}");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public Task Echo(string message)
        {
            return Clients.Caller.SendAsync("Send", $"{Context.ConnectionId}: {message}");
        }
    }
}