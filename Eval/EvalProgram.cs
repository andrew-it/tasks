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
		    string output = evaluator.evalString(input);
            
			Console.WriteLine(output);
		}
	}
}
