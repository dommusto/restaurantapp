using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Events
{
    public class OrderPaidEvent : IRequest
    {
        private readonly string _orderId;

        public OrderPaidEvent(string orderId)
        {
            Id = Guid.NewGuid();
            _orderId = orderId;
        }

        public Guid Id { get; set; }
    }
}