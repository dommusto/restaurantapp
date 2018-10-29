using System;
using Paramore.Brighter;

namespace RestaurantApp.Core.Commands
{
    public class AddOrderCommand : IRequest
    {
        public string MenuItem;

        public AddOrderCommand(string menuItem)
        {
            MenuItem = menuItem;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string OrderId { get; set; }
    }
}