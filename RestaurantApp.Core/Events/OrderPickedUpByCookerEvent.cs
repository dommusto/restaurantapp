using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Events
{
    public class OrderPickedUpByCookerEvent : IRequest
    {
        public string OrderId;

        public OrderPickedUpByCookerEvent(string orderId)
        {
            OrderId = orderId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}