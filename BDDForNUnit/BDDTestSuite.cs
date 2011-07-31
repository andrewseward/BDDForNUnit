using System;
using System.Reflection;
using BDDForNUnit.Attributes;
using NUnit.Core;

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

        internal void RunDoOneTimeSetUp(TestResult suiteResult)
        {
            DoOneTimeSetUp(suiteResult);
        }

        protected override void DoOneTimeSetUp(TestResult suiteResult)
        {
            base.DoOneTimeSetUp(suiteResult);

            /*var methods = _typeManager.GetNUnitTestMethodsWithAttribute(FixtureType, typeof(ThenAttribute));

            foreach (var nUnitTestMethod in methods)
            {
                Add(nUnitTestMethod);
            }*/

            //run given and when somehow
            //FixtureType.get

            /*
             * HERE'S HOW!!
             * object[] arguments = _arguments ?? new object[] {null};
    TestSuite testSuite = (TestSuite) this.Parent.Parent;
    Reflect.InvokeMethod(this.Method, testSuite.Fixture, arguments); //Execute Test */
        }
    }
}