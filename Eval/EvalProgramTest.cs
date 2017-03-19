using FluentAssertions;
using NUnit.Framework;

namespace EvalTask
{
    class EvalProgramTest
    {
        private Evaluator evaluator;

        [SetUp]
        public void SetUp()
        {
            evaluator = new Evaluator();
        }

        [Test]
        public void EvalProgram_TwoPlusThree()
        {
            evaluator.evalString("2 + 3").Should().Be("5");
        }


        [Test]
        public void EvalProgram_ComplexFormula()
        {
            evaluator.evalString("2 + 3 * 12 / (1 + (55 / 11))").Should().Be("8");
        }

        [Test]
        public void EvalProgram_FormulaWithVariables()
        {
            evaluator.evalStringWithVarsAsJson("2 + 2 * x", "{'x': 3.5}").Should().Be("9");
        }
    }
}
