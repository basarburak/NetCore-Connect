using Microsoft.AspNetCore.SignalR;
using NetConnect.Hosting.Core;
using System.Linq;

namespace NetConnect.Hosting.BaseHub.ChatHub
{
    public static class ChatHubExtensions
    {
        public static string GetUserFullName(this HubCallerContext hubCallerContext)
        {
            if (hubCallerContext.User.Identity.IsAuthenticated)
            {
                var name = hubCallerContext.User.Claims.FirstOrDefault(x => x.Type == NetConnectClaims.Name).Value;
                var lastname = hubCallerContext.User.Claims.FirstOrDefault(x => x.Type == NetConnectClaims.Lastname).Value;
                return name + " " + lastname;
            }
            return hubCallerContext.ConnectionId;
        }
    }
}