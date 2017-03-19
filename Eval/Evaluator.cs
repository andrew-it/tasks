using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Sprache;
using Sprache.Calc;

namespace EvalTask
{
    using Newtonsoft.Json;

    class Evaluator
    {
        XtensibleCalculator calc = new Sprache.Calc.XtensibleCalculator();

        public string evalString(string input)
        {
            var expr = calc.ParseExpression(input);
            var func = expr.Compile();
            return func().ToString();
        }

        public string evalStringWithVarsAsJson(string input, string data)
        {
            JObject obj = JObject.Parse(data);

            var vars = new Dictionary<string, double>();

            foreach (var prop in obj.Properties())
            {
                vars[prop.Name] = double.Parse(prop.Value.ToString());
            }

            return evalStringWithVars(input, vars);
        }

        private string evalStringWithVars(string input, Dictionary<string, double> vars)
        {
            var expr = calc.ParseExpression(input, vars);
            var func = expr.Compile();
            return func().ToString();
        }
    }
}
