using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Hubs;

namespace RestaurantApp.EventHandlers
{
    public class OrderPreparedEventHandler : RequestHandler<OrderPreparedEvent>
    {
        public override OrderPreparedEvent Handle(OrderPreparedEvent @event)
        {
            HubProvider.HubContext.Clients.All.SendAsync("ReceiveMessage", "EnablePayButton");
            return base.Handle(@event);
        }
    }
}