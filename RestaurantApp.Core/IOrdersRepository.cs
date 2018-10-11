namespace RestaurantApp.Core
{
    public interface IOrdersRepository
    {
        string AddOrder(string menuItem);
        string GetOrderStatus(string orderId);
        void UpdateOrderStatus(string orderId, string status);
    }
}