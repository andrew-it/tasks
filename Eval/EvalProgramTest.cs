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
            evaluator.EvalString("2 + 3").Should().Be("5");
        }

        [Test]
        public void EvalProgram_TwoPlusThreeWithCommas()
        {
            evaluator.EvalString("2,3 + 3,2").Should().Be("5.5");
        }


        [Test]
        public void EvalProgram_ComplexFormula()
        {
            evaluator.EvalString("2 + 3 * 12 / (1 + (55 / 11))").Should().Be("8");
        }

        [Test]
        public void EvalProgram_FormulaWithVariables()
        {
            evaluator.EvalStringWithVarsAsJson("2 + 2 * x", "{'x': 3.5}").Should().Be("9");
        }

        [Test]
        public void EvalProgram_FormulaWithVar()
        {
            evaluator.EvalStringWithVarsAsJson("b", "{\"a\": 1,\r\n" +
                                                    " \"b\": 2}").Should().Be("2");
        }

        [Test]
        public void EvalProgram_FormulaWithVarWithUnderscore()
        {
            evaluator.EvalStringWithVarsAsJson("b_", "{\"a\": 1,\r\n" +
                                                    " \"b_\": 2}").Should().Be("2");
        }
    }
}
