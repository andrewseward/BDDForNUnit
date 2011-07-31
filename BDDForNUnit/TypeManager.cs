using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using NUnit.Core;

namespace BDDForNUnit
{
    public class TypeManager : ITypeManager
    {
        public NUnitTestMethod[] GetNUnitTestMethodsWithAttribute(Type fixtureType, Type attributeType)
        {
            var nUnitTestMethods = new List<NUnitTestMethod>();
            foreach (MethodInfo method in fixtureType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                if (method.GetCustomAttributes(false).Any(attr=>attr.GetType().Equals(attributeType)))
                {
                    nUnitTestMethods.Add(new NUnitTestMethod(method));
                }
            }

            return nUnitTestMethods.ToArray();
        }
    }
}