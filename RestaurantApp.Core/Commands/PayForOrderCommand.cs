using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Commands
{
    public class PayForOrderCommand : IRequest
    {
        public string OrderId { get; }

        public PayForOrderCommand(string orderId)
        {
            OrderId = orderId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}