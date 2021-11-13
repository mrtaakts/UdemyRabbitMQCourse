using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace UdemyRabbitMQ.subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            /*channel.QueueDeclare("hello-queue", true, false, false); 
             
            publisher eğer kuyruğu oluşturmadıysa diye tekrar oluşturma*/

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue",true,consumer);
            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Console.WriteLine("Gelen mesaj: " + message);
            };

            Console.ReadLine();
        }

        
    }
}
