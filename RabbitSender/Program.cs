
//Direct Exchange Example Sender
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Sender App";
connectionFactory.VirtualHost = "newVHost1";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string queueName = "queue1";
string exchangeName = "exchange1";
string routingName = "routingKey1";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, true, false, false,null);
channel.QueueBind(queueName, exchangeName, routingName,null);

for(int i = 1; i <= 30; i++)
{
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
    channel.BasicPublish(exchangeName, routingName, null, messageBodyBytes);
    await Task.Delay(200);
}

channel.Close();
connection.Close();
*/

//Topic Exchange Example Sender
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Sender App";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "exchange1";
channel.ExchangeDeclare(exchangeName, ExchangeType.Topic);

for (int i = 1; i <= 30; i++)
{
    string routingKey = i % 2 == 0 ? "key.even" : "key.odd"; // Routing key for message
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
    channel.BasicPublish(exchangeName, routingKey, null, messageBodyBytes);
    await Task.Delay(200);
}

channel.Close();
connection.Close();
*/

// Header Exchange Example Sender
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Header Sender App";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "header_exchange1";
channel.ExchangeDeclare(exchangeName, ExchangeType.Headers);

for (int i = 1; i <= 10; i++)
{
    var properties = channel.CreateBasicProperties();
    properties.Headers = new Dictionary<string, object>();

    if (i % 2 == 0)
        properties.Headers.Add("format", "pdf");
    else
        properties.Headers.Add("format", "doc");

    string message = $"Message #{i}";
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchangeName, string.Empty, properties, messageBodyBytes);
    await Task.Delay(200);
}

channel.Close();
connection.Close();
*/

// Fanout Exchange Example Sender
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Fanout Sender App";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "fanout_exchange1";
channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout);

for (int i = 1; i <= 10; i++)
{
    string message = $"Message #{i}";
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchangeName, string.Empty, null, messageBodyBytes);
    await Task.Delay(200);
}

channel.Close();
connection.Close();
*/

// Priority Queue Example - Queue Declaration and Message Publishing
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Priority Publisher";
connectionFactory.VirtualHost = "newVHost1";

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();

string queueName = "task_priority_queue";

channel.QueueDeclare(queue: queueName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: new Dictionary<string, object> { { "x-max-priority", 10 } });

for (int i = 1; i <= 5; i++)
{
    var properties = channel.CreateBasicProperties();
    properties.Priority = (byte)i; // Set priority (1 to 5)

    string message = $"Task with priority {i}";
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish(exchange: "",
                         routingKey: queueName,
                         basicProperties: properties,
                         body: messageBodyBytes);
    Console.WriteLine($"Sent: {message}");
}

Console.WriteLine("All messages sent.");
*/

// Different Channel Acknowledgment Example Sender
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Sender App";
connectionFactory.VirtualHost = "newVHost1";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string queueName = "queue1";
string exchangeName = "exchange1";
string routingName = "routingKey1";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, true, false, false, null);
channel.QueueBind(queueName, exchangeName, routingName, null);

for (int i = 1; i <= 30; i++)
{
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
    channel.BasicPublish(exchangeName, routingName, null, messageBodyBytes);
    await Task.Delay(200);
}

channel.Close();
connection.Close();
*/

// Manual Batch Acknowledgment Example Sender
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Sender App";
connectionFactory.VirtualHost = "newVHost1";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string queueName = "queue1";
string exchangeName = "exchange1";
string routingName = "routingKey1";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, true, false, false, null);
channel.QueueBind(queueName, exchangeName, routingName, null);

for (int i = 1; i <= 20; i++)
{
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
    channel.BasicPublish(exchangeName, routingName, null, messageBodyBytes);
    await Task.Delay(200);
}

channel.Close();
connection.Close();
*/

// Requeue Example Sender
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Sender App";
connectionFactory.VirtualHost = "newVHost1";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string queueName = "queue1";
string exchangeName = "exchange1";
string routingName = "routingKey1";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, true, false, false, null);
channel.QueueBind(queueName, exchangeName, routingName, null);

for (int i = 1; i <= 30; i++)
{
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
    channel.BasicPublish(exchangeName, routingName, null, messageBodyBytes);
    await Task.Delay(200);
}

channel.Close();
connection.Close();
*/

//Publisher Confirms Guaranteed delivery example Sender
/*
using RabbitMQ.Client;
using System.Text;

string userName = "guest", password = "guest", queueMessagesPort = "5672";
ConnectionFactory connectionFactory = new();
connectionFactory.Uri = new Uri($"amqp://{userName}:{password}@localhost:{queueMessagesPort}");
connectionFactory.ClientProvidedName = "RabbitMQ Sender App";
connectionFactory.VirtualHost = "newVHost1";

IConnection connection = connectionFactory.CreateConnection();
IModel channel = connection.CreateModel();

string queueName = "queue1";
string exchangeName = "exchange1";
string routingName = "routingKey1";

channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, true, false, false, null);
channel.QueueBind(queueName, exchangeName, routingName, null);

channel.ConfirmSelect(); // Enable publisher confirms

for (int i = 1; i <= 30; i++)
{
    byte[] messageBodyBytes = Encoding.UTF8.GetBytes($"Message #{i}");
    var properties = channel.CreateBasicProperties();
    properties.Persistent = true; // Mark message as persistent

    channel.BasicPublish(exchangeName, routingName, properties, messageBodyBytes);
    if (!channel.WaitForConfirms())
    {
        Console.WriteLine($"Message #{i} was not confirmed by the broker.");
    }
    await Task.Delay(200);
}

channel.Close();
connection.Close();
*/

