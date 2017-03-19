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
            var lines = input.Split("\r\n".ToCharArray());

		    string output = "";

		    if (lines.Length == 1)
		    {
                output = evaluator.evalString(input);
            } else
		    {
		        var formula = lines[0];
		        var jsonData = String.Join("\r\n", lines.Skip(1));
               
                output = evaluator.evalStringWithVarsAsJson(formula, jsonData);
            }
            
			Console.WriteLine(output);
		}
	}
}
