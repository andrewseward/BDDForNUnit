
using BDDForNUnit.Attributes;
using NUnit.Framework;

namespace GivenWhenThenForNUnit.Test
{
    [BDDTestFixture]
    public class BDDTestFixtureTestClass
    {}

    [TestFixture]
    public class BDDSuiteBuilderTests
    {
        [SetUp]
        public void GivenClassWithBDDTestFixtureAttributeWhenCanBuildFromIsCalled()
        {
            //var bddSuiteBuilder = new BDDSuiteBuilder();
            

        }
    }
}