using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Commands
{
    public class RequestPaymentCommand : IRequest
    {
        public string MenuItem;

        public RequestPaymentCommand(string orderId)
        {
            OrderId = orderId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string OrderId { get; set; }
    }
}