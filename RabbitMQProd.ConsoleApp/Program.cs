using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace RabbitMQProd.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = factory.CreateConnection();

            using
            var channel = connection.CreateModel();
            channel.QueueDeclare("product", exclusive: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Product Mesajı Alındı : {message}");

            };



            channel.BasicConsume(queue: "product", autoAck: true, consumer: consumer);
            Console.ReadKey();
        }
    }
}
