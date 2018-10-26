using Paramore.Darker;

namespace RestaurantApp.Core.Queries
{
    public class GetOrderStatusQuery : IQuery<string>
    {
        public string OrderId;

        public GetOrderStatusQuery(string orderId)
        {
            OrderId = orderId;
        }
    }
}