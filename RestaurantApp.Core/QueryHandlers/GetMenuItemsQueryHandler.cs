
using System.Collections.Generic;
using Paramore.Darker;
using RestaurantApp.Core.Queries;

namespace RestaurantApp.Core.QueryHandlers
{
    public class GetMenuItemsQueryHandler : QueryHandler<GetMenuItemsQuery, IEnumerable<string>>
    {
        public override IEnumerable<string> Execute(GetMenuItemsQuery query)
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