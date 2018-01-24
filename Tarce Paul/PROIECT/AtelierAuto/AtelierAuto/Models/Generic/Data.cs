using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AtelierAuto.Models.Generic
{
    public class Data
    {
        private string _data;
        public string Id { get { return _data; } }
        public Data(string data)
        {
            _data = data;
                
        }
    }
}