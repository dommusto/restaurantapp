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
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMenuItemsRepository _menuMenuItemsRepository;
        private readonly IAmACommandProcessor _commandProcessor;
        public List<string> MenuItems { get; set; }
        [BindProperty]
        public string SelectedMenuItem { get; set; }

        public IndexModel(IOrdersRepository ordersRepository, IMenuItemsRepository menuMenuItemsRepository, IAmACommandProcessor commandProcessor)
        {
            _ordersRepository = ordersRepository;
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
            var orderId = _ordersRepository.AddOrder(SelectedMenuItem);
            _commandProcessor.Send(new PrepareOrderCommand(orderId));
            return RedirectToPage("Order", "OnGet", new { orderId });
        }
    }
}
