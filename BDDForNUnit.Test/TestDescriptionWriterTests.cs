using System;
using System.IO;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class TestDescriptionWriterTestsWithDescriptionWhichStartsWithKeyword
    {
        private string _testDescription;
        private string _textInConsole;

        [SetUp]
        public void GivenATestDescriptionStartingWithTheKeywordWhenWrite()
        {
            _testDescription = "ThenTheyLiveHappilyEverAfter";
            const string keyword = "Then";

            var testDescriptionWriter = new TestDescriptionWriter();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                
                testDescriptionWriter.Write(_testDescription, keyword);

                _textInConsole = sw.ToString();
            }

        }

        [Test]
        public void ThenTestDescriptionIsOutputToConsole()
        {
            var expectedText = _testDescription + Environment.NewLine;
            Assert.That(_textInConsole, Is.EqualTo(expectedText));
        }
    }

    [TestFixture]
    public class TestDescriptionWriterTestsWithDescriptionWhichDoesNotStartWithKeyword
    {
        private const string Keyword = "Then";
        private string _testDescription;
        private string _textInConsole;

        [SetUp]
        public void GivenATestDescriptionNotStartingWithTheKeywordWhenWrite()
        {
            _testDescription = "TheyAllEatADeliciousCake";

            var testDescriptionWriter = new TestDescriptionWriter();

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                testDescriptionWriter.Write(_testDescription, Keyword);

                _textInConsole = sw.ToString();
            }

        }

        [Test]
        public void ThenTestDescriptionIsOutputToConsoleWithKeyword()
        {
            var expectedText = Keyword + _testDescription + Environment.NewLine;
            Assert.That(_textInConsole, Is.EqualTo(expectedText));
        }
    }
}