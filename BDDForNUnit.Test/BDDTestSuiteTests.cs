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
        private NUnitTestMethod[] _methods;

        [SetUp]
        public void GivenTypeWithNUnitTestsWhenBDDTestSuiteIsConstructed()
        {
            _constructedFixture = new object();
            _fixtureType = typeof(BDDTestFixtureTestClass);

            _mockReflectionProvider = new Mock<IReflectionProvider>();
            _mockReflectionProvider.Setup(rp => rp.Construct(It.IsAny<Type>())).Returns(_constructedFixture);

            _methods = new[]
                           {
                               new NUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("TestMethod1")),
                               new NUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("TestMethod2"))
                           };
            _mockTypeManager = new Mock<ITypeManager>();
            _mockTypeManager.Setup(tm => tm.GetNUnitTestMethodsWithAttribute(It.IsAny<Type>(), It.IsAny<Type>())).
                Returns(_methods);

            _bddTestSuite = new BDDTestSuite(_mockReflectionProvider.Object, _mockTypeManager.Object, _fixtureType);
        }

        [Test]
        public void ThenMethodsWithThenAttributesAreRetrieved()
        {
            _mockTypeManager.Verify(tm => tm.GetNUnitTestMethodsWithAttribute(_fixtureType, typeof (ThenAttribute)));
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

    [TestFixture]
    public class BDDTestSuiteTests_DoOneTimeSetUp
    {
        private Mock<ITypeManager> _mockTypeManager;
        private Type _fixtureType;
        private Mock<IReflectionProvider> _mockReflectionProvider;
        private NUnitTestMethod[] _givenMethods;
        private NUnitTestMethod[] _whenMethods;
        private object _fixture;

        [SetUp]
        public void GivenWhenDoOneTimeSetUp()
        {
            _givenMethods = new[]
                           {
                               new NUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("GivenMethod1"))
                           };
            _whenMethods = new[]
                           {
                               new NUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("WhenMethod1"))
                           };
            _mockTypeManager = new Mock<ITypeManager>();
            _mockTypeManager
                .Setup(tm => tm.GetNUnitTestMethodsWithAttribute(It.IsAny<Type>(), typeof(GivenAttribute)))
                .Returns(_givenMethods);
            _mockTypeManager
                .Setup(tm => tm.GetNUnitTestMethodsWithAttribute(It.IsAny<Type>(), typeof(WhenAttribute)))
                .Returns(_whenMethods);

            _fixtureType = typeof (BDDTestFixtureTestClass);
            _mockReflectionProvider = new Mock<IReflectionProvider>();

            var bddTestSuite = new BDDTestSuite(_mockReflectionProvider.Object, _mockTypeManager.Object,
                                                _fixtureType);
            _fixture = new object();
            bddTestSuite.Fixture = _fixture;
            bddTestSuite.RunDoOneTimeSetUp(new TestResult(new TestName()));
        }

        [Test]
        public void ThenGivenMethodsAreRetrieved()
        {
            _mockTypeManager.Verify(tm=>tm.GetNUnitTestMethodsWithAttribute(_fixtureType, typeof(GivenAttribute)));
        }

        [Test]
        public void ThenGivenMethodsAreInvoked()
        {
            _mockReflectionProvider.Verify(rp => rp.InvokeMethod(_givenMethods[0].Method, _fixture));
        }

        [Test]
        public void ThenWhenMethodsAreRetrieved()
        {
            _mockTypeManager.Verify(tm => tm.GetNUnitTestMethodsWithAttribute(_fixtureType, typeof(WhenAttribute)));
        }

        [Test]
        public void ThenWhenMethodsAreInvoked()
        {
            _mockReflectionProvider.Verify(rp => rp.InvokeMethod(_whenMethods[0].Method, _fixture));
        }

        
    }
}