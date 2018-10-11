using System;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantApp.Core
{
    public class Cashier : IPay
    {
        private readonly IOrdersRepository _ordersRepository;

        public Cashier(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public void Pay(string orderId)
        {
            _ordersRepository.UpdateOrderStatus(orderId, "Waiting to pay");
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                OrderPaid?.Invoke(this, new EventArgs());
            });
        }

        public event EventHandler OrderPaid;
    }
}