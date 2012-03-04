﻿using System.Collections.Generic;

namespace BDDForNUnit.NUnitPlugin
{
    public interface ITestDescriber
    {
        void WriteDescription(string testName, IEnumerable<BDDNUnitTestMethod> givenMethods, IEnumerable<BDDNUnitTestMethod> whenMethods);
    }
}