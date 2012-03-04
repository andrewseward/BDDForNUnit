using System;
using System.Collections;
using BDDForNUnit.Attributes;
using Moq;
using NUnit.Core;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class TypeManagerTests
    {
        private BDDNUnitTestMethod[] _returnedMethods;
        private Type _testType;

        [SetUp]
        public void GivenClassTypeWithTestsWhenGetNUnitTestMethodsWithAttributeOfTypeThen()
        {
            _testType = typeof (ThenAttribute);
            _returnedMethods = new TypeManager(new Mock<IReflectionProvider>().Object, new Mock<ITestDescriber>().Object, new Mock<ITestExceptionWriter>().Object).GetNUnitTestMethodsWithAttribute(typeof(BDDTestFixtureTestClass),
                                                                                  _testType);
        }

        [Test]
        public void ThenNUnitTestMethodsWithAttributesOfTypeThenAreReturned()
        {
            //Not possible to compare NUnitTestMethod objects so this will do
            Assert.That(_returnedMethods.Length, Is.EqualTo(2));
            CollectionAssert.AllItemsAreNotNull(_returnedMethods);
            CollectionAssert.AllItemsAreInstancesOfType(_returnedMethods, typeof(BDDNUnitTestMethod));
            
        }

        [Test]
        public void ThenTheTestTypeIsSet()
        {
            foreach (BDDNUnitTestMethod testMethod in _returnedMethods)
            {
                Assert.That(testMethod.TestTypeAttribute, Is.EqualTo(_testType));
            }
        }




    }
}