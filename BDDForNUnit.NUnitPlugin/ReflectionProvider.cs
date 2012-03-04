using System;
using System.Reflection;
using NUnit.Core;

namespace BDDForNUnit.NUnitPlugin
{
    public class ReflectionProvider : IReflectionProvider
    {
        public object Construct(Type fixtureType)
        {
            return Reflect.Construct(fixtureType);
        }

        public bool HasAttribute(ICustomAttributeProvider member, Type attributeType, bool inherit)
        {
            var attributes = member.GetCustomAttributes(attributeType, inherit);
            return attributes.Length > 0;
        }
        

        public void InvokeMethod(MethodInfo method, object fixture)
        {
            Reflect.InvokeMethod(method, fixture);
        }
    }
}