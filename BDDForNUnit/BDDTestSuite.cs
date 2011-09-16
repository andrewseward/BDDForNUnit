using System;
using System.Collections;
using BDDForNUnit.Attributes;
using NUnit.Core;
using NUnit.Framework;

namespace BDDForNUnit
{
    public class BDDTestSuite : TestSuite
    {
        private readonly IReflectionProvider _reflectionProvider;
        private readonly ITypeManager _typeManager;

        public BDDTestSuite(IReflectionProvider reflectionProvider, ITypeManager typeManager, Type fixtureType) : base(fixtureType)
        {
            _reflectionProvider = reflectionProvider;
            _typeManager = typeManager;
            BuildTestsFromFixtureType(fixtureType);
        }

        public BDDTestSuite(Type fixtureType)
            : this(new ReflectionProvider(), new TypeManager(new ReflectionProvider(), new TestDescriber(new TestDescriptionWriter())), fixtureType)
        {
            
        }

        internal void BuildTestsFromFixtureType(Type fixtureType)
        {
            Fixture = _reflectionProvider.Construct(fixtureType);

            var methods = _typeManager.GetNUnitTestMethodsWithAttribute(fixtureType, typeof (ThenAttribute));
            var givenMethods = _typeManager.GetNUnitTestMethodsWithAttribute(FixtureType, typeof(GivenAttribute));
            var whenMethods = _typeManager.GetNUnitTestMethodsWithAttribute(FixtureType, typeof(WhenAttribute));
            foreach (var nUnitTestMethod in methods)
            {
                nUnitTestMethod.GivenMethods = givenMethods;
                nUnitTestMethod.WhenMethods = whenMethods;
                Add(nUnitTestMethod);
            }
        }

        public override sealed object Fixture
        {
            get { return base.Fixture; }
            set { base.Fixture = value; }
        }
        
        
    }
}