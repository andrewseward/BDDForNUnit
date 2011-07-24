using System;

namespace GivenWhenThenForNUnit
{
    public interface IReflectionProvider
    {
        object Construct(Type fixtureType);
    }
}