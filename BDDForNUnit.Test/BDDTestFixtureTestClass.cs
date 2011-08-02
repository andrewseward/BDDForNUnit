using BDDForNUnit.Attributes;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [BDDTestFixture]
    public class BDDTestFixtureTestClass
    {
        [Given]
        public void GivenMethod1()
        {
        }

        [When]
        public void WhenMethod1()
        {
        }

        [Then]
        public void TestMethod1()
        {
        }

        [Then]
        public void TestMethod2()
        {
        }

        [Test]
        public void TestMethodNotThen()
        {
        }

    }
}