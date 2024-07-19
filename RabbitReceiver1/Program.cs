
//Direct Exchange Example
/*
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Receiver App1";
connectionFactory.VirtualHost = "newVHost1";

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
*/

//Topic Exchange Example Receiver
/*
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
string queueName = "queue1";
channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);
channel.QueueDeclare(queueName, true, false, false, null);

// Bind queue to exchange with a routing key pattern
string routingPattern = "key.even"; // Will receive messages with "key.even" routing key
channel.QueueBind(queueName, exchangeName, routingPattern);

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
*/

// Header Exchange Example Receiver
/*
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Header Receiver App";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "header_exchange1";
string queueName = "header_queue1";
channel.ExchangeDeclare(exchangeName, ExchangeType.Headers);
channel.QueueDeclare(queueName, true, false, false, null);

// Bind queue to exchange with header properties
var headers = new Dictionary<string, object> { { "format", "pdf" }, { "x-match", "all" } };
channel.QueueBind(queueName, exchangeName, string.Empty, headers);

channel.BasicQos(0, 1, false);

EventingBasicConsumer consumer = new(channel);
consumer.Received += async (sender, args) =>
{
    byte[] body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"(R1)Processing message...: {message}");
    await Task.Delay(1000); // Simulate processing delay
    Console.WriteLine($"(R1)Message processed");

    channel.BasicAck(args.DeliveryTag, false);
};

var consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press any key to exit");
Console.ReadKey();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close();
*/

// Fanout Exchange Example Receiver
/*
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Fanout Receiver App";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "fanout_exchange1";
string queueName = "fanout_queue1";
channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);
channel.QueueDeclare(queueName, true, false, false, null);

// Bind queue to exchange without routing key
channel.QueueBind(queueName, exchangeName, string.Empty);

channel.BasicQos(0, 1, false);

EventingBasicConsumer consumer = new(channel);
consumer.Received += async (sender, args) =>
{
    byte[] body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"(R1)Processing message...: {message}");
    await Task.Delay(1000); // Simulate processing delay
    Console.WriteLine($"(R1)Message processed");

    channel.BasicAck(args.DeliveryTag, false);
};

var consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press any key to exit");
Console.ReadKey();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close();
*/
