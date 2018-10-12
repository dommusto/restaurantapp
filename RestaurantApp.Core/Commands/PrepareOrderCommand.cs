using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Commands
{
    public class PrepareOrderCommand : IRequest
    {
        public string OrderId { get; }

        public PrepareOrderCommand(string orderId)
        {
            OrderId = orderId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}