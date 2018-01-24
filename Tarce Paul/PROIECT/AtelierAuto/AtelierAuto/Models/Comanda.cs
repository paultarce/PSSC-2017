using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AtelierAuto.Models.Generic;
using AtelierAuto.Evenimente;
using AtelierAuto.Repository;
using AtelierAuto.Commands;
using System.Collections.ObjectModel;
using AtelierAuto.Models.Exceptii;

namespace AtelierAuto.Models
{

    // Agg Root : Comanda
    public class Comanda
    {
        public Mecanic mecanic { get; private set; }
        public Client client { get; private set; }
        public IDComanada iDComanda { get; private set; }  // primary key
        public StareComanda stareComanda { get;  set; }
        public Masina masina { get; private set; }
        public double cost { get; private set; }

        public string cerereClient { get; private set; }
        public List<string> evaluareMecanic { get; private set; } // every element in list : one thing to do for the car( from the mechanig point of view ) 

        private readonly List<Eveniment> _evenimenteNoi = new List<Eveniment>();
        public  ReadOnlyCollection<Eveniment> EvenimenteNoi { get => _evenimenteNoi.AsReadOnly(); }
        private MagistralaEvenimente _magistralaEveniment;

        public Comanda()
        {

        }
        public Comanda(Comanda comanda,MagistralaEvenimente magistrala = null)// for now
        {

            _magistralaEveniment = magistrala;
            //if (comanda.iDComanda == Guid.Empty) throw new Exception("Id meci invalid");
            var e = new EvenimentGeneric<Comanda>(comanda.iDComanda, TipEveniment.PlasareComnada, comanda);
            Aplica(e);
            PublicaEveniment(e);
        }
        public Comanda(Mecanic mecanic, Client client,IDComanada iDComanda,Masina masina,string cerereClient)
        {
            this.mecanic = mecanic;
            this.client = client;
            this.iDComanda = iDComanda;
            this.masina = masina;
            this.cerereClient = cerereClient;

            this.stareComanda = StareComanda.Creeata;
            this.evaluareMecanic = new List<string>();
        }

        public Comanda(IEnumerable<Eveniment> evenimente)
        {
            foreach(var e in evenimente)
            {
                RedaEveniment(e);
            }
        }

        
        //Eveniment
        #region Evenimente
        public void PlasareComanda(Comanda comanda)
        {
            var eveniment = new EvenimentGeneric<Comanda>(comanda.iDComanda, TipEveniment.PlasareComnada,comanda);
            Aplica(eveniment);
            PublicaEveniment(eveniment);
        }

        public void AcceptareComanda()
        {
            var eveniment = new EvenimentGeneric<Comanda>(iDComanda, TipEveniment.AcceptareComanda, new Comanda());
            Aplica(eveniment);
            PublicaEveniment(eveniment);
        }
        public void FacturareComanda()
        {
            var eveniment = new EvenimentGeneric<Comanda>(iDComanda, TipEveniment.Facturare, new Comanda());
            eveniment.Detalii.cost = CalculCostComanda();
            Aplica(eveniment);
            PublicaEveniment(eveniment);
        }
        #endregion

        public double CalculCostComanda()
        {
            //extrag stringul Pret : din evaluarea mecanicului
            this.cost = 1230.4;
            return this.cost;
        }

        #region Procesare Evenimente
        private void Aplica(EvenimentGeneric<Comanda> e)
        {
            this.mecanic = e.Detalii.mecanic;
            this.client = e.Detalii.client;
            this.iDComanda = e.Detalii.iDComanda;
            this.masina = e.Detalii.masina;
            this.cerereClient = e.Detalii.cerereClient;
            this.stareComanda = e.Detalii.stareComanda;
            this.evaluareMecanic = e.Detalii.evaluareMecanic;
            
        }

        #endregion Procesare Evenimente


        public void RedaEveniment(Eveniment e)
        {
            switch (e.Tip)
            {
                case TipEveniment.PlasareComnada:
                    Aplica(e.ToGeneric<Comanda>());
                    break;
                case TipEveniment.AcceptareComanda:
                    Aplica(e.ToGeneric<Comanda>());
                    break;
                case TipEveniment.EvaluareComanda_CalculCost:
                    Aplica(e.ToGeneric<Comanda>());
                    break;
                case TipEveniment.Lucreaza:
                    Aplica(e.ToGeneric<Comanda>());
                    break;
                case TipEveniment.Facturare:
                    Aplica(e.ToGeneric<Comanda>());
                    break;
                default:
                    throw new EvenimentNecunoscutException();                
            }

        }
        private void PublicaEveniment(Eveniment eveniment)
        {
            _evenimenteNoi.Add(eveniment);
           
            if(_magistralaEveniment != null)
            {
                _magistralaEveniment.Trimite(eveniment);
            }
            else
            {
                MagistralaEvenimente.Instanta.Value.Trimite(eveniment);
            }
            
        }
    }
}