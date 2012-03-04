using System;

namespace BDDForNUnit.NUnitPlugin
{
    public interface ITestExceptionWriter
    {
        void WriteException(string testName, Exception exception);
    }
}