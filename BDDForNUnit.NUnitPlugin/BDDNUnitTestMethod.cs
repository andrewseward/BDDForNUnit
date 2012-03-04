using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Core;

namespace BDDForNUnit.NUnitPlugin
{
    public class BDDNUnitTestMethod : NUnitTestMethod
    {
        private readonly IReflectionProvider _reflectionProvider;
        private readonly ITestDescriber _testDescriber;
        private readonly ITestExceptionWriter _testExceptionWriter;
        internal Type TestTypeAttribute { get; private set; }

        public BDDNUnitTestMethod(MethodInfo method, Type testTypeAttribute, IReflectionProvider reflectionProvider, ITestDescriber testDescriber, ITestExceptionWriter testExceptionWriter)
            : base(method)
        {
            _reflectionProvider = reflectionProvider;
            _testDescriber = testDescriber;
            _testExceptionWriter = testExceptionWriter;
            TestTypeAttribute = testTypeAttribute;
            GivenMethods = new BDDNUnitTestMethod[0];
            WhenMethods = new BDDNUnitTestMethod[0];
        }

        public BDDNUnitTestMethod[] GivenMethods { get; set; }

        public BDDNUnitTestMethod[] WhenMethods { get; set; }

        public override TestResult RunTest()
        {
            try
            {
                _testDescriber.WriteDescription(TestName.Name, GivenMethods, WhenMethods);
                InvokeMethods(GivenMethods);
                InvokeMethods(WhenMethods);
                return base.RunTest();
            }
            catch (Exception exception)
            {
                _testExceptionWriter.WriteException(TestName.Name, exception);
                throw;
            }
        }

        private void InvokeMethods(IEnumerable<BDDNUnitTestMethod> testMethods)
        {
            foreach (var nUnitTestMethod in testMethods)
            {
                _reflectionProvider.InvokeMethod(nUnitTestMethod.Method, Fixture);
            }
        }
    }
}