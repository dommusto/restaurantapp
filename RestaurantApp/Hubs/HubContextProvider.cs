using Microsoft.AspNetCore.SignalR;

namespace RestaurantApp.Hubs
{
    public static class HubContextProvider
    {
        public static IHubContext<PushHub> HubContext { get; set; }
    }
}