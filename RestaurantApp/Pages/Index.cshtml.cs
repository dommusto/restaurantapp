using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantApp.Core;

namespace RestaurantApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IPrepareOrder _cook;
        private readonly IMenuItemsRepository _menuMenuItemsRepository;
        public List<string> MenuItems { get; set; }
        [BindProperty]
        public string SelectedMenuItem { get; set; }

        public IndexModel(IOrdersRepository ordersRepository, IPrepareOrder cook, IMenuItemsRepository menuMenuItemsRepository)
        {
            _ordersRepository = ordersRepository;
            _cook = cook;
            _menuMenuItemsRepository = menuMenuItemsRepository;
            MenuItems = new List<string>();
        }

        public void OnGet()
        {
            MenuItems.AddRange(_menuMenuItemsRepository.GetItems());
        }

        public IActionResult OnPost()
        {
            var orderId = _ordersRepository.AddOrder(SelectedMenuItem);
            _cook.PrepareOrder(orderId);
            return RedirectToPage("Order", "OnGet", new { orderId });
        }
    }
}
