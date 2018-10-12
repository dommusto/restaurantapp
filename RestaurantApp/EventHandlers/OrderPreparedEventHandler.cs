using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Hubs;

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
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Food ready");
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "EnablePayButton");
            return base.Handle(@event);
        }
    }
}