using Newtonsoft.Json;
using Paramore.Brighter;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.Mappers
{
    public class OrderPreparentEventMapper : IAmAMessageMapper<OrderPreparedEvent>
    {
        public Message MapToMessage(OrderPreparedEvent request)
        {
            var header = new MessageHeader(messageId: request.Id, topic: "OrderPreparedEvent", messageType: MessageType.MT_EVENT);
            var body = new MessageBody(JsonConvert.SerializeObject(request));
            var message = new Message(header, body);
            return message;
        }

        public OrderPreparedEvent MapToRequest(Message message)
        {
            return JsonConvert.DeserializeObject<OrderPreparedEvent>(message.Body.Value);
        }
    }
}