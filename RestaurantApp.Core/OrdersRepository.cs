using System;
using System.Collections.Generic;

namespace RestaurantApp.Core
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly Dictionary<string, string> _orderStatuses;
        private Guid id;

        public OrdersRepository()
        {
            id = Guid.NewGuid();
            _orderStatuses = new Dictionary<string, string>();
        }

        public string AddOrder(string menuItem)
        {
            var orderId = Guid.NewGuid().ToString();
            _orderStatuses.Add(orderId, "Started");
            return orderId;
        }

        public string GetOrderStatus(string orderId)
        {
            return _orderStatuses[orderId];
        }

        public void UpdateOrderStatus(string orderId, string status)
        {
            _orderStatuses[orderId] = status;
        }
    }
}