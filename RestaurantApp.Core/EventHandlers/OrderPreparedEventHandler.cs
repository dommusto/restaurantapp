﻿using Paramore.Brighter;
using RestaurantApp.Core.Events;
using RestaurantApp.Core.ProcessManager;

namespace RestaurantApp.Core.EventHandlers
{
    public class OrderPreparedEventHandler : RequestHandler<OrderPreparedEvent>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IOrderProcessManager _processManager;

        public OrderPreparedEventHandler(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor, IOrderProcessManager processManager)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
            _processManager = processManager;
        }

        public override OrderPreparedEvent Handle(OrderPreparedEvent @event)
        {
            _ordersRepository.UpdateOrderStatus(@event.OrderId, "Food ready");
            _commandProcessor.Publish(new OrderStatusUpdatedEvent(@event.OrderId, "Food ready"));
            _commandProcessor.Send(_processManager.GetNext(@event));
            return base.Handle(@event);
        }
    }
}