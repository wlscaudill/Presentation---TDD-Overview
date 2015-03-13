using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Presentation_TDD_Overview_MSTest_Tests
{
    using System;
    using System.Runtime.Remoting.Messaging;

    using Moq;

    using Presentation_TDD_Overview;

    [TestClass]
    public class MsTestExampleClass
    {
        private static IDoStuff doer;

        [ClassInitialize]
        public static void GlobalSetup(TestContext testContext)
        {
            // I run once per run of the tests in this class.
            var saver = new Mock<ISaveEvents>();
            doer = new DoStuff(() => DateTime.Now, saver.Object);
        }

        [TestInitialize]
        public void IndividualSetup()
        {
            // I run once per run of any test.
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void Divid_ZeroDenominator_Throws()
        {
            doer.Divide(1, 0);
        }
    }
}
