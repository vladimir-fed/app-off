using Grace.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAO.Tests
{
    public class DIFixture : IDisposable
    {
        public DependencyInjectionContainer DIContainer {get; }

        public DIFixture()
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            DIContainer = new DependencyInjectionContainer();
            
            DIContainer.Configure(c => c.ExportInstance(configuration).As<IConfiguration>().Lifestyle.Singleton());
            DIContainer.Registrate(configuration);
        }

        public void Dispose()
        {
            DIContainer.Dispose();
        }
    }
}
