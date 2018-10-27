using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Events
{
    public class OrderStatusUpdatedEvent : IRequest
    {
        public string OrderId;
        public string Status;

        public OrderStatusUpdatedEvent(string orderId, string status)
        {
            Status = status;
            OrderId = orderId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}