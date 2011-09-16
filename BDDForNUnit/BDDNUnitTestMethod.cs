using System;
using System.Reflection;
using NUnit.Core;

namespace BDDForNUnit
{
    public class BDDNUnitTestMethod : NUnitTestMethod
    {
        private readonly IReflectionProvider _reflectionProvider;
        private readonly ITestDescriber _testDescriber;
        internal Type TestTypeAttribute { get; private set; }

        public BDDNUnitTestMethod(MethodInfo method, Type testTypeAttribute, IReflectionProvider reflectionProvider, ITestDescriber testDescriber)
            : base(method)
        {
            _reflectionProvider = reflectionProvider;
            _testDescriber = testDescriber;
            TestTypeAttribute = testTypeAttribute;
            GivenMethods = new BDDNUnitTestMethod[0];
            WhenMethods = new BDDNUnitTestMethod[0];
        }

        public BDDNUnitTestMethod[] GivenMethods { get; set; }

        public BDDNUnitTestMethod[] WhenMethods { get; set; }

        public override TestResult RunTest()
        {
            _testDescriber.WriteDescription(TestName.Name, GivenMethods, WhenMethods);
            InvokeMethods(GivenMethods);
            InvokeMethods(WhenMethods);
            return base.RunTest();
        }

        private void InvokeMethods(BDDNUnitTestMethod[] testMethods)
        {
            foreach (var nUnitTestMethod in testMethods)
            {
                _reflectionProvider.InvokeMethod(nUnitTestMethod.Method, Fixture);
            }
        }
    }
}