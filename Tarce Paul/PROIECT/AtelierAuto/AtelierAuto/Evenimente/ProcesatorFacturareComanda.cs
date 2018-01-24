using AtelierAuto.Models;
using AtelierAuto.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Evenimente
{
    public class ProcesatorFacturareComanda : ProcesatorEveniment
    {
        public override void Proceseaza(Eveniment e)
        {
            var repo = new WriteRepository();
            var ePlasareComanda = e.ToGeneric<Comanda>();

            var comanda = repo.GasesteComnada(ePlasareComanda.IdRadacina);
            // comanda. // metoda pentru schimbarea  starii in plasata
            comanda.stareComanda = StareComanda.Terminata;

           // repo.SalvareEvenimente(comanda);
        }
    }
}