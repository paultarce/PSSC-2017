using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.Contracts;

namespace AtelierAuto.Models.Generic
{
    public class SerieSasiu
    {
        private string _sasiu;
        public string sasiu { get { return _sasiu; } }
        public SerieSasiu(string sasiu)
        {
            //Contract.Requires<ArgumentNullException>(sasiu.Length != 8, "SerieSasiu lenght not right");
            _sasiu = sasiu;

        }
    }
}