using System.Collections.Generic;

namespace RestaurantApp.Core
{
    public interface IMenuItemsRepository
    {
        IEnumerable<string> GetItems();
    }
}