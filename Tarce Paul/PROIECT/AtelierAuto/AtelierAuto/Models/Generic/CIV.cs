using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.Contracts;

namespace AtelierAuto.Models.Generic
{
    public class CIV
    {
        private string _civ;
        public string civ { get { return _civ; } }
        public CIV(string civ)
        {
            //Contract.Requires<ArgumentNullException>(data.Length != 8, "CIV lenght not right");
            _civ = civ;

        }
    }
}