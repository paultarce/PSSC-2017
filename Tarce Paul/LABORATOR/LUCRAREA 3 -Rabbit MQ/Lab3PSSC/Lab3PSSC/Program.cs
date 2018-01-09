using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Lab3PSSC
{
    class Program
    {

    }
    class Send
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);
                /*
                                for (int i = 0; i < 5; i++)
                                {
                                    channel.BasicPublish(exchange: "",
                                                         routingKey: "Queue2",
                                                         basicProperties: null,
                                                         body: body);

                                    Console.WriteLine(" [x] Sent {0}", message);
                                }
                                */
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body2 = ea.Body;
                    var message2 = Encoding.UTF8.GetString(body2);
                    Console.WriteLine(" [x] Received {0}", message2);
                };
                channel.BasicConsume(queue: "Queue2",
                                     noAck: true,
                                     consumer: consumer);

                int x = 0;
                while (x == 0)
                {
                    Console.WriteLine("1.Introducere mesaj");
                    Console.WriteLine("2.Exit");
                    Console.WriteLine("Optiune");
                    int opt = 2;
                    try
                    {
                        opt = int.Parse(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Introduceti o valoare valida");
                    }


                    switch (opt)
                    {
                        case 1:
                            Console.WriteLine("Introduceti mesajul:");
                            var mesaj = Console.ReadLine();
                            body = Encoding.UTF8.GetBytes(mesaj);

                            channel.BasicPublish(exchange: "",
                                         routingKey: "Queue1",
                                         basicProperties: null,
                                         body: body);

                            Console.WriteLine(" [x] Sent {0}", mesaj);
                            break;
                        case 2:
                            x = 1;
                            break;

                    }
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}

  /*  class Receive
    {
        public static void Main()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "Queue1",
                                     noAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }

        }
  
    */