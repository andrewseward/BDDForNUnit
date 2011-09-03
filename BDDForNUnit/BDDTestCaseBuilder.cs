using System;
using System.Reflection;
using BDDForNUnit.Attributes;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace BDDForNUnit
{
    public class BDDTestCaseBuilder : ITestCaseBuilder
    {
        private readonly IReflectionProvider _reflectionProvider;
        private readonly ITestDescriber _testDescriber;

        public BDDTestCaseBuilder(IReflectionProvider reflectionProvider, ITestDescriber testDescriber) : base()
        {
            _reflectionProvider = reflectionProvider;
            _testDescriber = testDescriber;
        }

        public bool CanBuildFrom(MethodInfo method)
        {
            var canBuildFrom = _reflectionProvider.HasAttribute(method, typeof(ThenAttribute), false);
            return canBuildFrom;
        }

        public Test BuildFrom(MethodInfo method)
        {
            var nUnitTestMethod = new NUnitTestMethod(method) {Description = "Then Test Method"};
            _testDescriber.WriteDescription(nUnitTestMethod);
            return nUnitTestMethod;
        }
    }
}