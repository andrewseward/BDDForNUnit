using System;
using System.IO;
using BDDForNUnit.NUnitPlugin;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class TestExceptionWriterTests
    {
        private Exception _testException;
        private string _textInConsole;
        private string _testName;

        [SetUp]
        public void GivenAnExceptionAndTestExceptionWriterWhenWriteDescription()
        {
            _testException = TestExceptionHelper.GenerateException();
             _testName = "My Test Name";
            var testExceptionWriter = new TestExceptionWriter();

            using (var sw = new StringWriter())
            {
                
                Console.SetOut(sw);

                testExceptionWriter.WriteException(_testName, _testException);

                _textInConsole = sw.ToString();
            }

        }

        [Test]
        public void ThenTestNameIsOutputToConsole()
        {
            Assert.That(_textInConsole, Is.StringContaining(_testName));
        }

        [Test]
        public void ThenExceptionMessageIsOutputToConsole()
        {
            Assert.That(_textInConsole, Is.StringContaining(_testException.Message));
        }

        [Test]
        public void ThenExceptionStackTraceIsOutputToConsole()
        {
            Assert.That(_textInConsole, Is.StringContaining(_testException.StackTrace));
        }
    }
}