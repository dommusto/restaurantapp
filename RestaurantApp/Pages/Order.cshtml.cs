using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using RestaurantApp.Core;
using RestaurantApp.Core.Commands;
using RestaurantApp.Hubs;

namespace RestaurantApp.Pages
{
    public static class stuff
    {
        public static IHubContext<PushHub> hubcontext { get; set; }
    }

    public class OrderModel : PageModel
    {
        private readonly IHubContext<PushHub> _hubContext;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IAmACommandProcessor _commandProcessor;

        [BindProperty]
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }

        public OrderModel(IHubContext<PushHub> hubContext, IOrdersRepository ordersRepository, IAmACommandProcessor commandProcessor)
        {
            _hubContext = hubContext;
            stuff.hubcontext = hubContext;
            _ordersRepository = ordersRepository;
            _commandProcessor = commandProcessor;
        }
        
        public void OnGet(string orderId)
        {
            OrderId = orderId;
            OrderStatus = _ordersRepository.GetOrderStatus(orderId);
            Task.Run(() =>
            {
                Thread.Sleep(3000);
                _hubContext.Clients.All.SendAsync("ReceiveMessage", "test");
            });
        }

        public void OnPost()
        {
            _commandProcessor.Send(new PayForOrderCommand(OrderId));
            OrderStatus = _ordersRepository.GetOrderStatus(OrderId);
        }

        /*private void OrderPreparedEventHandler(object sender, EventArgs e)
        {
            //orderpreparedeventhandler
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "Food ready");
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "EnablePayButton");
        }*/

        private void OrderPaidEventHandler(object sender, EventArgs e)
        {
            //orderpaideventhandler
            _hubContext.Clients.All.SendAsync("ReceiveMessage", "All done, enjoy!");
        }
    }
}
