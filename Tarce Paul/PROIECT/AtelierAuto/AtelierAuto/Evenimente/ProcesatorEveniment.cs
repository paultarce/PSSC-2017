using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Evenimente
{
    public abstract class ProcesatorEveniment
    {
        public abstract void Proceseaza(Eveniment e);
    }
}