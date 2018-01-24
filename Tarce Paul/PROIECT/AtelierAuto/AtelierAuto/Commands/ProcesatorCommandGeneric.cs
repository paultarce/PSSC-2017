using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Commands
{
    public abstract class ProcesatorCommandGeneric<T> : ProcesatorCommand where T:Command
    {
        public abstract void Proceseaza(T command);
        public override void Proceseaza(Command command)
        {
            Proceseaza((T)command);
        }
    }
}