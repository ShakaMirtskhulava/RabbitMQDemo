
//Direct Exchange Example Sender
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

// Priority Queue Example - Message Consumption
/*
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Priority Consumer";
connectionFactory.VirtualHost = "newVHost1";

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "task_priority_queue";

channel.QueueDeclare(queue: queueName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: new Dictionary<string, object> { { "x-max-priority", 10 } });

EventingBasicConsumer consumer = new(channel);
consumer.Received += (sender, args) =>
{
    byte[] body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Received: {message}");

    channel.BasicAck(args.DeliveryTag, false);
};

channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();
*/

// Different Channel Acknowledgment Example Receiver
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
IModel channel1 = connection.CreateModel();
IModel channel2 = connection.CreateModel();

string exchangeName = "exchange1";
string routingName = "routingKey1";
string queueName = "queue1";
channel1.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel1.QueueDeclare(queueName, true, false, false, null);
channel1.QueueBind(queueName, exchangeName, routingName, null);

channel1.BasicQos(0, 1, false);

EventingBasicConsumer consumer = new(channel1);
consumer.Received += async (sender, args) =>
{
    byte[] body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"(R1)Processing message...: {message}");
    await Task.Delay(1000);
    Console.WriteLine($"(R1)Message processed");

    // Attempt to acknowledge the message on a different channel
    try
    {
        channel2.BasicAck(args.DeliveryTag, false);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
    }
};

var consumerTag = channel1.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press any key to exit");
Console.ReadKey();

channel1.BasicCancel(consumerTag);
channel1.Close();
channel2.Close();
connection.Close();
*/

// Manual Batch Acknowledgment Example Receiver
/*
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Receiver App1";
connectionFactory.VirtualHost = "newVHost1";

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

string exchangeName = "exchange1";
string routingName = "routingKey1";
string queueName = "queue1";
channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, true, false, false, null);
channel.QueueBind(queueName, exchangeName, routingName, null);  

channel.BasicQos(0, 4, false);

EventingBasicConsumer consumer = new(channel);
consumer.Received += async (sender, args) =>
{
    byte[] body = args.Body.ToArray();
    string message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Processing message...: {message}");
    await Task.Delay(1000);
    Console.WriteLine($"Message processed");
    
    if(args.DeliveryTag >= 4)
        channel.BasicAck(args.DeliveryTag, true);
};

var consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press any key to exit");
Console.ReadKey();
//channel.BasicCancel(consumerTag);
*/

//Requeue Example Receiver1
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
    try
    {
        await Task.Delay(1000);
        Console.WriteLine($"(R1)Message processed");
        channel.BasicAck(args.DeliveryTag, false);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"(R1)Error processing message: {ex.Message}");
        channel.BasicNack(args.DeliveryTag, false, true); // Requeue the message
    }
};

var consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press any key to exit");
Console.ReadKey();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close();
*/

//Publisher Confirms Guaranteed delivery example Receiver
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
    try
    {
        await Task.Delay(1000);
        Console.WriteLine($"(R1)Message processed");
        channel.BasicAck(args.DeliveryTag, false);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"(R1)Error processing message: {ex.Message}");
        channel.BasicNack(args.DeliveryTag, false, true); // Requeue the message
    }
};

var consumerTag = channel.BasicConsume(queueName, false, consumer);

Console.WriteLine("Press any key to exit");
Console.ReadKey();

channel.BasicCancel(consumerTag);
channel.Close();
connection.Close();
*/
