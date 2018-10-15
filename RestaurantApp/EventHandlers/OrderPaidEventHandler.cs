using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Hubs;

namespace RestaurantApp.EventHandlers
{
    public class OrderPaidEventHandler : RequestHandler<OrderPaidEvent>
    {
        public override OrderPaidEvent Handle(OrderPaidEvent @event)
        {
            HubContextProvider.HubContext.Clients.All.SendAsync("ReceiveMessage", "All done, enjoy");
            return base.Handle(@event);
        }
    }
}