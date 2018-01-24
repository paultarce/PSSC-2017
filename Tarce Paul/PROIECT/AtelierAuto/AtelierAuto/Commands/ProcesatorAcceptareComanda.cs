using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Repository;
using AtelierAuto.Models.Generic;

namespace AtelierAuto.Commands
{
    public class ProcesatorAcceptareComanda : ProcesatorCommandGeneric<CommandAcceptareComanda>
    {
        public ProcesatorAcceptareComanda()
        {

        }

        public override void Proceseaza(CommandAcceptareComanda command)//command -> comanda( arhitectura) ... comanda -> agg root
        {
            var repo = new WriteRepository();
            var comanda = repo.GasesteComnada((dynamic)command.IdComanda);
            // comanda. // metoda pentru schimbarea  starii in plasata
            repo.SalvareEvenimente(comanda);
        }
    }
}