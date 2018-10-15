using System.Threading;
using System.Threading.Tasks;
using Paramore.Brighter;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.CommandHandlers
{
    public class PayForOrderCommandHandler : RequestHandler<PayForOrderCommand>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        public PayForOrderCommandHandler(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
        }

        public override PayForOrderCommand Handle(PayForOrderCommand command)
        {
            _ordersRepository.UpdateOrderStatus(command.OrderId, "Waiting to pay");
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                _commandProcessor.Publish(new OrderPaidEvent(command.OrderId));
            });
            return base.Handle(command);
        }
    }
}