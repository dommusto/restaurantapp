using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Paramore.Brighter;
using Paramore.Darker;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Queries;
using RestaurantApp.Hubs;

namespace RestaurantApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAmACommandProcessor _commandProcessor;
        public List<string> MenuItems { get; set; }
        [BindProperty]
        public string SelectedMenuItem { get; set; }
        
        public IndexModel(IHubContext<PushHub> hubContext, IQueryProcessor queryProcessor, IAmACommandProcessor commandProcessor)
        {
            HubProvider.HubContext = hubContext;
            _queryProcessor = queryProcessor;
            _commandProcessor = commandProcessor;
            MenuItems = new List<string>();
        }

        public void OnGet()
        {
            MenuItems.AddRange(_queryProcessor.Execute(new GetMenuItemsQuery()));
        }

        public IActionResult OnPost()
        {
            var addOrderCommand = new AddOrderCommand(SelectedMenuItem);
             _commandProcessor.Send(addOrderCommand);
            var orderId = addOrderCommand.OrderId;
            return RedirectToPage("Order", "OnGet", new { orderId });
        }
    }
}
