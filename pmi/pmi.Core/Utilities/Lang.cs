using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pmi.Core.Utilities
{
    public class Lang
    {
        public string Text { get; private set; }
        public string Value { get; private set; }

        public string ImageName {
            get {
                return Value.Replace("-", "_");
            }
        }

        public Lang(string text, string value) {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
