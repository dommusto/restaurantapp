using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Paramore.Brighter;
using RestaurantApp.Core;
using RestaurantApp.Core.Commands;

namespace RestaurantApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMenuItemsRepository _menuMenuItemsRepository;
        private readonly IAmACommandProcessor _commandProcessor;
        public List<string> MenuItems { get; set; }
        [BindProperty]
        public string SelectedMenuItem { get; set; }

        public IndexModel(IMenuItemsRepository menuMenuItemsRepository, IAmACommandProcessor commandProcessor)
        {
            _menuMenuItemsRepository = menuMenuItemsRepository;
            _commandProcessor = commandProcessor;
            MenuItems = new List<string>();
        }

        public void OnGet()
        {
            MenuItems.AddRange(_menuMenuItemsRepository.GetItems());
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
