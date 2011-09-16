using BDDForNUnit.Attributes;
using Moq;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class BDDNUnitTestMethodTests
    {
        private Mock<IReflectionProvider> _mockReflectionProvider;
        private BDDNUnitTestMethod[] _givenMethods;
        private BDDNUnitTestMethod[] _whenMethods;
        private BDDNUnitTestMethod _bddNUnitTestMethod;
        private Mock<ITestDescriber> _mockTestDescriptionWriter;
        private string _testName;

        [SetUp]
        public void GivenBDDNUnitTestMethodWithGivenAndWhenMethodsWhenRun()
        {
            _mockReflectionProvider = new Mock<IReflectionProvider>();
            _mockTestDescriptionWriter = new Mock<ITestDescriber>();
            _givenMethods = new[]
                           {
                               new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("GivenMethod1"),typeof(GivenAttribute), _mockReflectionProvider.Object, new Mock<ITestDescriber>().Object)
                           };
            _whenMethods = new[]
                           {
                               new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("WhenMethod1"), typeof(WhenAttribute), _mockReflectionProvider.Object, new Mock<ITestDescriber>().Object)
                           };


            _testName = "TestMethod1";
            _bddNUnitTestMethod = new BDDNUnitTestMethod(typeof(BDDTestFixtureTestClass).GetMethod(_testName),
                                                         typeof(ThenAttribute), _mockReflectionProvider.Object, _mockTestDescriptionWriter.Object)
                                      {
                                          GivenMethods = _givenMethods,
                                          WhenMethods = _whenMethods
                                      };

            _bddNUnitTestMethod.RunTest();
        }

        [Test]
        public void ThenGivenMethodsAreInvoked()
        {
            _mockReflectionProvider.Verify(rp => rp.InvokeMethod(_givenMethods[0].Method, _bddNUnitTestMethod.Fixture));
        }

        [Test]
        public void ThenWhenMethodsAreInvoked()
        {
            _mockReflectionProvider.Verify(rp => rp.InvokeMethod(_whenMethods[0].Method, _bddNUnitTestMethod.Fixture));
        }

        [Test]
        public void ThenTestDescriptionIsWritten()
        {
            _mockTestDescriptionWriter.Verify(dw => dw.WriteDescription(_testName, _givenMethods, _whenMethods));
        }
    }
}