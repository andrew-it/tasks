using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppChallenge
{
    public class JsonVer3 : JsonVerX
    {
        public override int version => 3;

        public ProductWithId[] products { get; set; }
    }

}
