﻿using System;
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

            InvokeMethodsWithAttribute(typeof(GivenAttribute));

            InvokeMethodsWithAttribute(typeof(WhenAttribute));

        }

        private void InvokeMethodsWithAttribute(Type attributeType)
        {
            var methods = _typeManager.GetNUnitTestMethodsWithAttribute(FixtureType, attributeType);

            foreach (var nUnitTestMethod in methods)
            {
                _reflectionProvider.InvokeMethod(nUnitTestMethod.Method, nUnitTestMethod.Fixture);
            }
        }
    }
}