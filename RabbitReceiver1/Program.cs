using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Receiver App1";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();
string exchangeName = "exchange1";
string routingName = "routingKey1";
string queueName = "queue1";
channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, true, false, false, null);
channel.QueueBind(queueName, exchangeName, routingName, null);

channel.BasicQos(0, 1, false);

EventingBasicConsumer consumer = new(channel);
consumer.Received += async (sender, args) =>
{
    byte[] body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"(R1)Processing message...: {message}");
    await Task.Delay(1000);
    Console.WriteLine($"(R1)Message processed");

    channel.BasicAck(args.DeliveryTag, false);
};

var consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press any key to exit");
Console.ReadKey();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close();