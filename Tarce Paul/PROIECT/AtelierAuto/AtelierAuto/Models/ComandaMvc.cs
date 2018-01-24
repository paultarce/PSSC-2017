using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Models.Generic;

namespace AtelierAuto.Models
{
    public class ComandaMvc
    {

        public Mecanic mecanic { get;  set; }
        public Client client { get;  set; }
        public IDComanada iDComanda { get;  set; }  // primary key
        public StareComanda stareComanda { get; set; }
        public Masina masina { get;  set; }
        public double cost { get;  set; }

        public string cerereClient { get;  set; }
        public List<string> evaluareMecanic { get;  set; }

        public ComandaMvc()
        {

        }

        public ComandaMvc(PlainText numeMecanic,int idMecanic, PlainText numeClient,int idClient, IDComanada iDComanda,PlainText modelMasina,int anFabricatie,CIV civ,SerieSasiu serie, string cerereClient)
        {
            this.mecanic.nume = numeMecanic;
            this.mecanic.idMecanic = idMecanic;

            this.client.nume = numeClient;
            this.client.idClient = idClient;

            this.iDComanda = iDComanda;

            this.masina.Model = modelMasina;
            this.masina.anFabricatie = anFabricatie;
            this.masina.civ = civ;
            this.masina.serieSasiu = serie;

            this.cerereClient = cerereClient;

            this.stareComanda = StareComanda.Creeata;
            this.evaluareMecanic = new List<string>();
            this.cost = 0; 
        }
        /*
         * 
         * public PlainText Model { get; private set; }
        public int anFabricatie { get; private set; }
        public CIV civ { get; private set; }
        public SerieSasiu serieSasiu { get; private set; }
        */
    }
}