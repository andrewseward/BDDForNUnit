﻿using System;
using System.Reflection;

namespace BDDForNUnit
{
    public interface IReflectionProvider
    {
        object Construct(Type fixtureType);
        bool HasAttribute(ICustomAttributeProvider member, Type attributeType, bool inherit);
        void InvokeMethod(MethodInfo method, object fixture);
    }
}