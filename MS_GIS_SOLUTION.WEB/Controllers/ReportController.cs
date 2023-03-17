using DevExpress.DataAccess.Sql;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web.ReportDesigner;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System;
using System.Collections.Generic;
using System.IO;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ReportController : BaseController
    {
        public ReportController(MsGisDbContext db) : base(db)
        {

        }

        //public IActionResult Index()
        //{
        //    //var rep = _db.Find<Report>(x => x.Id == 2);
        //    //var report = new XtraReport();

        //    //using (var ms = new MemoryStream(rep.Content)) 
        //    //{
        //    //    report.LoadLayoutFromXml(ms);
        //    //}



        //    //return View(report);
        //    return View();
        //}

        [HttpGet]
        public IActionResult ReportView(int id)
        {
            var rep = _db.Find<ErpReport>(x => x.Id == id);
            var report = new XtraReport();
            if (rep != null)
            {
                using (var ms = new MemoryStream(rep.Content))
                {
                    report = XtraReport.FromXmlStream(ms, true);
                    foreach (var item in report.Parameters)
                    {
                        var tip = item.Type;

                        switch (tip.FullName)
                        {
                            case "System.DateTime":
                                item.Value = string.IsNullOrEmpty(HttpContext.Request.Query[item.Name]) ? null : Convert.ToDateTime(HttpContext.Request.Query[item.Name]);
                                break;
                            case "System.Byte":
                                item.Value = string.IsNullOrEmpty(HttpContext.Request.Query[item.Name]) ? null : Convert.ToByte(HttpContext.Request.Query[item.Name]);
                                break;
                            case "System.Int32":
                                item.Value = string.IsNullOrEmpty(HttpContext.Request.Query[item.Name]) ? null : Convert.ToInt32(HttpContext.Request.Query[item.Name]);
                                break;
                            case "System.Int16":
                                item.Value = string.IsNullOrEmpty(HttpContext.Request.Query[item.Name]) ? null : Convert.ToInt16(HttpContext.Request.Query[item.Name]);
                                break;
                            case "System.Int64":
                                item.Value = string.IsNullOrEmpty(HttpContext.Request.Query[item.Name]) ? null : Convert.ToInt64(HttpContext.Request.Query[item.Name]);
                                break;
                            case "System.Boolean":
                                item.Value = string.IsNullOrEmpty(HttpContext.Request.Query[item.Name]) ? null : Convert.ToBoolean(HttpContext.Request.Query[item.Name]);
                                break;
                            case "System.String":
                                item.Value = string.IsNullOrEmpty(HttpContext.Request.Query[item.Name]) ? null : HttpContext.Request.Query[item.Name].ToString();
                                break;
                            default:
                                item.Value = string.IsNullOrEmpty(HttpContext.Request.Query[item.Name]) ? "" : HttpContext.Request.Query[item.Name];
                                break;
                        }
                    }
                }
            }

            return View(report);
        }

        public ActionResult GetReportDesignerModel(string reportUrl)
        {
            string modelJsonScript =
                new ReportDesignerClientSideModelGenerator(HttpContext.RequestServices)
                .GetJsonModelScript(
                    reportUrl,                 // The URL of a report that is opened in the Report Designer when the application starts.
                    GetAvailableDataSources(), // Available data sources in the Report Designer that can be added to reports.
                    "DXXRD",   // The URI path of the default controller that processes requests from the Report Designer.
                    "DXXRDV",// The URI path of the default controller that that processes requests from the Web Document Viewer.
                    "DXXQB"      // The URI path of the default controller that processes requests from the Query Builder.
                );
            return Content(modelJsonScript, "application/json");
        }

        Dictionary<string, object> GetAvailableDataSources()
        {
            var dataSources = new Dictionary<string, object>();
            SqlDataSource ds = new SqlDataSource("ReportModel");
            var query = SelectQueryFluentBuilder.AddTable("Product").SelectAllColumns().Build("Product");
            ds.Queries.Add(query);
            ds.RebuildResultSchema();
            dataSources.Add("SqlDataSource", ds);
            return dataSources;
        }
    }
}
