using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Commands;
using System.Threading.Tasks;

namespace AtelierAuto.Commands
{
    public abstract class ProcesatorCommand
    {
        public abstract void Proceseaza(Command comanda);
    }
}