using Paramore.Brighter;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.CommandHandlers
{
    public class RequestPaymentCommandHandler : RequestHandler<RequestPaymentCommand>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        public RequestPaymentCommandHandler(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
        }

        public override RequestPaymentCommand Handle(RequestPaymentCommand command)
        {
            _ordersRepository.UpdateOrderStatus(command.OrderId, "Payment requested");
            _commandProcessor.Publish(new OrderStatusUpdatedEvent(command.OrderId, "Payment requested"));
            _commandProcessor.Publish(new PaymentRequestedEvent(command.OrderId));
            return base.Handle(command);
        }
    }
}