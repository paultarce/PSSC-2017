using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Repository;

namespace AtelierAuto.Commands
{
    public class ProcesatorFacturare : ProcesatorCommandGeneric<CommandFacturare>
    {
        public ProcesatorFacturare()
        {

        }
        public override void Proceseaza(CommandFacturare command)//command -> comanda( arhitectura) ... comanda -> agg root
        {
            var repo = new WriteRepository();
            var comanda = repo.GasesteComnada((dynamic)command.IdComanda);
            // comanda. // metoda pentru schimbarea  starii in plasata
            repo.SalvareEvenimente(comanda);
        }
    }
}