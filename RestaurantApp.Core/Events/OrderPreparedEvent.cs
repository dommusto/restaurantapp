using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Events
{
    public class OrderPreparedEvent : IRequest
    {
        public string OrderId { get; }

        public OrderPreparedEvent(string orderId)
        {
            OrderId = orderId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}