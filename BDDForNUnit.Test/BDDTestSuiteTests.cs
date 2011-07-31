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
        public void FixtureTypeIsStored()
        {
            Assert.That(_bddTestSuite.FixtureType, Is.EqualTo(_fixtureType));
        }
    }

    [TestFixture]
    public class BDDTestSuiteTests_DoOneTimeSetUp
    {
        private Mock<ITypeManager> _mockTypeManager;
        private Type _fixtureType;

        [SetUp]
        public void GivenWhenDoOneTimeSetUp()
        {
            _mockTypeManager = new Mock<ITypeManager>();
            _fixtureType = typeof (BDDTestFixtureTestClass);
            var bddTestSuite = new BDDTestSuite(new Mock<IReflectionProvider>().Object, _mockTypeManager.Object,
                                                _fixtureType);

            bddTestSuite.RunDoOneTimeSetUp(new TestResult(new TestName()));
        }

        [Test]
        public void ThenGivenMethodsAreRetrieved()
        {
            _mockTypeManager.Verify(tm=>tm.GetNUnitTestMethodsWithAttribute(_fixtureType, typeof(GivenAttribute)));
        }
    }
}