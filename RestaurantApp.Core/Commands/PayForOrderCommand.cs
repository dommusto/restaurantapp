using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Commands
{
    public class PayForOrderCommand : IRequest
    {
        public PayForOrderCommand(string orderId)
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}