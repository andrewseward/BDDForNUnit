using System;
using BDDForNUnit.Attributes;
using NUnit.Core;

namespace BDDForNUnit
{
    public class TestDescriber : ITestDescriber
    {
        private readonly ITestDescriptionWriter _testDescriptionWriter;
        private readonly ITypeManager _typeManager;

        public TestDescriber(ITestDescriptionWriter testDescriptionWriter, ITypeManager typeManager)
        {
            _testDescriptionWriter = testDescriptionWriter;
            _typeManager = typeManager;
        }

        public void WriteDescription(Test test)
        {
            DescribeMethodsWithAttribute(test.FixtureType, typeof (GivenAttribute));
            DescribeMethodsWithAttribute(test.FixtureType, typeof(WhenAttribute));
            _testDescriptionWriter.Write(test.TestName.Name, GetAttributeKeyword(typeof(ThenAttribute)));
        }

        private void DescribeMethodsWithAttribute(Type fixtureType, Type attributeType)
        {
            var methods = _typeManager.GetNUnitTestMethodsWithAttribute(fixtureType, attributeType);

            foreach (var nUnitTestMethod in methods)
            {
                _testDescriptionWriter.Write(nUnitTestMethod.Method.Name, GetAttributeKeyword(attributeType));
            }
        }

        private static string GetAttributeKeyword(Type attributeType)
        {
            return attributeType.Name.Substring(0, attributeType.Name.LastIndexOf("Attribute"));
        }
    }
}