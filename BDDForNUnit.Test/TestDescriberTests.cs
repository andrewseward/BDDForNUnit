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
        private Mock<ITypeManager> _mockTypeManager;
        private string _givenMethodName;
        private string _whenMethodName;
        private string _testMethodName;

        [SetUp]
        public void GivenATestWhenWriteDescription()
        {
            _givenMethodName = "GivenMethod1";
            var givenMethods = new[]
                                       {
                                           new NUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod(_givenMethodName))
                                       };
            _whenMethodName = "WhenMethod1";
            var whenMethods = new[]
                                      {
                                          new NUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod(_whenMethodName))
                                      };
            _mockTypeManager = new Mock<ITypeManager>();
            _mockTypeManager
                .Setup(tm => tm.GetNUnitTestMethodsWithAttribute(It.IsAny<Type>(), typeof(GivenAttribute)))
                .Returns(givenMethods);
            _mockTypeManager
                .Setup(tm => tm.GetNUnitTestMethodsWithAttribute(It.IsAny<Type>(), typeof(WhenAttribute)))
                .Returns(whenMethods);

            _mockTestDescriptionWriter = new Mock<ITestDescriptionWriter>();
            _testMethodName = "TestMethod1";
            var nUnitTestMethod = new NUnitTestMethod(typeof (BDDTestFixtureTestClass).GetMethod(_testMethodName));

            var testDescriber = new TestDescriber(_mockTestDescriptionWriter.Object, _mockTypeManager.Object);
            testDescriber.WriteDescription(nUnitTestMethod);
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