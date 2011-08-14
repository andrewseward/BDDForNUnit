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
        private Mock<IExtensionPoint> _mockSuiteBuildersExtensionPoint;
        private Mock<IExtensionPoint> _mockTestCaseBuildersExtensionPoint;

        [SetUp]
        public void GivenBDDAddinWithExtensionPointFromExtensionHostWhenInstalled()
        {
            _mockExtensionHost = new Mock<IExtensionHost>();
            _mockSuiteBuildersExtensionPoint = new Mock<IExtensionPoint>();
            _mockExtensionHost.Setup(eh => eh.GetExtensionPoint("SuiteBuilders")).Returns(_mockSuiteBuildersExtensionPoint.Object);
            _mockTestCaseBuildersExtensionPoint = new Mock<IExtensionPoint>();
            _mockExtensionHost.Setup(eh => eh.GetExtensionPoint("TestCaseBuilders")).Returns(_mockTestCaseBuildersExtensionPoint.Object);
            var bddAddin = new BDDAddin();
            _result = bddAddin.Install(_mockExtensionHost.Object);
        }

        [Test]
        public void ThenBDDSuiteBuilderIsInstalledAgainstExtensionPoint()
        {
            _mockSuiteBuildersExtensionPoint.Verify(eh => eh.Install(It.IsAny<BDDSuiteBuilder>()));
        }

        [Test]
        public void ThenBDDTestCaseBuilderIsInstalledAgainstExtensionPoint()
        {
            _mockTestCaseBuildersExtensionPoint.Verify(eh => eh.Install(It.IsAny<BDDTestCaseBuilder>()));
        }

        [Test]
        public void ReturnsTrue()
        {
            Assert.That(_result, Is.True);
        }
    }
}