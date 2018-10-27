using Newtonsoft.Json;
using Paramore.Brighter;
using RestaurantApp.Core.Commands;

namespace RestaurantApp.Core.Mappers
{
    public class PrepareOrderCommandMessageMapper : IAmAMessageMapper<PrepareOrderCommand>
    {
        public Message MapToMessage(PrepareOrderCommand request)
        {
            var header = new MessageHeader(messageId: request.Id, topic: "PrepareOrderCommand", messageType: MessageType.MT_EVENT);
            var body = new MessageBody(JsonConvert.SerializeObject(request));
            var message = new Message(header, body);
            return message;
        }

        public PrepareOrderCommand MapToRequest(Message message)
        {
            return JsonConvert.DeserializeObject<PrepareOrderCommand>(message.Body.Value);
        }
    }
}