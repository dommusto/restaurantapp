using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Commands
{
    public class AddOrderCommand : IRequest
    {
        public AddOrderCommand(string menuItem)
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}