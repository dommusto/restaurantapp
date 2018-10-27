using Newtonsoft.Json;
using Paramore.Brighter;
using RestaurantApp.Core.Events;

namespace RestaurantApp.Core.Mappers
{
    public class OrderPickedUpByCookerEventMapper : IAmAMessageMapper<OrderPickedUpByCookerEvent>
    {
        public Message MapToMessage(OrderPickedUpByCookerEvent request)
        {
            var header = new MessageHeader(messageId: request.Id, topic: "OrderPickedUpByCookerEvent", messageType: MessageType.MT_EVENT);
            var body = new MessageBody(JsonConvert.SerializeObject(request));
            var message = new Message(header, body);
            return message;
        }

        public OrderPickedUpByCookerEvent MapToRequest(Message message)
        {
            return JsonConvert.DeserializeObject<OrderPickedUpByCookerEvent>(message.Body.Value);
        }
    }
}