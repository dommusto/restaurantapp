using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Hubs;
using RestaurantApp.Pages;

namespace RestaurantApp.EventHandlers
{
    public class OrderPreparedEventHandler : RequestHandler<OrderPreparedEvent>
    {
        private readonly IHubContext<PushHub> _hubContext;

        public OrderPreparedEventHandler(IHubContext<PushHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public override OrderPreparedEvent Handle(OrderPreparedEvent @event)
        {
            stuff.hubcontext.Clients.All.SendAsync("ReceiveMessage", "Food ready");
            stuff.hubcontext.Clients.All.SendAsync("ReceiveMessage", "EnablePayButton");
            return base.Handle(@event);
        }
    }
}