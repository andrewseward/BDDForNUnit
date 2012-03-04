using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace BDDForNUnit.NUnitPlugin
{
    public class TypeManager : ITypeManager
    {
        private readonly IReflectionProvider _reflectionProvider;
        private readonly ITestDescriber _testDescriber;
        private readonly ITestExceptionWriter _testExceptionWriter;

        public TypeManager(IReflectionProvider reflectionProvider, ITestDescriber testDescriber, ITestExceptionWriter testExceptionWriter)
        {
            _reflectionProvider = reflectionProvider;
            _testDescriber = testDescriber;
            _testExceptionWriter = testExceptionWriter;
        }

        public BDDNUnitTestMethod[] GetNUnitTestMethodsWithAttribute(Type fixtureType, Type attributeType)
        {
            var nUnitTestMethods = new List<BDDNUnitTestMethod>();
            foreach (MethodInfo method in fixtureType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (method.GetCustomAttributes(false).Any(attr=>attr.GetType() == attributeType))
                {
                    nUnitTestMethods.Add(new BDDNUnitTestMethod(method, attributeType, _reflectionProvider, _testDescriber, _testExceptionWriter));
                }
            }

            return nUnitTestMethods.ToArray();
        }
    }
}