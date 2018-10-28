using Paramore.Brighter;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.CommandHandlers
{
    public class AddOrderCommandHandler : RequestHandler<AddOrderCommand>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        public AddOrderCommandHandler(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
        }

        public override AddOrderCommand Handle(AddOrderCommand command)
        {
            var orderId = _ordersRepository.AddOrder(command.MenuItem);
            command.OrderId = orderId;
            _commandProcessor.Publish(new OrderAddedEvent(orderId));
            return base.Handle(command);
        }
    }
}