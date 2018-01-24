using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using AtelierAuto.Models.Generic;

namespace AtelierAuto.Evenimente
{
    //in primul eveniment ( adaugare masina - Plasare comanda) pun si detaliile despre comanda
    public class Eveniment  //aceste evenimnete le pun in baza de date 
    {
        public IDComanada Id { get; private set; } //Id eveniment 
        public IDComanada IdRadacina { get; private set; } //identificatoru aggregation root-ului : comanda
        public TipEveniment Tip;
        public object Detalii { get; private set; }// detaliile despre obiectul adaugat ( comanda respectiva) 
                //daca evenimentul meu este adaugare comanda ... acest obiect "Detalii" trebuie sa contina toate detaliile despre comanda mea in momentul adaugarii ei

        public Eveniment(IDComanada idRadacina,TipEveniment tipEveniment,object detalii)
        {
            Tip = tipEveniment;
            IdRadacina = idRadacina;
            Detalii = detalii;
            Id = idRadacina ;
        }

        public EvenimentGeneric<T> ToGeneric<T>()
        {
            EvenimentGeneric<T> eveniment = null;

            if (Detalii is T)
            {
                eveniment = new EvenimentGeneric<T>(this.IdRadacina, this.Tip, (T)Detalii);
            }
            else if (Detalii is JObject)
            {
                var detalii = ((JObject)this.Detalii).ToObject<T>();
                eveniment = new EvenimentGeneric<T>(this.IdRadacina, this.Tip, detalii);
            }
            else
            {
                throw new InvalidCastException();
            }
            return eveniment;
        }
    }

    public class EvenimentGeneric<T> : Eveniment
    {
        public EvenimentGeneric(IDComanada idRadacina,TipEveniment tipEveniment,T detalii) :base(idRadacina,tipEveniment,detalii)
        {
        }
        public new T Detalii { get => (T)base.Detalii; }
    }
}