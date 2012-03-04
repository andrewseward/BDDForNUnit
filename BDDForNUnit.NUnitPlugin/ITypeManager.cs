using System;

namespace BDDForNUnit.NUnitPlugin
{
    public interface ITypeManager
    {
        BDDNUnitTestMethod[] GetNUnitTestMethodsWithAttribute(Type fixtureType, Type attributeType);
    }
}