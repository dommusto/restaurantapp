using Microsoft.AspNetCore.SignalR;

namespace RestaurantApp.Hubs
{
    public static class HubProvider
    {
        public static IHubContext<PushHub> HubContext { get; set; }
    }
}