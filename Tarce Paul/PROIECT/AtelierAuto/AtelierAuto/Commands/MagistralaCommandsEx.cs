using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Models;
using AtelierAuto.Evenimente;


namespace AtelierAuto.Commands
{
    public static class MagistralaCommandsEx
    {
        public static void InregistreazaProcesatoareStandard(this MagistralaCommands magistrala)
        {
            magistrala.InregistreazaProcesator(new ProcesatorPlasareComanda());
            magistrala.InregistreazaProcesator(new ProcesatorAcceptareComanda());
            magistrala.InregistreazaProcesator(new ProcesatorFacturare());
        }
    }
}