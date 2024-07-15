
//Direct Exchange Example
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

//Topic Exchange Example
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
