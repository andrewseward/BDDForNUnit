using System;
using System.Reflection;
using BDDForNUnit;

namespace BDDForNUnit
{
    public class ReflectionProvider : IReflectionProvider
    {
        public object Construct(Type fixtureType)
        {
            throw new NotImplementedException();
        }

        public bool HasAttribute(ICustomAttributeProvider member, string attrName, bool inherit)
        {
            throw new NotImplementedException();
        }
    }
}