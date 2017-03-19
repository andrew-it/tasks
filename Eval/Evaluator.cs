using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Sprache;
using Sprache.Calc;

namespace EvalTask
{
    public class Evaluator
    {
        XtensibleCalculator calc = new XtensibleCalculator();

        public string EvalString(string input)
        {
            var lines = input.Split("\r\n".ToCharArray());

            if (lines.Length == 1)
            {
                return EvalStringWithVars(input, new Dictionary<string, double>());
            }

            var formula = lines[0];
            var jsonData = String.Join("\r\n", lines.Skip(1));

            return EvalStringWithVarsAsJson(formula, jsonData);
            
        }

        public string EvalStringWithVarsAsJson(string input, string data)
        {
            JObject obj = JObject.Parse(data);

            var vars = new Dictionary<string, double>();

            foreach (var prop in obj.Properties())
            {
                vars[prop.Name] = double.Parse(prop.Value.ToString());
            }

            return EvalStringWithVars(input, vars);
        }

        public string EvalStringWithVars(string input, Dictionary<string, double> vars)
        {
            // HACK
            var newVars = new Dictionary<string, double>();
            foreach (var item in vars)
            {
                newVars[item.Key.Replace("_", "SUPER")] = item.Value;
            }

            var expr = calc.ParseExpression(input.Replace("_", "SUPER").Replace(",", "."), newVars);
            var func = expr.Compile();
            return func().ToString(CultureInfo.InvariantCulture);
        }
    }
}
