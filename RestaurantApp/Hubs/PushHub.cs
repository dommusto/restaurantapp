using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace RestaurantApp.Hubs
{
    public class PushHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
