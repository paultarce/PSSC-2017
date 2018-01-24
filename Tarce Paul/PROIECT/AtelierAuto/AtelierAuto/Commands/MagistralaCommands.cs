using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Commands
{
    public class MagistralaCommands
    {
        //singleton
        public static readonly Lazy<MagistralaCommands> Instance = new Lazy<MagistralaCommands>(() => new MagistralaCommands());

        private readonly Dictionary<Type, ProcesatorCommand> registru = new Dictionary<Type, ProcesatorCommand>();
        private MagistralaCommands() { }

        public void InregistreazaProcesator<T>(ProcesatorCommandGeneric<T> procesator) where T:Command
        {
            registru.Add(typeof(T), procesator);
        }

        public void Trimite<T>(T comanda) where T :Command
        {
            var procesator = registru[typeof(T)];
            procesator.Proceseaza(comanda);
        }
    }
}