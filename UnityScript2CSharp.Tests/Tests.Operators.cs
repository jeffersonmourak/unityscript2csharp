using NUnit.Framework;

namespace UnityScript2CSharp.Tests
{
    public partial class Tests
    {
        [Test]
        public void Bool_Implicit()
        {
            var sourceFiles = SingleSourceFor("bool_implicit.js", "import UnityScript2CSharp.Tests; function F(o:Operators) { if (o) { return o; } return false; }");
            var expectedConvertedContents = SingleSourceFor("bool_implicit.cs", "using UnityScript2CSharp.Tests; " + DefaultGeneratedClass + @"bool_implicit : MonoBehaviour { public virtual object F(Operators o) { if (o) { return o; } return false; } }");

            AssertConversion(sourceFiles, expectedConvertedContents);
        }

        [TestCase("o != null", "!(o == null)")]
        [TestCase("o == null")]
        [TestCase("o == \"foo\"")]
        [TestCase("i == 42")]
        [TestCase("i != 42")]
        [TestCase("i > 10")]
        [TestCase("i < 10")]
        [TestCase("i >= 10")]
        [TestCase("i <= 10")]
        [TestCase("i > 0 || i < 30")]
        [TestCase("i >= 0 && i <= 30")]
        public void BoolOperators(string usOperatorUsage, string csOperatorUsage = null)
        {
            var sourceFiles = SingleSourceFor("operators.js", $"function F(o:Object, i:int) {{ return {usOperatorUsage}; }}");
            var expectedConvertedContents = SingleSourceFor("operators.cs", DefaultGeneratedClass + $"operators : MonoBehaviour {{ public virtual bool F(object o, int i) {{ return {csOperatorUsage ?? usOperatorUsage}; }} }}");

            AssertConversion(sourceFiles, expectedConvertedContents);
        }

        [TestCase("i + j")]
        [TestCase("i << j")]
        [TestCase("i >> j")]
        [TestCase("i | j")]
        [TestCase("i & j")]
        [TestCase("i ^ j")]
        public void ArithmethicOperators(string operatorUsage)
        {
            var sourceFiles = SingleSourceFor("arithmetic_operators.js", $"function F(j:int, i:int) {{ return {operatorUsage}; }}");
            var expectedConvertedContents = SingleSourceFor("arithmetic_operators.cs", DefaultGeneratedClass + $"arithmetic_operators : MonoBehaviour {{ public virtual int F(int j, int i) {{ return {operatorUsage}; }} }}");

            AssertConversion(sourceFiles, expectedConvertedContents);
        }

        [TestCase("i >>= j", "i = i >> j")]
        [TestCase("i <<= j", "i = i << j")]
        [TestCase("i &= j", "i = i & j")]
        [TestCase("i |= j", "i = i | j")]
        [TestCase("i ^= j", "i = i ^ j")]
        [TestCase("i += j", "i = i + j")]
        [TestCase("i -= j", "i = i - j")]
        [TestCase("i *= j", "i = i * j")]
        [TestCase("i /= j", "i = i / j")]
        public void Arithmethic_InPlace_Operators(string usOperatorUsage, string csOperatorUsage)
        {
            var sourceFiles = SingleSourceFor("arithmetic_implicit_operators.js", $"function F(j:int, i:int) {{ {usOperatorUsage}; }}");
            var expectedConvertedContents = SingleSourceFor("arithmetic_implicit_operators.cs", DefaultGeneratedClass + $"arithmetic_implicit_operators : MonoBehaviour {{ public virtual void F(int j, int i) {{ {csOperatorUsage}; }} }}");

            AssertConversion(sourceFiles, expectedConvertedContents);
        }

        [TestCase("~x")]
        [TestCase("x++")]
        [TestCase("++x")]
        [TestCase("x--")]
        [TestCase("--x")]
        public void Unary_Numeric_Operators(string operatorUsage)
        {
            var sourceFiles = SingleSourceFor("unary_operators.js", $"function F(x:int) {{ return {operatorUsage}; }}");
            var expectedConvertedContents = SingleSourceFor("unary_operators.cs", DefaultGeneratedClass + $"unary_operators : MonoBehaviour {{ public virtual int F(int x) {{ return {operatorUsage}; }} }}");

            AssertConversion(sourceFiles, expectedConvertedContents);
        }

        [Test]
        public void Unary_Logical_Not()
        {
            var sourceFiles = SingleSourceFor("unary_logical_not.js", "function F(b:boolean) { return !b; }");
            var expectedConvertedContents = SingleSourceFor("unary_logical_not.cs", DefaultGeneratedClass + "unary_logical_not : MonoBehaviour { public virtual bool F(bool b) { return !b; } }");

            AssertConversion(sourceFiles, expectedConvertedContents);
        }

        [Test]
        public void X()
        {
            var sourceFiles = SingleSourceFor("operator_expression_type.js", "import UnityScript2CSharp.Tests; function F(op:Operators) { return (op * 1.2f).Message; }");
            var expectedConvertedContents = SingleSourceFor("operator_expression_type.cs", "using UnityScript2CSharp.Tests; " + DefaultGeneratedClass + "operator_expression_type : MonoBehaviour { public virtual string F(Operators op) { return (op * 1.2f).Message; } }");

            AssertConversion(sourceFiles, expectedConvertedContents);
        }
    }
}