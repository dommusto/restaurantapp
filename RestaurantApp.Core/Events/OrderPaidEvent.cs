using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Events
{
    public class OrderPaidEvent : IRequest
    {
        public string OrderId;

        public OrderPaidEvent(string orderId)
        {
            OrderId = orderId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}