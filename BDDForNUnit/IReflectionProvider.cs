using System;
using System.Reflection;

namespace BDDForNUnit
{
    public interface IReflectionProvider
    {
        object Construct(Type fixtureType);
        bool HasAttribute(ICustomAttributeProvider member, string attrName, bool inherit);
        void InvokeMethod(MethodInfo method, object fixture);
    }
}