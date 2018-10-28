using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Hubs;

namespace RestaurantApp.EventHandlers
{
    public class OrderStatusUpdatedEventHandler : RequestHandler<OrderStatusUpdatedEvent>
    {
        public override OrderStatusUpdatedEvent Handle(OrderStatusUpdatedEvent @event)
        {
            HubProvider.HubContext.Clients.All.SendAsync("ReceiveMessage", @event.Status);
            return base.Handle(@event);
        }
    }
}