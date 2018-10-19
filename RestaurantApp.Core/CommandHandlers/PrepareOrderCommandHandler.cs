using System;
using System.Threading;
using System.Threading.Tasks;
using Paramore.Brighter;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.CommandHandlers
{
    public class PrepareOrderCommandHandler : RequestHandler<PrepareOrderCommand>
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        public PrepareOrderCommandHandler(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
        }


        public override PrepareOrderCommand Handle(PrepareOrderCommand command)
        {
            throw new Exception("a");
            Task.Run(() =>
            {
                _ordersRepository.UpdateOrderStatus(command.OrderId, "Preparing food");
                PrepareOrder();
                _commandProcessor.Publish(new OrderPreparedEvent(command.OrderId));
            });
            return base.Handle(command);
        }

        private static void PrepareOrder()
        {
            Thread.Sleep(5000);
        }
    }
}