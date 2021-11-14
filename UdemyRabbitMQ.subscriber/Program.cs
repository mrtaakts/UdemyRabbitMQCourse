using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

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

            channel.BasicQos(0, 1,false);

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume("hello-queue",false,consumer);
            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(200);
                Console.WriteLine("Gelen mesaj: " + message);

                channel.BasicAck(e.DeliveryTag, false);
            };

            Console.ReadLine();
        }

        
    }
}
