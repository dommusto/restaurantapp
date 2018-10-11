using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using RestaurantApp.Core;
using RestaurantApp.Hubs;

namespace RestaurantApp.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IHubContext<PushHub> _hubContext;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IPay _cashier;
        private readonly IPrepareOrder _cook;

        [BindProperty]
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }

        public OrderModel(IHubContext<PushHub> hubContext, IOrdersRepository ordersRepository, IPay cashier, IPrepareOrder cook)
        {
            _hubContext = hubContext;
            _ordersRepository = ordersRepository;
            _cashier = cashier;
            _cook = cook;
            _cook.OrderPrepared += OrderPreparedEventHandler;
            _cashier.OrderPaid += OrderPaidEventHandler;
        }
        
        public void OnGet(string orderId)
        {
            OrderId = orderId;
            OrderStatus = _ordersRepository.GetOrderStatus(orderId);
        }

        public void OnPost()
        {
            _cashier.Pay(OrderId);
            OrderStatus = _ordersRepository.GetOrderStatus(OrderId);
        }

        private void OrderPreparedEventHandler(object sender, EventArgs e)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Food ready");
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "EnablePayButton");
        }

        private void OrderPaidEventHandler(object sender, EventArgs e)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "All done, enjoy!");
        }
    }
}
