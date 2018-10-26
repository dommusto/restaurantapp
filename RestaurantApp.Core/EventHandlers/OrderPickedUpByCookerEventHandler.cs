using Paramore.Brighter;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.EventHandlers
{
    public class OrderPickedUpByCookerEventHandler : RequestHandler<OrderPickedUpByCookerEvent>
    {
        private readonly IOrdersRepository _ordersRepository;

        public OrderPickedUpByCookerEventHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public override OrderPickedUpByCookerEvent Handle(OrderPickedUpByCookerEvent @event)
        {
            _ordersRepository.UpdateOrderStatus(@event.OrderId, "Preparing food");
            return base.Handle(@event);
        }
    }
}