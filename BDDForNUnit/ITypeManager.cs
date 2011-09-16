using System;
using NUnit.Core;

namespace BDDForNUnit
{
    public interface ITypeManager
    {
        BDDNUnitTestMethod[] GetNUnitTestMethodsWithAttribute(Type fixtureType, Type attributeType);
    }
}