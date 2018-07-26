using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR;
using NetConnect.Hosting.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetConnect.Hosting.HubServer.Hubs
{
    public class ChatHub : Hub
    {
        readonly static Dictionary<string, string> userList = new Dictionary<string, string>();

        public override async Task OnConnectedAsync()
        {
            var userId = Context.GetHttpContext().Request.Query[NetConnectClaims.UserId];

            var name = Context.GetHttpContext().Request.Query[NetConnectClaims.Name];
            var lastname = Context.GetHttpContext().Request.Query[NetConnectClaims.Lastname];

            var fullName = name + " " + lastname;

            var connectionId = Context.ConnectionId;

            userList.Add(connectionId, userId);

            await Clients.All.SendAsync("broadcastMessage", fullName, "şuan bağlandı");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            userList.Remove(Context.ConnectionId);

            await Clients.Others.SendAsync("Send", $"{Context.ConnectionId} left");
        }

        public void Send(string name, string message)
        {
            Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public Dictionary<string, string> GetAllUsers()
        {
            return userList;
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
