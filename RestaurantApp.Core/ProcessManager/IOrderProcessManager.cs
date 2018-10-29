using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.ProcessManager
{
    public interface IOrderProcessManager
    {
        RequestPaymentCommand GetNext(OrderAddedEvent @event);
        PrepareOrderCommand GetNext(OrderPaidEvent @event);
        OrderCompletedEvent GetNext(OrderPreparedEvent @event);
    }
}