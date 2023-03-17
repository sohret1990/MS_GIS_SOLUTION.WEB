using DevExpress.DataAccess.Sql;

namespace AspNetCoreWizardCustomization
{
    public class Utils
    {
        public static SqlDataSource CreateDataSource()
        {
            // Create a SQL data source with the specified connection string.
            SqlDataSource ds = new SqlDataSource("ReportModel");

            // Create a SQL query to access the Products data table.
            SelectQuery query = SelectQueryFluentBuilder.AddTable("ERP_PRODUCT").SelectAllColumnsFromTable().Build("ERP_PRODUCT");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();

            return ds;
        }
    }
}
