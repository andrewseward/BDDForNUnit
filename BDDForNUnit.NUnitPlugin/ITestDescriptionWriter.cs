namespace BDDForNUnit.NUnitPlugin
{
    public interface ITestDescriptionWriter
    {
        void Write(string methodDescription, string keyword);
    }
}