using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Evenimente
{
    public enum TipEveniment
    {
        PlasareComnada,  //de catre client
        AcceptareComanda,
        EvaluareComanda_CalculCost,
        Lucreaza,
        Facturare
    }
}