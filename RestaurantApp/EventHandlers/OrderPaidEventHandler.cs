using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Hubs;

namespace RestaurantApp.EventHandlers
{
    public class OrderPaidEventHandler : RequestHandler<OrderPaidEvent>
    {
        private readonly IHubContext<PushHub> _hubContext;

        public OrderPaidEventHandler(IHubContext<PushHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public override OrderPaidEvent Handle(OrderPaidEvent @event)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "All done, enjoy");
            return base.Handle(@event);
        }
    }
}