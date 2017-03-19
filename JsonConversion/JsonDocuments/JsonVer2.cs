using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppChallenge
{
    public class JsonVer2 : JsonVerX
    {
        public Dictionary<string, Product> products { get; set; }
    }
}
