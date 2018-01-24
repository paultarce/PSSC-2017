using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Models;
using Newtonsoft.Json;
using System.IO;
using AtelierAuto.Evenimente;
using System.Data.SqlClient;
using AtelierAuto.Models.Generic;
using System.Text.RegularExpressions;
using System.Data;

namespace AtelierAuto.Repository
{
    public class ReadRepository
    {
        public ReadRepository()
        {

        }

        public IEnumerable<Comanda> ObtineComenzi()
        {
            List<Comanda> toateComenzile = new List<Comanda>();
            //read comenzi din DB
            return toateComenzile.AsEnumerable();
        }

       /* public Comanda CautaComanda(Guid Id)
        {
            return ObtineComenzi().Where(m => (dynamic)m.iDComanda == Id).FirstOrDefault();
        }*/

        public static List<Comanda> IncarcaDinListaDeComenzi() // sau list <comenzi> //sau static
        {
            //List<Eveniment> toateEvenimentele = new List<Eveniment>();
            List<Comanda> comneziCitite = new List<Comanda>();
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf;Integrated Security=True
            //  using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
            //    @"='C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf';Integrated Security=True'"))
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
              @"='C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf';Integrated Security=True"))
            {
                string _sql = @"SELECT * FROM [dbo].[Comanda] ";


                var cmd = new SqlCommand(_sql, cn);
                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //afisez ce citesc din baza de date
                        Console.WriteLine(String.Format("{0} {1} {2} {3}", reader["Id"], reader["TipEveniment"], reader["DetaliiEveniment"], reader["IdRadacina"]));


                        // split pe detalii: campurile din detaliii sa  fie string - split cu " split(' " ');
                        // din Detalii imi creez un agg root .
                        string[] tokens = reader["DetaliiEveniment"].ToString().Split('"');
                        string tip = Regex.Match(tokens[8], @"\d+").Value;
                        Comanda c = new Comanda();



                       // Eveniment e = new Eveniment(new IDComanada(Convert.ToInt32(reader["Id"])), TipEveniment.PlasareComnada, reader["DetaliiEveniment"]); //(TipEveniment)Enum.Parse(typeof(TipEveniment), reader["TipEveniment"].ToString())
                       // evenimenteCitite.Add(e);
                    }
                }

            }

            return comneziCitite; 
        }

        public Comanda CautaComanda(string idRadacina)
        {
            Comanda c = new Comanda();
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
              @"='C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf';Integrated Security=True"))
            {
                string _sql = @"SELECT * FROM [dbo].[Comanda] WHERE [IdRadacina]=@idRadacina";


                var cmd = new SqlCommand(_sql, cn);
                cmd.Parameters
                    .Add(new SqlParameter("@idRadacina", SqlDbType.NVarChar))
                    .Value = idRadacina;

                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string[] tokens = reader["DetaliiEveniment"].ToString().Split('"');
                        //     marca.Add(tokens[13]);
                        string tip = Regex.Match(tokens[8], @"\d+").Value;
                       // c = new Masina(new PlainText(tokens[5]), (TipMasina)Enum.Parse(typeof(TipMasina), tip), new PlainText(tokens[13]), new PlainText(tokens[67]), new PlainText(tokens[19]), new PlainText(tokens[61]), new PlainText(tokens[25]), new PlainText(tokens[37]), new PlainText(tokens[51]), new PlainText(tokens[45]), new PlainText(tokens[43]), new PlainText(tokens[31]));
                        string stare = Regex.Match(tokens[70], @"\d+").Value;
                       
                       // c.stare = (StareMasina)Enum.Parse(typeof(StareMasina), stare);

                    }
                }

            }
            return c;

        }



        public static List<Comanda> IncarcaDinListaDeEvenimente() // sau list <comenzi>
        {
            List<Eveniment> toateEvenimentele = new List<Eveniment>();
            List<Eveniment> evenimenteCitite = new List<Eveniment>();
            List<Comanda> comanda = new List<Comanda>();
            //Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf;Integrated Security=True
            //  using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
            //    @"='C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf';Integrated Security=True'"))
            using (var cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename" +
              @"='C:\Users\Paul\Documents\GitHub\ProjectsAn4\AtelierAuto\AtelierAuto\App_Data\MecanicDatabase.mdf';Integrated Security=True"))
            {
                string _sql = @"SELECT * FROM [dbo].[Comanda] ";
                var cmd = new SqlCommand(_sql, cn);
                cn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //afisez ce citesc din baza de date
                        Console.WriteLine(String.Format("{0} {1} {2} {3}", reader["Id"], reader["TipEveniment"], reader["DetaliiEveniment"], reader["IdRadacina"]));


                        // split pe detalii: campurile din detaliii sa  fie string - split cu " split(' " ');
                        // din Detalii imi creez un agg root .
                        string[] tokens = reader["DetaliiEveniment"].ToString().Split('"');
                        string stareComanda = Regex.Match(tokens[26], @"\d+").Value;
                        string idMecanic = Regex.Match(tokens[10], @"\d+").Value;
                        string idClient = Regex.Match(tokens[20], @"\d+").Value;
                        string idComanda = Regex.Match(tokens[24], @"\d+").Value;
                        string anFabricatie = Regex.Match(tokens[36], @"\d+").Value;
                        int a = Convert.ToInt32(idMecanic);
                        Comanda c = new Comanda(new Mecanic(new PlainText(tokens[7]), int.Parse(idMecanic)), new Client(new PlainText(tokens[17]), int.Parse(idClient)), new IDComanada(int.Parse(idComanda)),
                            new Masina(new PlainText(tokens[33]),int.Parse(anFabricatie), new CIV(tokens[41]), new SerieSasiu(tokens[47])), tokens[53]);

                        //Produs p = new Produs(new PlainText(tokens[5]), new PlainText(tokens[11]), new PlainText(tokens[17]), (TipProdus)Enum.Parse(typeof(TipProdus), tip), new PlainText(tokens[25]), new PlainText(tokens[31]), new PlainText(tokens[37]), new PlainText(tokens[43]), new PlainText(tokens[49]), (StareProdus)Enum.Parse(typeof(StareProdus), stare));
                        //  Eveniment e = new Eveniment(new IDComanada(Convert.ToInt32(reader["Id"])), TipEveniment.PlasareComnada, reader["DetaliiEveniment"]); //(TipEveniment)Enum.Parse(typeof(TipEveniment), reader["TipEveniment"].ToString())
                        // evenimenteCitite.Add(e);

                        comanda.Add(c);
                    }
                }
            }

            return comanda;
        }
    }
}


