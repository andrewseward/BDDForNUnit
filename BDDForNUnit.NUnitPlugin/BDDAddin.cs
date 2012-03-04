using NUnit.Core.Extensibility;

namespace BDDForNUnit.NUnitPlugin
{
    [NUnitAddin(Description = "An NUnit plugin which allows Given-When-Then style tests", Name = "BDDForNUnit", Type = ExtensionType.Core)]
    public class BDDAddin : IAddin 
    {
        public bool Install(IExtensionHost host)
        {
            var suiteBuildersExtensionPoint = host.GetExtensionPoint("SuiteBuilders");
            if (suiteBuildersExtensionPoint == null)
                return false;

            var reflectionProvider = new ReflectionProvider();
            var typeManager = new TypeManager(reflectionProvider, new TestDescriber(new TestDescriptionWriter()), new TestExceptionWriter());
            suiteBuildersExtensionPoint.Install(new BDDSuiteBuilder(reflectionProvider, typeManager));

            return true;
        }
    }
}
