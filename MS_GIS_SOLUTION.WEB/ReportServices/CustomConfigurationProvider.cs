using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreWizardCustomization
{
    public class CustomConfigurationProvider
    {
        [System.Obsolete]
        readonly IHostingEnvironment hostingEnvironment;

        [System.Obsolete]
        public CustomConfigurationProvider(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        [System.Obsolete]
        public IConfigurationSection GetSqlConnectionStrings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build()
                .GetSection("ConnectionStrings");
        }

        [System.Obsolete]
        public IConfigurationSection GetJsonConnectionStrings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build()
                .GetSection("JsonConnectionStrings");
        }

        [System.Obsolete]
        public IConfigurationSection GetErpConnectionStrings()
        {
            return new ConfigurationBuilder()
                .SetBasePath(hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build()
                .GetSection("ReportModel");
        }

        [System.Obsolete]
        public IDictionary<string, string> GetGlobalConnectionStrings()
        {
            var sqlConnectionStrings = GetSqlConnectionStrings().AsEnumerable(true).ToDictionary(x => "ConnectionStrings:" + x.Key, x => x.Value);
            var jsonConnectionStrings = GetJsonConnectionStrings().AsEnumerable(true).ToDictionary(x => "ConnectionStrings:" + x.Key, x => x.Value);
            var erpConnectionStrings = GetJsonConnectionStrings().AsEnumerable(true).ToDictionary(x => "ConnectionStrings:" + x.Key, x => x.Value);
            return new ConfigurationBuilder()
              .SetBasePath(hostingEnvironment.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              //.AddInMemoryCollection(sqlConnectionStrings)
              //.AddInMemoryCollection(jsonConnectionStrings)
              .AddInMemoryCollection(erpConnectionStrings)
              .AddEnvironmentVariables()
              .Build()
              .GetSection("ConnectionStrings")
              .AsEnumerable(true)
              .Where(x => x.Key == "ReportModel")
              .ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
