using BDDForNUnit.Attributes;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class ReflectionProviderTests_ClassWithDesiredAttributeHasAtrribute
    {
        private bool _returnedValue;

        [SetUp]
        public void GivenATypeWithTheDesiredAttributeWhenHasAttributeIsCalled()
        {
            var reflectionProvider = new ReflectionProvider();
            _returnedValue = reflectionProvider.HasAttribute(typeof(Tests), typeof(BDDTestFixtureAttribute), false);
        }

        [Test]
        public void ThenTrueIsReturned()
        {
            Assert.That(_returnedValue, Is.True);
        }
    }

    [TestFixture]
    public class ReflectionProviderTests_ClassWithoutDesiredAttributeHasAtrributeReturnsFalse
    {
        private bool _returnedValue;

        [SetUp]
        public void GivenATypeWithoutTheDesiredAttributeWhenHasAttributeIsCalled()
        {
            var reflectionProvider = new ReflectionProvider();
            _returnedValue = reflectionProvider.HasAttribute(typeof(StandardTests), typeof(BDDTestFixtureAttribute), false);
        }

        [Test]
        public void ThenFalseIsReturned()
        {
            Assert.That(_returnedValue, Is.False);
        }
    }

    [TestFixture]
    public class ReflectionProviderTests_ConstructCreatesClassOfGivenType
    {
        private object _returnedObject;

        [SetUp]
        public void GivenATypeWithoutTheDesiredAttributeWhenHasAttributeIsCalled()
        {
            var reflectionProvider = new ReflectionProvider();
            _returnedObject = reflectionProvider.Construct(typeof (Tests));
        }

        [Test]
        public void ThenAnInstanceOfTheTestClassIsReturned()
        {
            Assert.That(_returnedObject, Is.InstanceOf<Tests>());
        }
    }
}