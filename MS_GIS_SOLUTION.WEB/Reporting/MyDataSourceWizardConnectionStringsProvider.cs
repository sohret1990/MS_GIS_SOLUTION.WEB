using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Native;
using DevExpress.DataAccess.Web;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
// ...

namespace MS_GIS_SOLUTION.WEB.Reporting
{
    public class MyDataSourceWizardConnectionStringsProvider : IDataSourceWizardConnectionStringsProvider
    {
        public Dictionary<string, string> GetConnectionDescriptions()
        {
            Dictionary<string, string> connections = AppConfigHelper.GetConnections().Keys.ToDictionary(x => x, x => x);

            // Customize the loaded connections list. 
            connections.Remove("ReportModel");
            connections.Add("ReportModel", "Custom SQL Connection");
            return connections;
        }


        public string ConnectionString
        {
            get
            {
                IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
                return configuration.GetConnectionString("ReportModel");
            }
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString: ConnectionString);
            // Return custom connection parameters for the custom connection. 
            if (name == "ReportModel")
            {
                return new MsSqlConnectionParameters(sqlConnection.DataSource, sqlConnection.Database, sqlConnection.Credential.UserId, sqlConnection.Credential.Password.ToString(), MsSqlAuthorizationType.SqlServer);
            }

            return AppConfigHelper.LoadConnectionParameters(name);
        }
    }
}
