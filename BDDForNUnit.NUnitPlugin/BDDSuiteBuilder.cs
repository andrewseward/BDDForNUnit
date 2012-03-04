using System;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace BDDForNUnit.NUnitPlugin
{
    public class BDDSuiteBuilder : ISuiteBuilder
    {
        private readonly IReflectionProvider _reflectionProvider;
        private readonly ITypeManager _typeManager;

        public BDDSuiteBuilder(IReflectionProvider reflectionProvider, ITypeManager typeManager) : base()
        {
            _reflectionProvider = reflectionProvider;
            _typeManager = typeManager;
        }

        public bool CanBuildFrom(Type type)
        {
            var canBuildFrom = _reflectionProvider.HasAttribute(type, typeof(BDDTestFixtureAttribute), false);
            return canBuildFrom;
        }

        public Test BuildFrom(Type type)
        {
            return new BDDTestSuite(_reflectionProvider, _typeManager, type);
        }

    }
    //http://www.simple-talk.com/content/print.aspx?article=484
}