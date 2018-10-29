using Paramore.Brighter;
using RestaurantApp.Core.Commands;

namespace RestaurantApp.Core.CommandHandlers
{
    public class AddOrderCommandHandler : RequestHandler<AddOrderCommand>
    {
        private readonly IOrdersRepository _ordersRepository;

        public AddOrderCommandHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public override AddOrderCommand Handle(AddOrderCommand command)
        {
            command.OrderId = _ordersRepository.AddOrder(command.MenuItem);
            return base.Handle(command);
        }
    }
}