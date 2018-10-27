using System;
using Paramore.Brighter;

namespace RestaurantApp
{
    public class SimpleMessageMapperFactory : IAmAMessageMapperFactory
    {
        private readonly IServiceProvider _container;

        public SimpleMessageMapperFactory(IServiceProvider container)
        {
            _container = container;
        }

        public IAmAMessageMapper Create(Type messageMapperType)
        {
            return (IAmAMessageMapper)_container.GetService(messageMapperType);
        }
    }
}