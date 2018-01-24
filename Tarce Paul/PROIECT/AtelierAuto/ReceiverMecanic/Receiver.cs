using System;
using AtelierAuto.Commands;
using AtelierAuto.Controllers;
using AtelierAuto.Models;
using AtelierAuto.Models.Generic;
using AtelierAuto.Repository;
using AtelierAuto.Evenimente;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;

namespace ReceiverMecanic
{
    class Receiver
    {
        static void Main(string[] args)
        {
            MagistralaCommands.Instance.Value.InregistreazaProcesatoareStandard();
            MagistralaEvenimente.Instanta.Value.InregistreazaProcesatoareStandard();
            MagistralaEvenimente.Instanta.Value.InchideInregistrarea();
            List<Eveniment> eve = new List<Eveniment>();

            var writeRepo = new WriteRepository();
            var readRepo = new ReadRepository();



            //var commandPlasareComanda = new CommandPlasareComanda();
            //MagistralaCommands.Instance.Value.Trimite(commandPlasareComanda);
            //AtlierAuto.Evenimente.ProcesatorPlasareComanda procesatorPlasareComanda = new ProcesatorPlasareComanda()

            //AtelierAuto.Evenimente.ProcesatorPlasareComanda procesatorPlasareComanda = new AtelierAuto.Evenimente.ProcesatorPlasareComanda();

            //  SalvareEvenimente(comanda);
            //readRepo.CautaComanda(new Guid());
            // readRepo.IncarcaDinListaDeEvenimente();
            int k = 2;
            while (k == 2)
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
                    var message22 = "";
                    consumer.Received += (model, ea) =>
                    {
                        var body2 = ea.Body;
                        var message2 = Encoding.UTF8.GetString(body2);
                        Console.WriteLine(" [x] Received {0}", message2);
                    };
                     channel.BasicConsume(queue: "hello",
                                          autoAck: true,
                                          consumer: consumer);
                    if (message22.Equals("ComandaPlasata"))
                    {
                        Console.WriteLine("Comanda a fost plasata - se poate citi din baza de date ");
                        k = 3;
                       // eve = ReadRepository.IncarcaDinListaDeEvenimente();
                    }
                }




                Console.ReadLine();
               // Console.ReadKey();
            }
        }
    }
}
