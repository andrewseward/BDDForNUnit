using System;
using BDDForNUnit.Attributes;
using Moq;
using NUnit.Core;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class TestDescriberTests
    {
        private Mock<ITestDescriptionWriter> _mockTestDescriptionWriter;
        private string _givenMethodName;
        private string _whenMethodName;
        private string _testMethodName;

        [SetUp]
        public void GivenATestWhenWriteDescription()
        {
            _givenMethodName = "GivenMethod1";
            var givenMethods = new[]
                                       {
                                           new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod(_givenMethodName), typeof(GivenAttribute), new Mock<IReflectionProvider>().Object, new Mock<ITestDescriber>().Object)
                                       };
            _whenMethodName = "WhenMethod1";
            var whenMethods = new[]
                                      {
                                          new BDDNUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod(_whenMethodName), typeof(WhenAttribute), new Mock<IReflectionProvider>().Object, new Mock<ITestDescriber>().Object)
                                      };
            
            _mockTestDescriptionWriter = new Mock<ITestDescriptionWriter>();
            _testMethodName = "TestMethod1";

            var testDescriber = new TestDescriber(_mockTestDescriptionWriter.Object);
            testDescriber.WriteDescription(_testMethodName, givenMethods, whenMethods);
        }

        [Test]
        public void ThenGivenMethodDescriptionIsOutput()
        {
            _mockTestDescriptionWriter.Verify(co => co.Write(_givenMethodName, "Given"));
        }

        [Test]
        public void ThenWhenMethodDescriptionIsOutput()
        {
            _mockTestDescriptionWriter.Verify(co => co.Write(_whenMethodName, "When"));
        }

        [Test]
        public void ThenTestMethodDescriptionIsOutput()
        {
            _mockTestDescriptionWriter.Verify(co => co.Write(_testMethodName, "Then"));
        }
    }
}