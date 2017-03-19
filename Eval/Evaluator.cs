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
            var lines = input.Split("\r\n".ToCharArray());

            if (lines.Length == 1)
            {
                return evalStringWithVars(input, new Dictionary<string, double>());
            }

            var formula = lines[0];
            var jsonData = String.Join("\r\n", lines.Skip(1));

            return evalStringWithVarsAsJson(formula, jsonData);
            
        }

        public string evalStringWithVarsAsJson(string input, string data)
        {
            JObject obj = JObject.Parse(data);

            var vars = new Dictionary<string, double>();

            foreach (var prop in obj.Properties())
            {
                // HACK
                vars[prop.Name.Replace("_", "SUPER")] = double.Parse(prop.Value.ToString());
            }

            return evalStringWithVars(input, vars);
        }

        private string evalStringWithVars(string input, Dictionary<string, double> vars)
        {
            // HACK
            var expr = calc.ParseExpression(input.Replace("_", "SUPER").Replace(",", "."), vars);
            var func = expr.Compile();
            return func().ToString();
        }
    }
}
