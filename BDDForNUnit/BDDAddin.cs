using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDDForNUnit;
using NUnit.Core.Extensibility;


namespace BDDForNUnit
{
    [NUnitAddin(Description = "An NUnit plugin which allows Given-When-Then style tests", Name = "BDDForNUnit", Type = ExtensionType.Core)]
    public class BDDAddin : IAddin 
    {
        public bool Install(IExtensionHost host)
        {
            var suiteBuildersExtensionPoint = host.GetExtensionPoint("SuiteBuilders");
            if (suiteBuildersExtensionPoint == null)
                return false;

            suiteBuildersExtensionPoint.Install(new BDDSuiteBuilder(new ReflectionProvider()));

            var testCaseBuildersExtensionPoint = host.GetExtensionPoint("TestCaseBuilders");
            testCaseBuildersExtensionPoint.Install(new BDDTestCaseBuilder(new ReflectionProvider()));
            return true;
        }
    }
}
