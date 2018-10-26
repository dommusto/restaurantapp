using System.Threading;
using System.Threading.Tasks;
using Paramore.Brighter;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.CommandHandlers
{
    public class PrepareOrderCommandHandler : RequestHandler<PrepareOrderCommand>
    {
        private readonly IAmACommandProcessor _commandProcessor;

        public PrepareOrderCommandHandler(IAmACommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        public override PrepareOrderCommand Handle(PrepareOrderCommand command)
        {
            Task.Run(() =>
            {
                _commandProcessor.Publish(new OrderPickedUpByCookerEvent(command.OrderId));
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