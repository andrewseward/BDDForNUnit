using System;
using System.Reflection;
using NUnit.Core;

namespace BDDForNUnit
{
    public class ReflectionProvider : IReflectionProvider
    {
        public object Construct(Type fixtureType)
        {
            return Reflect.Construct(fixtureType);
        }

        public bool HasAttribute(ICustomAttributeProvider member, string attrName, bool inherit)
        {
            return Reflect.HasAttribute(member, attrName, inherit);
        }

        public void InvokeMethod(MethodInfo method, object fixture)
        {
            Reflect.InvokeMethod(method, fixture);
        }
    }
}