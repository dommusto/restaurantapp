using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Hubs;

namespace RestaurantApp.EventHandlers
{
    public class OrderCompltetedEventHandler : RequestHandler<OrderCompletedEvent>
    {
        public override OrderCompletedEvent Handle(OrderCompletedEvent @event)
        {
            HubProvider.HubContext.Clients.All.SendAsync("ReceiveMessage", "All done, enjoy");
            return base.Handle(@event);
        }
    }
}