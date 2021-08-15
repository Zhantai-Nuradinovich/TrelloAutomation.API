﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using TrelloAutomation.API.API.Common.Settings;
using TrelloAutomation.API.IoC.Configuration.DI;
using TrelloAutomation.API.Tools.Configurations;

namespace TrelloAutomation.API.API.Tests.Controllers
{
    [TestClass]
    public class TestBase
    {
        internal IConfigurationRoot _configurationRoot;
        internal ServiceCollection _services;
        internal ServiceProvider _serviceProvider;

        public TestBase()
        {
            _configurationRoot = ConfigurationHelper.GetIConfigurationRoot(Directory.GetCurrentDirectory());
            var appSettings = _configurationRoot.GetSection(nameof(AppSettings));

            _services = new ServiceCollection();

            //We load EXACTLY the same settings (DI and others) than API real solution, what is much better for tests.
            _services.ConfigureBusinessServices((IConfiguration)_configurationRoot);

            _services.ConfigureMappings();
            _services.AddLogging();
            _services.Configure<AppSettings>(appSettings);

            _serviceProvider = _services.BuildServiceProvider();
        }

        ~TestBase()
        {
            if (_serviceProvider != null)
                _serviceProvider.Dispose();
        }
    }
}
