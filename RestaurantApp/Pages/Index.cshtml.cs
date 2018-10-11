using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RestaurantApp.Core;

namespace RestaurantApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRestaurantService _restaurantService;
        public List<string> MenuItems { get; set; }
        [BindProperty]
        public string SelectedMenuItem { get; set; }

        public IndexModel(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
            MenuItems = new List<string>();
        }

        public void OnGet()
        {
            MenuItems.AddRange(_restaurantService.GetItems());
        }

        public IActionResult OnPost()
        {
            var orderId = _restaurantService.AddOrder(SelectedMenuItem);
            _restaurantService.PrepareOrder(orderId);
            return RedirectToPage("Order", "OnGet", new { orderId });
        }
    }
}
