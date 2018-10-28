using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Hubs;

namespace RestaurantApp.EventHandlers
{
    public class PaymentRequestedEventHandler : RequestHandler<PaymentRequestedEvent>
    {
        public override PaymentRequestedEvent Handle(PaymentRequestedEvent @event)
        {
            HubProvider.HubContext.Clients.All.SendAsync("ReceiveMessage", "EnablePayButton");
            return base.Handle(@event);
        }
    }
}