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
                Pay();
                _commandProcessor.Publish(new OrderPaidEvent(command.OrderId));
            });
            return base.Handle(command);
        }

        private static void Pay()
        {
            Thread.Sleep(2000);
        }
    }
}