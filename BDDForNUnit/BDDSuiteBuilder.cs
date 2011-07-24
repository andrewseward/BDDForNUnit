using System;
using System.Reflection;
using GivenWhenThenForNUnit;
using NUnit.Core;
using NUnit.Core.Extensibility;

namespace BDDForNUnit
{
    public class BDDSuiteBuilder : ISuiteBuilder
    {
        public bool CanBuildFrom(Type type)
        {
            return Reflect.HasAttribute(type, "GivenWhenThenForNUnit.Attributes.BDDTestFixtureAttribute", false);
        }

        public Test BuildFrom(Type type)
        {
            return new GivenWhenThenTestSuite(type);
        }
    }
    //http://www.simple-talk.com/content/print.aspx?article=484
    public class GivenWhenThenTestSuite : TestSuite
    {
        private readonly IReflectionProvider _reflectionProvider;


        public GivenWhenThenTestSuite(Type fixtureType) : base(fixtureType)
        {
            _reflectionProvider = new ReflectionProvider();
            Fixture = _reflectionProvider.Construct(fixtureType);//Reflect.Construct(fixtureType);
            BuildTestsFromFixtureType(fixtureType);
        }

        internal void BuildTestsFromFixtureType(Type fixtureType)
        {
            foreach (MethodInfo method in fixtureType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                foreach (object attr in method.GetCustomAttributes(false))
                {
                    var wrrrd = attr.GetType().ToString();

                    Add(new NUnitTestMethod(method));
                }
            }
        }

        public override sealed object Fixture
        {
            get { return base.Fixture; }
            set { base.Fixture = value; }
        }

        protected override void DoOneTimeSetUp(TestResult suiteResult)
        {
            base.DoOneTimeSetUp(suiteResult);
            
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