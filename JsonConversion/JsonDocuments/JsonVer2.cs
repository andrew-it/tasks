using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleAppChallenge
{
    public class JsonVer2 : JsonVerX
    {
        public override string version => "2";
        public Dictionary<string, double> constants { get; set; }
        public Dictionary<string, ProductWithPriceFormula> products { get; set; }
    }
}
