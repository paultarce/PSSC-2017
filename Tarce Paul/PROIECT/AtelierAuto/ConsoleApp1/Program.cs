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


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            MagistralaCommands.Instance.Value.InregistreazaProcesatoareStandard();
            MagistralaEvenimente.Instanta.Value.InregistreazaProcesatoareStandard();
            MagistralaEvenimente.Instanta.Value.InchideInregistrarea();
            List<Eveniment> eve = new List<Eveniment>();

            var writeRepo = new WriteRepository();
            var readRepo = new ReadRepository();

            var masina = new Masina(new PlainText("WV Passat"), 2005, new CIV("EI309MNN"), new SerieSasiu("ALABALAPR"));
            var mecanic = new Mecanic(new PlainText("Nelutu"), 1);
            var client = new Client(new PlainText("Orlando"), 1);

            var comanda = new Comanda(mecanic, client,new IDComanada(1), masina, "reparatie turbina");

            writeRepo.PlaseazaComanda(comanda);

            //var commandPlasareComanda = new CommandPlasareComanda();
            //MagistralaCommands.Instance.Value.Trimite(commandPlasareComanda);
            //AtlierAuto.Evenimente.ProcesatorPlasareComanda procesatorPlasareComanda = new ProcesatorPlasareComanda()

            //AtelierAuto.Evenimente.ProcesatorPlasareComanda procesatorPlasareComanda = new AtelierAuto.Evenimente.ProcesatorPlasareComanda();

            //  SalvareEvenimente(comanda);
            //readRepo.CautaComanda(new Guid());
            // readRepo.IncarcaDinListaDeEvenimente();
            //eve = ReadRepository.IncarcaDinListaDeEvenimente();
            Console.ReadLine();
            Console.ReadKey();
        }  
    }
}
