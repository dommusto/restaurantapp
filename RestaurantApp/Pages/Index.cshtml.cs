﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paramore.Brighter;
using Paramore.Darker;
using RestaurantApp.Core.Commands;
using RestaurantApp.Core.Queries;

namespace RestaurantApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IQueryProcessor _queryProcessor;
        private readonly IAmACommandProcessor _commandProcessor;
        public List<string> MenuItems { get; set; }
        [BindProperty]
        public string SelectedMenuItem { get; set; }
        
        public IndexModel(IQueryProcessor queryProcessor, IAmACommandProcessor commandProcessor)
        {
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
            _commandProcessor.Send(new PrepareOrderCommand(orderId));
            return RedirectToPage("Order", "OnGet", new { orderId });
        }
    }
}
