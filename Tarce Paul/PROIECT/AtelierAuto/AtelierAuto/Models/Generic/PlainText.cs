using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics.Contracts;

namespace AtelierAuto.Models.Generic
{
    public class PlainText
    {
        private string _text;
        public string Text { get { return _text; } }

        public PlainText(string text)
        {
            

            _text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}