using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace NetConnect.Hosting.Hub
{
    public class ChatHub : Hub<IChatClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.Send($"{Context.ConnectionId} joined");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.Others.Send($"{Context.ConnectionId} left");
        }

        public Task Send(string message)
        {
            return Clients.All.Send($"{Context.ConnectionId}: {message}");
        }

        public Task SendToOthers(string message)
        {
            return Clients.Others.Send($"{Context.ConnectionId}: {message}");
        }

        public Task SendToGroup(string groupName, string message)
        {
            return Clients.Group(groupName).Send($"{Context.ConnectionId}@{groupName}: {message}");
        }

        public Task SendToOthersInGroup(string groupName, string message)
        {
            return Clients.OthersInGroup(groupName).Send($"{Context.ConnectionId}@{groupName}: {message}");
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).Send($"{Context.ConnectionId} joined {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Clients.Group(groupName).Send($"{Context.ConnectionId} left {groupName}");

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public Task Echo(string message)
        {
            return Clients.Caller.Send($"{Context.ConnectionId}: {message}");
        }
    }

    public interface IChatClient
    {
        Task Send(string message);
    }
}
