using Moq;
using NUnit.Core.Extensibility;
using NUnit.Framework;

namespace BDDForNUnit.Test
{
    [TestFixture]
    public class BDDAddinTests_ExtensionHostReturnsNull
    {
        private Mock<IExtensionHost> _mockExtensionHost;
        private bool _result;

        [SetUp]
        public void GivenBDDAddinWithNullResponseFromExtensionHostWhenInstalled()
        {
            _mockExtensionHost = new Mock<IExtensionHost>();
            _mockExtensionHost.Setup(eh => eh.GetExtensionPoint(It.IsAny<string>())).Returns(null as IExtensionPoint);
            var bddAddin = new BDDAddin();
            _result = bddAddin.Install(_mockExtensionHost.Object);
        }

        [Test]
        public void ThenExtensionPointIsRetrieved()
        {
            _mockExtensionHost.Verify(eh => eh.GetExtensionPoint("SuiteBuilders"));
        }

        [Test]
        public void ReturnsFalse()
        {
            Assert.That(_result, Is.False);
        }
    }

    [TestFixture]
    public class BDDAddinTests_ExtensionHostReturnsExtensionPoint
    {
        private Mock<IExtensionHost> _mockExtensionHost;
        private bool _result;
        private Mock<IExtensionPoint> _mockExtensionPoint;

        [SetUp]
        public void GivenBDDAddinWithExtensionPointFromExtensionHostWhenInstalled()
        {
            _mockExtensionPoint = new Mock<IExtensionPoint>();
            _mockExtensionHost = new Mock<IExtensionHost>();
            _mockExtensionHost.Setup(eh => eh.GetExtensionPoint(It.IsAny<string>())).Returns(_mockExtensionPoint.Object);
            var bddAddin = new BDDAddin();
            _result = bddAddin.Install(_mockExtensionHost.Object);
        }

        [Test]
        public void ThenBDDSuiteBuilderIsInstalledAgainstExtensionPoint()
        {
            _mockExtensionPoint.Verify(eh => eh.Install(It.IsAny<BDDSuiteBuilder>()));
        }

        [Test]
        public void ReturnsTrue()
        {
            Assert.That(_result, Is.True);
        }
    }
}