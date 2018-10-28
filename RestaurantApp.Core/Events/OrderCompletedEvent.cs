using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Events
{
    public class OrderCompletedEvent : IRequest
    {
        public string OrderId;

        public OrderCompletedEvent(string orderId)
        {
            OrderId = orderId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}