using System;
using System.Reflection;
using Paramore.Brighter;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;
using RestaurantApp.Core.ProcessManager;

namespace RestaurantApp.Core.EventHandlers
{
    public class OrderAddedEventHandler : RequestHandler<OrderAddedEvent>
    {
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IOrderProcessManager _processManager;

        public OrderAddedEventHandler(IAmACommandProcessor commandProcessor, IOrderProcessManager processManager)
        {
            _commandProcessor = commandProcessor;
            _processManager = processManager;
        }

        public override OrderAddedEvent Handle(OrderAddedEvent @event)
        {
            _commandProcessor.Send(_processManager.GetNext(@event));
            return base.Handle(@event);
        }
    }
}