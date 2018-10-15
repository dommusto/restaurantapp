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
            HubContextProvider.HubContext.Clients.All.SendAsync("ReceiveMessage", "Food ready");
            HubContextProvider.HubContext.Clients.All.SendAsync("ReceiveMessage", "EnablePayButton");
            return base.Handle(@event);
        }
    }
}