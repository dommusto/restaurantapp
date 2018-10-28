using Paramore.Brighter;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core
{
    public class OrderProcessManager : IHandleRequests<OrderAddedEvent>,
                                       IHandleRequests<OrderPaidEvent>,
                                       IHandleRequests<OrderPreparedEvent>
    {
        private readonly IAmACommandProcessor _commandProcessor;

        public OrderProcessManager(IAmACommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public OrderAddedEvent Handle(OrderAddedEvent @event)
        {
            _commandProcessor.Send(new RequestPaymentCommand(@event.OrderId));
            return @event;
        }

        public OrderPaidEvent Handle(OrderPaidEvent @event)
        {
            _commandProcessor.Post(new PrepareOrderCommand(@event.OrderId));
            return @event;
        }

        public OrderPreparedEvent Handle(OrderPreparedEvent @event)
        {
            _commandProcessor.Publish(new OrderCompletedEvent(@event.OrderId));
            return @event;
        }


        #region brighter

        public OrderAddedEvent Fallback(OrderAddedEvent request)
        {
            return request;
        }

        public void SetSuccessor(IHandleRequests<OrderAddedEvent> successor)
        {
        }

        public void DescribePath(IAmAPipelineTracer pathExplorer)
        {
        }

        public void InitializeFromAttributeParams(params object[] initializerList)
        {
        }

        public void AddToLifetime(IAmALifetime instanceScope)
        {
        }

        public OrderPaidEvent Fallback(OrderPaidEvent request)
        {
            return request;
        }

        public void SetSuccessor(IHandleRequests<OrderPaidEvent> successor)
        {
        }

        public OrderPreparedEvent Fallback(OrderPreparedEvent request)
        {
            return @request;
        }

        public void SetSuccessor(IHandleRequests<OrderPreparedEvent> successor)
        {
        }

        public IRequestContext Context { get; set; }
        public HandlerName Name { get; }

        #endregion
    }
}