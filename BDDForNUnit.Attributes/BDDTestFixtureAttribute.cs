using System;

namespace BDDForNUnit.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class BDDTestFixtureAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class GivenAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class WhenAttribute : Attribute
    {
    }


    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ThenAttribute : Attribute
    {
    }
}