using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Events
{
    public class OrderAddedEvent : IRequest
    {
        public string OrderId;

        public OrderAddedEvent(string orderId)
        {
            OrderId = orderId;
        }

        public Guid Id { get; set; }
    }
}