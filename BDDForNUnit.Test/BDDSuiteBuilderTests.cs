
using System;
using System.Reflection;
using BDDForNUnit.Attributes;
using Moq;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class BDDSuiteBuilderTests_CanBuildFrom
    {
        private Mock<IReflectionProvider> _mockReflectionProvider;

        [SetUp]
        public void GivenClassWithBDDTestFixtureAttributeWhenCanBuildFromIsCalled()
        {
            _mockReflectionProvider = new Mock<IReflectionProvider>();

            _mockReflectionProvider.Setup(
                rp => rp.HasAttribute(It.IsAny<ICustomAttributeProvider>(), It.IsAny<Type>(), It.IsAny<bool>())).
                Returns(true);


            var bddSuiteBuilder = new BDDSuiteBuilder(_mockReflectionProvider.Object);

            bddSuiteBuilder.CanBuildFrom(typeof (BDDTestFixtureTestClass));
        }

        [Test]
        public void ThenCanBuildFromIsChecked()
        {
            _mockReflectionProvider.Verify(rp => rp.HasAttribute(typeof(BDDTestFixtureTestClass), typeof(BDDTestFixtureAttribute), false));
        }
    }

    [TestFixture]
    public class BDDSuiteBuilderTests_BuildFrom
    {
        private NUnit.Core.Test _returnedTestFixture;

        [SetUp]
        public void GivenClassWithBDDTestFixtureAttributeWhenBuildFromIsCalled()
        {
            var bddSuiteBuilder = new BDDSuiteBuilder(new Mock<IReflectionProvider>().Object);

            _returnedTestFixture = bddSuiteBuilder.BuildFrom(typeof(BDDTestFixtureTestClass));
        }

        [Test]
        public void ThenBDDTestFixtureIsReturned()
        {
            Assert.That(_returnedTestFixture, Is.InstanceOf<BDDTestSuite>());
        }
    }
}