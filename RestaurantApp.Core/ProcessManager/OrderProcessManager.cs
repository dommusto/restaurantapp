using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.ProcessManager
{
    public class OrderProcessManager : IOrderProcessManager
    {
        public RequestPaymentCommand GetNext(OrderAddedEvent @event)
        {
            return new RequestPaymentCommand(@event.OrderId);
        }

        public PrepareOrderCommand GetNext(OrderPaidEvent @event)
        {
            return new PrepareOrderCommand(@event.OrderId);
        }

        public OrderCompletedEvent GetNext(OrderPreparedEvent @event)
        {
            return new OrderCompletedEvent(@event.OrderId);
        }
    }
}