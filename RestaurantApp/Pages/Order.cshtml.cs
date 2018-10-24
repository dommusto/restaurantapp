using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paramore.Brighter;
using Paramore.Darker;
using RestaurantApp.Core;
using RestaurantApp.Core.Commands;
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

        public OrderModel(IQueryProcessor queryProcessor, IAmACommandProcessor commandProcessor)
        {
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
