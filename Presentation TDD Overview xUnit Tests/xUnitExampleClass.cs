namespace Presentation_TDD_Overview_xUnit_Tests
{
    using System;

    using Moq;

    using Presentation_TDD_Overview;

    using Xunit;

    public class xUnitExampleClass
    {
        private static IDoStuff doer;

        public static void IndividualSetup()
        {
            // I run once per run of any test.
            var saver = new Mock<ISaveEvents>();
            doer = new DoStuff(() => DateTime.Now, saver.Object);
        }

        [Fact]
        public static void Divid_ZeroDenominator_Throws()
        {
            // Arrange
            var localDoer = GetDoStuff();

            // Act/Assert
            Assert.ThrowsDelegate action = () => localDoer.Divide(1, 0);
            var exception = Assert.Throws<DivideByZeroException>(action);
            Assert.Equal("Attempted to divide by zero.", exception.Message);
        }

        [Fact]
        public static void FireEvent_LeapYear_Writes()
        {
            var expectedYear = 2000;
            var saver = new Mock<ISaveEvents>();
            int localCopy = 0;
            saver.Setup(_ => _.Save(It.IsAny<int>())).Callback<int>(_ => localCopy = _);
            doer = new DoStuff(() => new DateTime(expectedYear, 01, 01), saver.Object);
            doer.FireEvent();
            Assert.Equal(expectedYear, localCopy);
        }

        [Fact]
        public static void FireEvent_NonLeapYear_DoesntWrite()
        {
            var saver = new Mock<ISaveEvents>();
            var initialValue = 0;
            int localCopy = initialValue;
            saver.Setup(_ => _.Save(It.IsAny<int>())).Callback<int>(_ => localCopy = _);
            doer = new DoStuff(() => new DateTime(2015, 01, 01), saver.Object);
            doer.FireEvent();
            Assert.Equal(initialValue, localCopy);
        }

        private static IDoStuff GetDoStuff()
        {
            // Extension point for additional setup for DoStuff (like mocking necessary construction arguments)
            var saver = new Mock<ISaveEvents>();
            doer = new DoStuff(() => DateTime.Now, saver.Object);
            return doer;
        }
    }
}
