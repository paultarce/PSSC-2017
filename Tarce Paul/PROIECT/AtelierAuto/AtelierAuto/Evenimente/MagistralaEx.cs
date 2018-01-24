using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Evenimente
{
    public static class MagistralaEx
    {
        public static void InregistreazaProcesatoareStandard(this MagistralaEvenimente magistrala)
        {
            magistrala.InregistreazaProcesator(TipEveniment.PlasareComnada, new ProcesatorPlasareComanda());
            magistrala.InregistreazaProcesator(TipEveniment.AcceptareComanda, new ProcesatorAcceptareComanda());
            magistrala.InregistreazaProcesator(TipEveniment.Facturare, new ProcesatorFacturareComanda());
        }
    }
}