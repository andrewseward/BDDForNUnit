using System;
using BDDForNUnit.Attributes;
using NUnit.Framework;

namespace BDDForNUnit.Example
{
    [BDDTestFixture]
    public class ExampleTest
    {
        [Given]
        public void GivenIHaveWrittenGivenToTheConsole()
        {
            Console.WriteLine("GIVEN");
        }

        [When]
        public void WhenIWriteWhenToTheConsole()
        {
            Console.WriteLine("WHEN");
        }

        [Then]
        public void ThenMyTestPasses()
        {
            Console.WriteLine("THEN");
            Assert.Pass("MY TEST PASSES!");
        }

        [Then]
        public void ThenMyOtherTestPasses()
        {
            Console.WriteLine("THEN2");
            Assert.Pass("MY TEST PASSES!");
        }
    }
}
