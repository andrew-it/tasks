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
        public void EvalProgram_BigNumber()
        {
            evaluator.EvalString("10'000").Should().Be("10000");
        }

        [Test]
        public void EvalProgram_Sqrt()
        {
            evaluator.EvalString("3.3 * sqrt(9)").Should().Be("9.9");
        }

        [Test]
        public void EvalProgram_SqrtOne()
        {
            evaluator.EvalString("sqrt(1)").Should().Be("1");
        }

        [Test]
        public void EvalProgram_Max()
        {
            evaluator.EvalString("2.2 * max(3; 4) * 2.5").Should().Be("22");
        }

        [Test]
        public void EvalProgram_Min()
        {
            evaluator.EvalString("min(3, 9)").Should().Be("3");
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
