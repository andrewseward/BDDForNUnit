using System;

namespace BDDForNUnit
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