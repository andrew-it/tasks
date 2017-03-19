using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;
using Sprache.Calc;

namespace EvalTask
{




    class Evaluator
    {
        XtensibleCalculator calc = new Sprache.Calc.XtensibleCalculator();

        public string evalString(string input)
        {
            var expr = calc.ParseExpression(input);
            var func = expr.Compile();
            return func().ToString();
        }
    }
}
