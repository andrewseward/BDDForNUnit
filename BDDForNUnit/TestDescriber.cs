using System;
using System.Collections.Generic;
using BDDForNUnit.Attributes;

namespace BDDForNUnit
{
    public class TestDescriber : ITestDescriber
    {
        private readonly ITestDescriptionWriter _testDescriptionWriter;

        public TestDescriber(ITestDescriptionWriter testDescriptionWriter)
        {
            _testDescriptionWriter = testDescriptionWriter;
        }

        public void WriteDescription(string testName, IEnumerable<BDDNUnitTestMethod> givenMethods, IEnumerable<BDDNUnitTestMethod> whenMethods)
        {
            DescribeMethodsWithAttribute(givenMethods, typeof (GivenAttribute));
            DescribeMethodsWithAttribute(whenMethods, typeof(WhenAttribute));
            _testDescriptionWriter.Write(testName, GetAttributeKeyword(typeof(ThenAttribute)));
        }

        private void DescribeMethodsWithAttribute(IEnumerable<BDDNUnitTestMethod> methods, Type attributeType)
        {

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