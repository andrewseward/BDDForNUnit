using System.Collections.Generic;
using System.Linq;
using System.Text;
using BDDForNUnit;
using NUnit.Core.Extensibility;


namespace GivenWhenThenForNUnit
{
    [NUnitAddin(Description = "Just testing", Name = "Andrewtest", Type = ExtensionType.Core)]
    public class Addin : IAddin 
    {
        public bool Install(IExtensionHost host)
        {
            var builders = host.GetExtensionPoint("SuiteBuilders");
            if (builders == null)
                return false;

            builders.Install(new BDDSuiteBuilder()); //this implments both interfaces
            return true;
        }
    }
}
