using System;
using BDDForNUnit.Attributes;
using Moq;
using NUnit.Core;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class BDDTestSuiteTests
    {
        private Mock<ITypeManager> _mockTypeManager;
        private Mock<IReflectionProvider> _mockReflectionProvider;
        private Type _fixtureType;
        private object _constructedFixture;
        private BDDTestSuite _bddTestSuite;
        private BDDNUnitTestMethod[] _methods;
        private BDDNUnitTestMethod[] _givenMethods;
        private BDDNUnitTestMethod[] _whenMethods;

        [SetUp]
        public void GivenTypeWithNUnitTestsWhenBDDTestSuiteIsConstructed()
        {
            _constructedFixture = new object();
            _fixtureType = typeof(BDDTestFixtureTestClass);

            _mockReflectionProvider = new Mock<IReflectionProvider>();
            _mockReflectionProvider.Setup(rp => rp.Construct(It.IsAny<Type>())).Returns(_constructedFixture);

            _givenMethods = new[]
                            {
                               new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("GivenMethod1"),typeof(GivenAttribute), _mockReflectionProvider.Object, new Mock<ITestDescriber>().Object,  new Mock<ITestExceptionWriter>().Object)
                           };

            _whenMethods = new[]
                            {
                               new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("WhenMethod1"), typeof(WhenAttribute), _mockReflectionProvider.Object, new Mock<ITestDescriber>().Object,  new Mock<ITestExceptionWriter>().Object)
                           };

            _methods = new[]
                           {
                               new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("TestMethod1"), typeof (ThenAttribute), _mockReflectionProvider.Object, new Mock<ITestDescriber>().Object,  new Mock<ITestExceptionWriter>().Object),
                               new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("TestMethod2"), typeof (ThenAttribute), _mockReflectionProvider.Object, new Mock<ITestDescriber>().Object,  new Mock<ITestExceptionWriter>().Object)
                           };
            _mockTypeManager = new Mock<ITypeManager>();
            _mockTypeManager.Setup(tm => tm.GetNUnitTestMethodsWithAttribute(It.IsAny<Type>(), typeof(ThenAttribute))).
                Returns(_methods);

            _mockTypeManager.Setup(tm => tm.GetNUnitTestMethodsWithAttribute(It.IsAny<Type>(), typeof(GivenAttribute))).
                Returns(_givenMethods);
            _mockTypeManager.Setup(tm => tm.GetNUnitTestMethodsWithAttribute(It.IsAny<Type>(), typeof(WhenAttribute))).
                Returns(_whenMethods);

            _bddTestSuite = new BDDTestSuite(_mockReflectionProvider.Object, _mockTypeManager.Object, _fixtureType);
        }

        [Test]
        public void ThenMethodsWithThenAttributesAreRetrieved()
        {
            _mockTypeManager.Verify(tm => tm.GetNUnitTestMethodsWithAttribute(_fixtureType, typeof (ThenAttribute)));
        }

        [Test]
        public void ThenGivenMethodsAreRetrieved()
        {
            _mockTypeManager.Verify(tm => tm.GetNUnitTestMethodsWithAttribute(_fixtureType, typeof(GivenAttribute)));
        }

        [Test]
        public void ThenWhenMethodsAreRetrieved()
        {
            _mockTypeManager.Verify(tm => tm.GetNUnitTestMethodsWithAttribute(_fixtureType, typeof(WhenAttribute)));
        }

        [Test]
        public void ThenTheGivenMethodsAreAssignedToTheThenMethods()
        {
            foreach (BDDNUnitTestMethod bddnUnitTestMethod in _bddTestSuite.Tests)
            {
                CollectionAssert.AreEqual(bddnUnitTestMethod.GivenMethods, _givenMethods);
            }
        }

        [Test]
        public void ThenTheWhenMethodsAreAssignedToTheThenMethods()
        {
            foreach (BDDNUnitTestMethod bddnUnitTestMethod in _bddTestSuite.Tests)
            {
                CollectionAssert.AreEqual(bddnUnitTestMethod.WhenMethods, _whenMethods);
            }
        }

        [Test]
        public void ThenFixtureIsConstructed()
        {
            _mockReflectionProvider.Verify(rp => rp.Construct(_fixtureType));
        }

        [Test]
        public void ThenConstructedFixtureIsStoredAgainstTestSuite()
        {
            Assert.That(_bddTestSuite.Fixture, Is.EqualTo(_constructedFixture));
        }

        [Test]
        public void ThenTestMethodsAreAdded()
        {
            CollectionAssert.AreEquivalent(_bddTestSuite.Tests, _methods);
        }

        [Test]
        public void ThenFixtureTypeIsStored()
        {
            Assert.That(_bddTestSuite.FixtureType, Is.EqualTo(_fixtureType));
        }
    }

}