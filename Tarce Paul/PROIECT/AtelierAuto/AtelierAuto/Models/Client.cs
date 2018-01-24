using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Models.Generic;

namespace AtelierAuto.Models
{
    public class Client
    {
       
        public PlainText nume { get;  set; }
        public int idClient { get;  set; }

        public Client(PlainText nume, int idClient)
        {
            this.nume = nume;
            this.idClient = idClient;
        }


    }
}