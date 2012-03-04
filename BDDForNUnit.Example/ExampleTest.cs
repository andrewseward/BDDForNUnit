using System;
using NUnit.Framework;

namespace BDDForNUnit.Example
{
    [BDDTestFixture]
    public class ExampleTest
    {
        [Given]
        public void GivenIHaveWrittenGivenToTheConsole()
        {
            Console.WriteLine("the GIVEN method runs...");
        }

        [When]
        public void WhenIWriteWhenToTheConsole()
        {
            Console.WriteLine("the WHEN method runs...");
        }

        [Then]
        public void ThenMyFirstTestPasses()
        {
            Assert.Pass("My TEST passes!");
        }

        [Then]
        public void ThenMySecondTestPasses()
        {
            Assert.Pass("My second TEST passes!");
        }

        [Then]
        public void ThenMyThirdTestFails()
        {
            Assert.Fail("My third TEST fails!");
        }

        [Then]
        public void ThenMyFourthTestFailsBecauseOfAnInequality()
        {
            Assert.That("cat", Is.EqualTo("dog"));
        }
    }
}
