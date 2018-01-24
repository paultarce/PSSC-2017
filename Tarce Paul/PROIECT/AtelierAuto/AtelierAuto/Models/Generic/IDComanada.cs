using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Models.Generic
{
    public class IDComanada
    {
        private int _id;
        public int Id { get { return _id; } }
        public IDComanada(int id)
        {
            _id = id;
        }

       
    }
}