using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Models
{
    public class Factura
    {
        public List<Comanda> comenzi { get; private set; }
        public  int IdFactura { get; private set; }
        public double totalPlata { get; private set; }

        //creez factura
        public Factura(int IdFactura,List<Comanda> comenzi)
        {
            this.IdFactura = IdFactura;
            this.comenzi = comenzi;
            this.totalPlata = 0;
        }

        public double CalculTotalPlata()
        {
            foreach(var com in comenzi)
            {
               this.totalPlata += com.cost;
            }
            return this.totalPlata;
        }
    }
}