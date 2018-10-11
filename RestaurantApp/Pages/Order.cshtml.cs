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
        private readonly IRestaurantService _restaurantService;

        [BindProperty]
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }

        public OrderModel(IHubContext<PushHub> hubContext, IRestaurantService restaurantService)
        {
            _hubContext = hubContext;
            _restaurantService = restaurantService;
            _restaurantService.OrderPrepared += OrderPreparedEventHandler;
            _restaurantService.OrderPaid += OrderPaidEventHandler;
        }
        
        public void OnGet(string orderId)
        {
            OrderId = orderId;
            OrderStatus = _restaurantService.GetOrderStatus(orderId);
        }

        public void OnPost()
        {
            _restaurantService.Pay(OrderId);
            OrderStatus = _restaurantService.GetOrderStatus(OrderId);
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
