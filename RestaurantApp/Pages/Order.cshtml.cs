﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paramore.Brighter;
using RestaurantApp.Core;
using RestaurantApp.Core.Commands;

namespace RestaurantApp.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        [BindProperty]
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }

        public OrderModel(IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor)
        {
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
        }
        
        public void OnGet(string orderId)
        {
            OrderId = orderId;
            OrderStatus = _ordersRepository.GetOrderStatus(orderId);
        }

        public void OnPost()
        {
            _commandProcessor.Send(new PayForOrderCommand(OrderId));
            OrderStatus = _ordersRepository.GetOrderStatus(OrderId);
        }
    }
}
