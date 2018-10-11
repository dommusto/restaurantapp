using System.Collections.Generic;

namespace RestaurantApp.Core
{
    public class MenuItemsRepository : IMenuItemsRepository
    {
        public IEnumerable<string> GetItems()
        {
            return new List<string>()
            {
                "Steak",
                "Salad",
                "Pizza",
                "Ice cream"
            };
        }
    }
}