using System;

namespace BDDForNUnit.NUnitPlugin
{
    public class TestExceptionWriter : ITestExceptionWriter
    {
        public void WriteException(string testName, Exception exception)
        {
            Console.WriteLine("Test \"{0}\" Failed\n{1}", testName, exception);
        }
    }
}