using Paramore.Brighter;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.EventHandlers
{
    public class OrderPreparedEventHandler : RequestHandler<OrderPreparedEvent>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        public OrderPreparedEventHandler(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
        }

        public override OrderPreparedEvent Handle(OrderPreparedEvent @event)
        {
            _ordersRepository.UpdateOrderStatus(@event.OrderId, "Food ready");
            _commandProcessor.Publish(new OrderStatusUpdatedEvent(@event.OrderId, "Food ready"));
            return base.Handle(@event);
        }
    }
}