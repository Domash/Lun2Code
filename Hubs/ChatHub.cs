using System.Threading.Tasks;
using Lun2Code.Models;
using Microsoft.AspNetCore.SignalR;

namespace Lun2Code.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(GeneralChatMessage message) =>
            await Clients.All.SendAsync("Send", message);
    }
}