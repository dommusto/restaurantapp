using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using Paramore.Darker;
using RestaurantApp.Core.Commands;
using RestaurantApp.Hubs;
using RestaurantApp.Core.Queries;

namespace RestaurantApp.Pages
{
    public class OrderModel : PageModel
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAmACommandProcessor _commandProcessor;

        [BindProperty]
        public string OrderId { get; set; }
        public string OrderStatus { get; set; }
        
        public OrderModel(IHubContext<PushHub> hubContext, IAmACommandProcessor commandProcessor, IQueryProcessor queryProcessor)
        {
            HubProvider.HubContext = hubContext;
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
        }
        
        public void OnGet(string orderId)
        {
            OrderId = orderId;
            OrderStatus = _queryProcessor.Execute(new GetOrderStatusQuery(orderId));
        }

        public void OnPost()
        {
            _commandProcessor.Send(new PayForOrderCommand(OrderId));
            OrderStatus = _queryProcessor.Execute(new GetOrderStatusQuery(OrderId));
        }
    }
}
