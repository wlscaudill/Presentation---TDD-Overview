namespace Presentation_TDD_Overview_NUnit_Tests
{
    using System;

    using Moq;

    using NUnit.Framework;

    using Presentation_TDD_Overview;

    [TestFixture]
    public class NUnitExampleClass
    {
        private static IDoStuff doer;

        [TestFixtureSetUp]
        public static void GlobalSetup()
        {
            // I run once per run of the tests in this class.
            var saver = new Mock<ISaveEvents>();
            doer = new DoStuff(() => DateTime.Now, saver.Object);
        }

        [SetUp]
        public static void IndividualSetup()
        {
            // I run once per run of any test.
        }

        [Test]
        public void Divid_ZeroDenominator_ThrowsDivideByZeroException()
        {
            TestDelegate action = () => doer.Divide(1, 0);
            var exception = Assert.Throws<DivideByZeroException>(action);
            Assert.That(exception.Message, Is.EqualTo("Attempted to divide by zero."));
        }
    }
}
