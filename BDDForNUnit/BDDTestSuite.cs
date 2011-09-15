using System;
using System.Reflection;
using BDDForNUnit.Attributes;
using NUnit.Core;

namespace BDDForNUnit
{
    public class BDDNUnitTestMethod : NUnitTestMethod
    {

        public BDDNUnitTestMethod(MethodInfo method) : base(method)
        {
        }

        public override TestResult RunTest()
        {
            //describe test
            //run given, when
            return base.RunTest();
        }
    }

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

        public BDDTestSuite(Type fixtureType) : this(new ReflectionProvider(), new TypeManager(), fixtureType)
        {
            
        }

        internal void BuildTestsFromFixtureType(Type fixtureType)
        {
            Fixture = _reflectionProvider.Construct(fixtureType);

            var methods = _typeManager.GetNUnitTestMethodsWithAttribute(fixtureType, typeof (ThenAttribute));

            foreach (var nUnitTestMethod in methods)
            {
                Add(nUnitTestMethod);
            }
        }

        public override sealed object Fixture
        {
            get { return base.Fixture; }
            set { base.Fixture = value; }
        }

        internal void RunExecuteActions()
        {
            ExecuteActions(ActionLevel.Test, ActionPhase.Before);
        }

        protected override void ExecuteActions(ActionLevel level, ActionPhase phase)
        {

            InvokeMethodsWithAttribute(typeof(GivenAttribute));

            InvokeMethodsWithAttribute(typeof(WhenAttribute));

            base.ExecuteActions(level, phase);
        }


        private void InvokeMethodsWithAttribute(Type attributeType)
        {
            var methods = _typeManager.GetNUnitTestMethodsWithAttribute(FixtureType, attributeType);

            foreach (var nUnitTestMethod in methods)
            {
                _reflectionProvider.InvokeMethod(nUnitTestMethod.Method, Fixture);
            }
        }
    }
}