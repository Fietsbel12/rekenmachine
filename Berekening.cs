using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rekenmachine
{
    internal class Berekening
    {
        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public string Operation { get; set; }
        public double Result { get; set; }

        // Override de ToString-methode om netjes te kunnen printen
        public override string ToString()
        {
            return $"{Number1} {Operation} {Number2} = {Result}";
        }
    }
}
