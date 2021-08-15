using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TrelloAutomation.API.API.Common.Settings;

namespace TrelloAutomation.API.API.Tests.Controllers.TestBaseTests
{
    [TestClass]
    public class ConfigurationTests : TestBase
    {
        [TestMethod]
        public async Task ConfigurationRoot_OK()
        {
            Assert.IsNotNull(_configurationRoot);
        }

        [TestMethod]
        public async Task AppSettingsIConfiguration_OK()
        {
            Assert.IsNotNull(_configurationRoot);

            var appSettings = _configurationRoot.GetSection(nameof(AppSettings));
            Assert.IsNotNull(appSettings);
        }

        [TestMethod]
        public async Task AppSettings_OK()
        {
            Assert.IsNotNull(_configurationRoot);

            var iConfiguration = _configurationRoot.GetSection(nameof(AppSettings));
            Assert.IsNotNull(iConfiguration);

            var appSettings = iConfiguration.Get<AppSettings>();
            Assert.IsNotNull(appSettings);
        }
    }
}
