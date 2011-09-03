using System;
using System.Reflection;
using BDDForNUnit.Attributes;
using Moq;
using NUnit.Core;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class BDDTestCaseBuilderTests_CanBuildFromMethodWithThenAttribute
    {
        private Mock<IReflectionProvider> _mockReflectionProvider;
        private bool _returnedValue;
        private MethodInfo _methodInfo;

        [SetUp]
        public void GivenBDDTestCaseBuilderAndMethodWithThenAttributeWhenCanBuildFromIsCalled()
        {
            _mockReflectionProvider = new Mock<IReflectionProvider>();

            _mockReflectionProvider.Setup(
                rp => rp.HasAttribute(It.IsAny<ICustomAttributeProvider>(), It.IsAny<Type>(), It.IsAny<bool>())).
                Returns(true);


            var bddTestCaseBuilder = new BDDTestCaseBuilder(_mockReflectionProvider.Object, new Mock<ITestDescriber>().Object);

            _methodInfo = typeof(BDDTestFixtureTestClass).GetMethod("TestMethod1");
            _returnedValue = bddTestCaseBuilder.CanBuildFrom(_methodInfo);
        }

        [Test]
        public void ThenHasAttributeIsChecked()
        {
            _mockReflectionProvider.Verify(rp => rp.HasAttribute(_methodInfo, typeof(ThenAttribute), false));
        }

        [Test]
        public void ThenTrueIsReturned()
        {
            Assert.That(_returnedValue, Is.True);   
        }

    }

    [TestFixture]
    public class BDDTestCaseBuilderTests_BuildFrom
    {
        private NUnit.Core.Test _returnedTestMethod;
        private Mock<ITestDescriber> _mockTestDescriber;

        [SetUp]
        public void GivenClassWithBDDTestFixtureAttributeWhenBuildFromIsCalled()
        {
            _mockTestDescriber = new Mock<ITestDescriber>();
            var bddTestCaseBuilder = new BDDTestCaseBuilder(new Mock<IReflectionProvider>().Object, _mockTestDescriber.Object);

            var methodInfo = typeof (BDDTestFixtureTestClass).GetMethod("TestMethod1");
            _returnedTestMethod = bddTestCaseBuilder.BuildFrom(methodInfo);
        }

        [Test]
        public void ThenNUnitTestMethodIsReturned()
        {
            Assert.That(_returnedTestMethod, Is.InstanceOf<NUnitTestMethod>());
        }

        [Test]
        public void ThenTestIsSentTestDescriber()
        {
            _mockTestDescriber.Verify(td => td.WriteDescription(_returnedTestMethod));
        }
    }
}