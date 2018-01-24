using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Models.Generic;
using AtelierAuto.Models;

namespace AtelierAuto.Commands
{
    public class CommandPlasareComanda: Command
    {        
        public Comanda Comanda { get; set; }
    }
}