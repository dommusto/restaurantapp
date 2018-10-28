using Paramore.Brighter;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.EventHandlers
{
    public class OrderAddedEventHandler : RequestHandler<OrderAddedEvent>
    {
        private readonly IAmACommandProcessor _commandProcessor;

        public OrderAddedEventHandler(IAmACommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public override OrderAddedEvent Handle(OrderAddedEvent @event)
        {
            _commandProcessor.Post(new PrepareOrderCommand(@event.OrderId));
            return base.Handle(@event);
        }
    }
}