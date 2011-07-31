using System.Collections;
using BDDForNUnit.Attributes;
using NUnit.Core;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class TypeManagerTests
    {
        private NUnitTestMethod[] _returnedMethods;

        [SetUp]
        public void GivenClassTypeWithTestsWhenGetNUnitTestMethodsWithAttributeOfTypeThen()
        {
            _returnedMethods = new TypeManager().GetNUnitTestMethodsWithAttribute(typeof (BDDTestFixtureTestClass),
                                                                                  typeof (ThenAttribute));
        }

        [Test]
        public void ThenNUnitTestMethodsWithAttributesOfTypeThenAreReturned()
        {
            //Not possible to compare NUnitTestMethod objects so this will do
            Assert.That(_returnedMethods.Length, Is.EqualTo(2));
            CollectionAssert.AllItemsAreNotNull(_returnedMethods);
            CollectionAssert.AllItemsAreInstancesOfType(_returnedMethods, typeof(NUnitTestMethod));
            
        }
    }
}