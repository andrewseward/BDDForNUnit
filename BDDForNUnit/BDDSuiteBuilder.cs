using System;
using BDDForNUnit;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace BDDForNUnit
{
    public class BDDSuiteBuilder : ISuiteBuilder
    {
        private readonly IReflectionProvider _reflectionProvider;

        public BDDSuiteBuilder(IReflectionProvider reflectionProvider) : base()
        {
            _reflectionProvider = reflectionProvider;
        }

        public bool CanBuildFrom(Type type)
        {
            return _reflectionProvider.HasAttribute(type, "GivenWhenThenForNUnit.Attributes.BDDTestFixtureAttribute", false);
        }

        public Test BuildFrom(Type type)
        {
            return new BDDTestSuite(_reflectionProvider, new TypeManager(), type);
        }
    }
    //http://www.simple-talk.com/content/print.aspx?article=484
}