using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Core.ProcessManager;

namespace RestaurantApp.Core.EventHandlers
{
    public class OrderPaidEventHandler : RequestHandler<OrderPaidEvent>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IOrderProcessManager _processManager;

        public OrderPaidEventHandler(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor, IOrderProcessManager processManager)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
            _processManager = processManager;
        }

        public override OrderPaidEvent Handle(OrderPaidEvent @event)
        {
            _ordersRepository.UpdateOrderStatus(@event.OrderId, "Order paid");
            _commandProcessor.Publish(new OrderStatusUpdatedEvent(@event.OrderId, "Order paid"));
            _commandProcessor.Send(_processManager.GetNext(@event));
            return base.Handle(@event);
        }
    }
}