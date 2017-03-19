using System;
using System.Linq;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
            Evaluator evaluator = new Evaluator();;
			string input = Console.In.ReadToEnd();
            var lines = input.Split("\n".ToCharArray());

		    string output = "";

		    if (lines.Length == 1)
		    {
                output = evaluator.evalString(input);
            } else if (lines.Length == 2)
		    {
		        var formula = lines[0];
		        var jsonData = String.Join("\n", lines.Skip(1));
               
                output = evaluator.evalStringWithVarsAsJson(formula, jsonData);
            }
            
			Console.WriteLine(output);
		}
	}
}
