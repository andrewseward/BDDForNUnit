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
            var builders = host.GetExtensionPoint("SuiteBuilders");
            if (builders == null)
                return false;

            builders.Install(new BDDSuiteBuilder(new ReflectionProvider())); //this implments both interfaces
            return true;
        }
    }
}
