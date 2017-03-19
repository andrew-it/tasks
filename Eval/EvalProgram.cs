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
		    string output = evaluator.EvalString(input);
            
			Console.WriteLine(output);
		}
	}
}
