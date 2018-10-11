using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RestaurantApp.Core
{
    public class RestaurantService : IRestaurantService
    {
        private readonly Dictionary<string, string> _orderStatuses;

        public RestaurantService()
        {
            _orderStatuses = new Dictionary<string, string>();
        }

        public IEnumerable<string> GetItems()
        {
            return new List<string>()
            {
                "Steak",
                "Salad",
                "Pizza",
                "Ice cream"
            };
        }

        public string GetOrderStatus(string orderId)
        {
            return _orderStatuses[orderId];
        }

        public string AddOrder(string menuItem)
        {
            var orderId = Guid.NewGuid().ToString();
            _orderStatuses.Add(orderId, "Started");
            return orderId;
        }

        public void PrepareOrder(string orderId)
        {
            Task.Run(() =>
            {
                _orderStatuses[orderId] = "Preparing food";
                Thread.Sleep(2000);
                OrderPrepared?.Invoke(this, new EventArgs());
            });
        }

        public void Pay(string orderId)
        {
            _orderStatuses[orderId] = "Waiting to pay";
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                OrderPaid?.Invoke(this, new EventArgs());
            });
        }

        public event EventHandler OrderPrepared;
        public event EventHandler OrderPaid;
    }
}