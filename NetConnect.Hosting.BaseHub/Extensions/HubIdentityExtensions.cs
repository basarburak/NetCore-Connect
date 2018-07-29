using Microsoft.AspNetCore.SignalR;
using NetConnect.Hosting.BaseHub.Models;
using System.Collections.Generic;
using System.Linq;

namespace NetConnect.Hosting.BaseHub.Extensions
{
    public static class HubIdentityExtensions
    {
        public static string GetUserFullName(this HubCallerContext hubCallerContext)
        {
            if (hubCallerContext.User.Identity.IsAuthenticated)
                return hubCallerContext.User.GetChatUsers().NameLastname;

            return hubCallerContext.ConnectionId;
        }

        public static List<ChatUsers> MapChatUsers(this Dictionary<string, ChatUsers> dictionary)
        {
            return dictionary
               .Select(kvp => new ChatUsers()
               {
                   ConnectionId = kvp.Key,
                   NameLastname = kvp.Value.NameLastname,
                   UserId = kvp.Value.UserId
               }).ToList();
        }
    }
}