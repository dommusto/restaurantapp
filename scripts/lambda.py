var ORDER_PREPARED_EVENT_QUEUE_URL = 'https://sqs.us-west-2.amazonaws.com/058052576087/OrderPreparedEvent';
var ORDER_PICKED_UP_BY_COOKER_EVENT_QUEUE_URL = 'https://sqs.us-west-2.amazonaws.com/058052576087/OrderPickedUpByCookerEvent';
var AWS = require('aws-sdk');
var sqs = new AWS.SQS({region : 'us-west-2'});

exports.handler = function(event, context) {
  
  var order_picked_up_by_cooker_event ={
	  "Type" : "Notification",
	  "MessageId" : "73b61bd8-b445-5fb6-bba0-4f9cd61bd627",
	  "TopicArn" : "arn:aws:lambda:us-west-2:058052576087:function:OrderPickedUpByCookerEvent",
	  "Message" : "{\"Header\":{\"TimeStamp\":\"2018-10-19T22:52:12Z\",\"Id\":\"90f96b0f-1f9f-4e2a-ac92-08769b38d73c\",\"Topic\":\"OrderPickedUpByCookerEvent\",\"MessageType\":2,\"Bag\":{},\"HandledCount\":0,\"DelayedMilliseconds\":0,\"CorrelationId\":\"00000000-0000-0000-0000-000000000000\",\"ContentType\":\"text/plain\",\"ReplyTo\":null},\"Body\":{\"Bytes\":\"eyJPcmRlcklkIjoiNzMyMTAwZWYtZDQyNy00YjQ4LWFiZjUtOTJkYjFmODdlODhhIiwiSWQiOiI5MGY5NmIwZi0xZjlmLTRlMmEtYWM5Mi0wODc2OWIzOGQ3M2MifQ==\",\"BodyType\":\"JSON\",\"Value\":\"{\\\"OrderId\\\":\\\"732100ef-d427-4b48-abf5-92db1f87e88a\\\",\\\"Id\\\":\\\"90f96b0f-1f9f-4e2a-ac92-08769b38d73c\\\"}\",\"PostBack\":null},\"Id\":\"90f96b0f-1f9f-4e2a-ac92-08769b38d73c\",\"DeliveryTag\":0,\"Redelivered\":false}",
	  "Timestamp" : "2018-10-19T22:52:14.199Z",
	  "SignatureVersion" : "1",
	  "Signature" : "N2S5XqRX1bxX5JsM7HuUSOCjFREHPtQ2ERnFweyOFgJO9aA9aUAVuZ7raqv7+zJjtb3rhTlY9b4GUJRbF/vabMgeqLkUMK3ZD/vMMoNxywSnIW9UmOWbCO6bfnPSrbwBz7Ve2UJUzB2YGdZNeLIKbBrb173vCHiIIv69PendhZazrTmbaHFCQNgbpBDywAZ0RGED0h7GawitSJ7QZMizJf9SbzNsbBK7Dy7Lm1Xtmyg3lykDtPOIBoF9GZNp0mbLXPLYmSn6Mne9OFzqv3v0K7bNKL91oqn5unDcDSJe7Xyr1nghaSUijnxOwD2+OS6ovfflhee2NbeWUEtAfecfMA==",
	  "SigningCertURL" : "https://sns.us-west-2.amazonaws.com/SimpleNotificationService-ac565b8b1a6c5d002d285f9598aa1d9b.pem",
	  "UnsubscribeURL" : "https://sns.us-west-2.amazonaws.com/?Action=Unsubscribe&SubscriptionArn=arn:aws:sns:us-west-2:058052576087:testtopic:2e4810a2-dd06-4d69-8b64-1ad67c80aade"
	};
	
  var order_picked_up_by_cooker_event_json = JSON.stringify(order_picked_up_by_cooker_event);
  var params = {
    MessageBody: order_picked_up_by_cooker_event_json,
    QueueUrl: ORDER_PICKED_UP_BY_COOKER_EVENT_QUEUE_URL
  };
  console.log('sending OrderPickedUpByCookerEvent');
  sqs.sendMessage(params, function(err,data){
    if(err) {
      console.log('error:',"Fail Send Message" + err);
      context.done('error', "ERROR Put SQS");  // ERROR with message
    }else{
      console.log('successfully sent OrderPickedUpByCookerEvent message to queue')
      console.log('data:',data.MessageId);
      context.done(null,'');  // SUCCESS 
    }
  });
  
  var order_prepared_event ={
	  "Type" : "Notification",
	  "MessageId" : "73b61bd8-b445-5fb6-bba0-4f9cd61bd627",
	  "TopicArn" : "arn:aws:lambda:us-west-2:058052576087:function:PrepareOrderCommandHandler",
	  "Message" : "{\"Header\":{\"TimeStamp\":\"2018-10-19T22:52:12Z\",\"Id\":\"90f96b0f-1f9f-4e2a-ac92-08769b38d73c\",\"Topic\":\"OrderPreparedEvent\",\"MessageType\":2,\"Bag\":{},\"HandledCount\":0,\"DelayedMilliseconds\":0,\"CorrelationId\":\"00000000-0000-0000-0000-000000000000\",\"ContentType\":\"text/plain\",\"ReplyTo\":null},\"Body\":{\"Bytes\":\"eyJPcmRlcklkIjoiNzMyMTAwZWYtZDQyNy00YjQ4LWFiZjUtOTJkYjFmODdlODhhIiwiSWQiOiI5MGY5NmIwZi0xZjlmLTRlMmEtYWM5Mi0wODc2OWIzOGQ3M2MifQ==\",\"BodyType\":\"JSON\",\"Value\":\"{\\\"OrderId\\\":\\\"732100ef-d427-4b48-abf5-92db1f87e88a\\\",\\\"Id\\\":\\\"90f96b0f-1f9f-4e2a-ac92-08769b38d73c\\\"}\",\"PostBack\":null},\"Id\":\"90f96b0f-1f9f-4e2a-ac92-08769b38d73c\",\"DeliveryTag\":0,\"Redelivered\":false}",
	  "Timestamp" : "2018-10-19T22:52:14.199Z",
	  "SignatureVersion" : "1",
	  "Signature" : "N2S5XqRX1bxX5JsM7HuUSOCjFREHPtQ2ERnFweyOFgJO9aA9aUAVuZ7raqv7+zJjtb3rhTlY9b4GUJRbF/vabMgeqLkUMK3ZD/vMMoNxywSnIW9UmOWbCO6bfnPSrbwBz7Ve2UJUzB2YGdZNeLIKbBrb173vCHiIIv69PendhZazrTmbaHFCQNgbpBDywAZ0RGED0h7GawitSJ7QZMizJf9SbzNsbBK7Dy7Lm1Xtmyg3lykDtPOIBoF9GZNp0mbLXPLYmSn6Mne9OFzqv3v0K7bNKL91oqn5unDcDSJe7Xyr1nghaSUijnxOwD2+OS6ovfflhee2NbeWUEtAfecfMA==",
	  "SigningCertURL" : "https://sns.us-west-2.amazonaws.com/SimpleNotificationService-ac565b8b1a6c5d002d285f9598aa1d9b.pem",
	  "UnsubscribeURL" : "https://sns.us-west-2.amazonaws.com/?Action=Unsubscribe&SubscriptionArn=arn:aws:sns:us-west-2:058052576087:testtopic:2e4810a2-dd06-4d69-8b64-1ad67c80aade"
	};
  
  var order_prepared_event_json = JSON.stringify(order_prepared_event);
  var params = {
    MessageBody: order_prepared_event_json,
    QueueUrl: ORDER_PREPARED_EVENT_QUEUE_URL
  };
  console.log('sending OrderPreparedEvent');
  sqs.sendMessage(params, function(err,data){
    if(err) {
      console.log('error:',"Fail Send Message" + err);
      context.done('error', "ERROR Put SQS");  // ERROR with message
    }else{
      console.log('successfully sent OrderPreparedEvent message to queue')
      console.log('data:',data.MessageId);
      context.done(null,'');  // SUCCESS 
    }
  });
}