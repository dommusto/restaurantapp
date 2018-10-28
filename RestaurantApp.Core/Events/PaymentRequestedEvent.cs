using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Events
{
    public class PaymentRequestedEvent : IRequest
    {
        public string OrderId;

        public PaymentRequestedEvent(string orderId)
        {
            OrderId = orderId;
        }

        public Guid Id { get; set; }
    }
}