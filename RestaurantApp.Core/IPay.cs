
using System;

namespace RestaurantApp.Core
{
    public interface IPay
    {
        void Pay(string orderId);
        event EventHandler OrderPaid;
    }
}