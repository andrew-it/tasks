using System;

namespace EvalTask
{
    internal class EvalProgram
    {
        private static void Main(string[] args)
        {
            var evaluator = new Evaluator();
            ;
            var input = Console.In.ReadToEnd();
            var output = evaluator.EvalString(input);

            Console.WriteLine(output);
        }
    }
}