﻿using System;
using System.Collections.Generic;

namespace RestaurantApp.Core
{
    public interface IRestaurantService
    {
        IEnumerable<string> GetItems();
        string AddOrder(string menuItem);
        void PrepareOrder(string orderId);
        void Pay(string orderId);
        event EventHandler OrderPrepared;
        event EventHandler OrderPaid;
        string GetOrderStatus(string orderId);
    }
}
