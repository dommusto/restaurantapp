using System;

namespace RestaurantApp.Core
{
    public class OrdersRepository : IOrdersRepository
    {
        private string _orderStatus = "";
        private Guid id;

        public OrdersRepository()
        {
            id = Guid.NewGuid();
        }

        public string AddOrder(string menuItem)
        {
            lock (_orderStatus)
            {
                var orderId = Guid.NewGuid().ToString();
                _orderStatus = "Started";
                return orderId;
            }
        }

        public string GetOrderStatus(string orderId)
        {
            return _orderStatus;
        }

        public void UpdateOrderStatus(string orderId, string status)
        {
            lock (_orderStatus)
            {
                _orderStatus = status;
            }
        }
    }
}