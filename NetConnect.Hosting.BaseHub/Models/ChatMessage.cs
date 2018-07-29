using System.Collections.Generic;

namespace NetConnect.Hosting.BaseHub.ChatHub.Models
{
    public class ChatMessage
    {
        public string CurrentConenctionId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Fullname { get; set; }
        public string Message { get; set; }
        public List<string> Groups { get; set; }
    }
}
