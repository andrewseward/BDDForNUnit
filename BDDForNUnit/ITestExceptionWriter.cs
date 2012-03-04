using System;

namespace BDDForNUnit
{
    public interface ITestExceptionWriter
    {
        void WriteException(string testName, Exception exception);
    }
}