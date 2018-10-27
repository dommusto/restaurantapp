using Paramore.Brighter;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.EventHandlers
{
    public class OrderPickedUpByCookerEventHandler : RequestHandler<OrderPickedUpByCookerEvent>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        public OrderPickedUpByCookerEventHandler(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
        }

        public override OrderPickedUpByCookerEvent Handle(OrderPickedUpByCookerEvent @event)
        {
            if (_ordersRepository.GetOrderStatus(@event.OrderId) == "Food ready")
            {
                return base.Handle(@event);
            }
            _ordersRepository.UpdateOrderStatus(@event.OrderId, "Preparing food");
            _commandProcessor.Publish(new OrderStatusUpdatedEvent(@event.OrderId, "Preparing food"));
            return base.Handle(@event);
        }
    }
}