using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantApp.Core
{
    public class Cook : IPrepareOrder
    {
        private readonly IOrdersRepository _ordersRepository;

        public Cook(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public void PrepareOrder(string orderId)
        {
            Task.Run(() =>
            {
                _ordersRepository.UpdateOrderStatus(orderId, "Preparing food");
                Thread.Sleep(2000);
                OrderPrepared?.Invoke(this, new EventArgs());
            });
        }

        public event EventHandler OrderPrepared;
    }
}