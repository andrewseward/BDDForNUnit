using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using NUnit.Core;

namespace BDDForNUnit
{
    public class TypeManager : ITypeManager
    {
        private readonly IReflectionProvider _reflectionProvider;
        private readonly ITestDescriber _testDescriber;

        public TypeManager(IReflectionProvider reflectionProvider, ITestDescriber testDescriber)
        {
            _reflectionProvider = reflectionProvider;
            _testDescriber = testDescriber;
        }

        public BDDNUnitTestMethod[] GetNUnitTestMethodsWithAttribute(Type fixtureType, Type attributeType)
        {
            var nUnitTestMethods = new List<BDDNUnitTestMethod>();
            foreach (MethodInfo method in fixtureType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (method.GetCustomAttributes(false).Any(attr=>attr.GetType().Equals(attributeType)))
                {
                    nUnitTestMethods.Add(new BDDNUnitTestMethod(method, attributeType, _reflectionProvider, _testDescriber));
                }
            }

            return nUnitTestMethods.ToArray();
        }
    }
}