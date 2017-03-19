using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppChallenge
{

    class JsonVer3 : JsonVerX
    {
        public override int version => 3;

        public Product[] products { get; set; }
    }

}
