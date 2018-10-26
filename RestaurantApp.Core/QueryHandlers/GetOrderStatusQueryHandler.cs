using Paramore.Darker;
using RestaurantApp.Core.Queries;

namespace RestaurantApp.Core.QueryHandlers
{
    public class GetOrderStatusQueryHandler : QueryHandler<GetOrderStatusQuery, string>
    {
        private readonly IOrdersRepository _ordersRepository;

        public GetOrderStatusQueryHandler(IOrdersRepository ordersRepository)
        {
            _ordersRepository = ordersRepository;
        }

        public override string Execute(GetOrderStatusQuery query)
        {
            return _ordersRepository.GetOrderStatus(query.OrderId);
        }
    }
}