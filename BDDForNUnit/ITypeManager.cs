using System;
using NUnit.Core;

namespace BDDForNUnit
{
    public interface ITypeManager
    {
        NUnitTestMethod[] GetNUnitTestMethodsWithAttribute(Type fixtureType, Type attributeType);
    }
}