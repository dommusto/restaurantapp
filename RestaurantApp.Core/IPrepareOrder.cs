using System;

namespace RestaurantApp.Core
{
    public interface IPrepareOrder
    {
        void PrepareOrder(string orderId);
        event EventHandler OrderPrepared;
    }
}