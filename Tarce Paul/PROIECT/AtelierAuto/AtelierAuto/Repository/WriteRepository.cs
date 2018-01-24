using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using AtelierAuto.Models;
using AtelierAuto.Evenimente;
using System.IO;

using System.Data;
using System.Data.SqlClient;

using System.Data.SqlTypes;
using System.Data.Sql;
using AtelierAuto.Models.Generic;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;



namespace AtelierAuto.Repository
{
    public class WriteRepository
    {
        public WriteRepository()
        {

        }


        public Comanda GasesteComnada(IDComanada idComanda)
        {
            //load events
            var evemimenteComanda = IncarcaListaDeEvenimente().Where(e => e.IdRadacina == idComanda);
            return new Comanda(evemimenteComanda);
        }
        public Comanda PlaseazaComanda(Comanda comanda)
        {
            var comandaNoua = new Comanda(comanda);
            //SalvareEvenimente(comandaNoua);
            SalvareComandaInListaComenzi(comanda);
            return comandaNoua;

        }

       
        public void SalvareEvenimente(Eveniment evenimentNoi)
        {
            //  List<Eveniment> toateEvenimentele = IncarcaListaDeEvenimente();
            //  toateEvenimentele.AddRange(evenimentNoi);

            string detalii = JsonConvert.SerializeObject(evenimentNoi.Detalii);
            var tipEveniment = evenimentNoi.Tip;
            // detalii = 
            var IdRadacina = evenimentNoi.IdRadacina.Id;
            var IdEveniment = evenimentNoi.Id.Id;

            ///Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf;Integrated Security=True
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                @"='C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf';Integrated Security=True")) //incerc si fara '
            {
                string _sql = @"INSERT INTO [dbo].[Comanda](IdEveniment,TipEveniment,DetaliiEveniment,IdRadacina)" +
                      "VALUES (@IdEveniment,@tipEveniment,@detalii,@IdRadacina)";
                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@tipEveniment", SqlDbType.VarChar))
                    .Value = tipEveniment;
                cmd.Parameters
                    .Add(new SqlParameter("@detalii", SqlDbType.VarChar))
                    .Value = detalii;
                cmd.Parameters
                    .Add(new SqlParameter("@IdRadacina", SqlDbType.VarChar))
                    .Value = IdRadacina;
                cmd.Parameters
                    .Add(new SqlParameter("@IdEveniment", SqlDbType.VarChar))
                    .Value = IdRadacina;
                cn.Open();
                var reader = cmd.ExecuteReader();

                TrimiteNotificareaLaMecanic(); //RABBIT MQ

                //scrie in baza de date evenimentele// sau in fisier : scrie - continut - fisier
            }
        }

        public bool StergereMasina(string idRadacina)
        {
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
                 @"='C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf';Integrated Security=True")) //incerc si fara '
            {
                string _sql = @"DELETE FROM [dbo].[Comanda] WHERE [IdRadacina]=@idRadacina";

                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@idRadacina", SqlDbType.NVarChar))
                    .Value = idRadacina;
                cn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return true;
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    return false;
                }
            }
        }
        private void SalvareComandaInListaComenzi(Comanda comanda)
        {
            List<Comanda> toateComenzile = IncarcaListaDeComenzi();
            toateComenzile.Add(comanda);
        }

        private List<Comanda> IncarcaListaDeComenzi()
        {
            List<Comanda> toateComenzile = new List<Comanda>();
            //citire din baza date a comenzilor // sau din fisier json

            return toateComenzile;
        }

        public List<Eveniment> IncarcaListaDeEvenimente()
        {
            List<Eveniment> toateEvenimentele = new List<Eveniment>();

            //citire din DB evenimente 
            return toateEvenimentele;
        }
        public void TrimiteNotificareaLaMecanic()
        {
            Send objSend = new Send();
            objSend.Sender();
        }
    }

    class Send //CLIENTUL TRIMITE COMANDA
    {
        public void Sender()
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

                string message = "ComandaPlasata";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine("ComandaPlasata", message);
            }


        }
    }
}