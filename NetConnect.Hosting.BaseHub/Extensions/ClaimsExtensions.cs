using NetConnect.Hosting.BaseHub.Models;
using NetConnect.Hosting.Core;
using System.Linq;
using System.Security.Claims;

namespace NetConnect.Hosting.BaseHub.Extensions
{
    public static class ClaimsExtensions
    {
        public static ChatUsers GetChatUsers(this ClaimsPrincipal claimsPrincipal)
        {
            return new ChatUsers()
            {
                UserId = claimsPrincipal.Claims.First(x=>x.Type == NetConnectClaims.UserId).Value,
                NameLastname = claimsPrincipal.Claims.First(x => x.Type == NetConnectClaims.Name).Value + " " + claimsPrincipal.Claims.First(x => x.Type == NetConnectClaims.Lastname).Value
            };
        }
    }
}