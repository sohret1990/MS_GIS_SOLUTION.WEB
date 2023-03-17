using DevExpress.DataAccess.Json;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.Web;
using DevExpress.DataAccess.Wizard.Services;
using DevExpress.XtraReports.Web.ClientControls;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace AspNetCoreWizardCustomization.Services
{
    public class CustomConfigFileDataConnectionProviderService : IJsonDataConnectionProviderService, IConnectionProviderService
    {
        readonly CustomConfigurationProvider customConfigurationProvider;
        public CustomConfigFileDataConnectionProviderService(CustomConfigurationProvider customConfigurationProvider)
        {
            this.customConfigurationProvider = customConfigurationProvider;
        }
        JsonDataConnection IJsonDataConnectionProviderService.GetJsonDataConnection(string name)
        {
            var connectionStringDictionary = customConfigurationProvider.GetJsonConnectionStrings().AsEnumerable(true).ToDictionary(x => x.Key, y => y.Value);
            if (connectionStringDictionary.ContainsKey(name))
            {
                return new JsonDataConnection(connectionStringDictionary[name]);
            }

            throw new FaultException($"Cannot find connection string: {name}.");
        }

        SqlDataConnection IConnectionProviderService.LoadConnection(string name)
        {
            var connectionStringDictionary = customConfigurationProvider.GetSqlConnectionStrings().AsEnumerable(true).Where(x => x.Key == "ReportModel").ToDictionary(x => x.Key, y => y.Value);
            if (connectionStringDictionary.ContainsKey(name))
            {
                return new SqlDataConnection
                {
                    ConnectionString = connectionStringDictionary[name]
                };
            }
            throw new FaultException($"Cannot find connection string: {name}.");
        }
    }

    public class CustomConnectionProviderFactory : IJsonDataConnectionProviderFactory, IConnectionProviderFactory
    {
        readonly CustomConfigFileDataConnectionProviderService jsonDataConnectionProviderService;
        public CustomConnectionProviderFactory(CustomConfigFileDataConnectionProviderService jsonDataConnectionProviderService)
        {
            this.jsonDataConnectionProviderService = jsonDataConnectionProviderService;
        }
        public IJsonDataConnectionProviderService Create()
        {
            return jsonDataConnectionProviderService;
        }

        IConnectionProviderService IConnectionProviderFactory.Create()
        {
            return jsonDataConnectionProviderService;
        }
    }
}
