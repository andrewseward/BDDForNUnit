using System;
using System.Reflection;
using BDDForNUnit.NUnitPlugin;
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
                               new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("GivenMethod1"),typeof(GivenAttribute), _mockReflectionProvider.Object, new Mock<ITestDescriber>().Object,  new Mock<ITestExceptionWriter>().Object)
                           };
            _whenMethods = new[]
                           {
                               new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod("WhenMethod1"), typeof(WhenAttribute), _mockReflectionProvider.Object, new Mock<ITestDescriber>().Object,  new Mock<ITestExceptionWriter>().Object)
                           };


            _testName = "TestMethod1";
            _bddNUnitTestMethod = new BDDNUnitTestMethod(typeof(BDDTestFixtureTestClass).GetMethod(_testName),
                                                         typeof(ThenAttribute), _mockReflectionProvider.Object, _mockTestDescriptionWriter.Object, new Mock<ITestExceptionWriter>().Object)
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

    [TestFixture]
    public class BDDNUnitTestMethodThrowingErrorTests
    {
        private BDDNUnitTestMethod _bddNUnitTestMethod;

        private Mock<ITestDescriber> _mockTestDescriptionWriter;

        

        private Exception thrownException;
        private string _testName;
        private Mock<ITestExceptionWriter> _mockTestExceptionWriter;
        private Exception _caughtException;

        [SetUp]
        public void GivenBDDNUnitTestMethodWithGivenAndWhenMethodsIsRunWhenAnExceptionIsThrown()
        {
            thrownException = TestExceptionHelper.GenerateException();
            _mockTestExceptionWriter = new Mock<ITestExceptionWriter>();
            _mockTestDescriptionWriter = new Mock<ITestDescriber>();
            var mockReflectionProvider = new Mock<IReflectionProvider>();
            mockReflectionProvider.Setup(rp => rp.InvokeMethod(It.IsAny<MethodInfo>(), It.IsAny<object>())).Throws(thrownException);

            _testName = "TestMethod1";
            _bddNUnitTestMethod = new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod(_testName),
                                                         typeof (ThenAttribute), mockReflectionProvider.Object,
                                                         _mockTestDescriptionWriter.Object,
                                                         _mockTestExceptionWriter.Object)
                                      {
                                          GivenMethods = new[]
                                                             {
                                                                 new BDDNUnitTestMethod(
                                                                     typeof (BDDTestFixtureTestClass).GetMethod(
                                                                         "GivenMethod1"), typeof (GivenAttribute),
                                                                     mockReflectionProvider.Object,
                                                                     new Mock<ITestDescriber>().Object,
                                                                     new Mock<ITestExceptionWriter>().Object)
                                                             }
                                      };
            try
            {
                _bddNUnitTestMethod.RunTest();
            }
            catch (Exception exception)
            {
                _caughtException = exception;
            }
            
        }

        [Test]
        public void ThenTheExceptionIsWritten()
        {
            _mockTestExceptionWriter.Verify(writer => writer.WriteException(_testName, thrownException));
        }

        [Test]
        public void ThenTheExceptionIsReThrown()
        {
            Assert.That(_caughtException, Is.EqualTo(thrownException));
        }
    }
}