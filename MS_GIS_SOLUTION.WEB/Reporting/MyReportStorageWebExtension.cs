using DevExpress.XtraReports.Web.Extensions;
using Microsoft.Extensions.Configuration;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Reporting
{
    public class MyReportStorageWebExtension : ReportStorageWebExtension
    {
        IConfiguration _configuration;
        MsGisDbContext _db;
        public MyReportStorageWebExtension(IConfiguration configuration, MsGisDbContext db)
        {
            _configuration = configuration;
            _db = db;
        }


        public override bool CanSetData(string url)
        {
            return _db.SelectById<ErpReport>(int.Parse(url)) != null;
            //return base.CanSetData(url);
        }
        public override bool IsValidUrl(string url)
        {
            return true;
        }

        public override byte[] GetData(string url)
        {
            //   return base.GetData(url);
            int id = Convert.ToInt32(url);
            var reports = _db.SelectById<ErpReport>(int.Parse(url));
            return reports != null ? reports.Content : null;
        }

        public override Dictionary<string, string> GetUrls()
        {
            //return base.GetUrls();
            try
            {
                var reports = _db.Asqueryable<ErpReport>().Where(x => x.Status == 1).Select(m => new { m.Id, m.Name, m.Description }).ToList().ToDictionary(x => x.Id.ToString(), x => x.Id + ". " + x.Name);
                return reports;
            }
            catch (Exception ex)
            {
                SentryExceptionLogger.WriteToLog(ex);
                //Sentry.SentryClient client = new Sentry.SentryClient(new Sentry.SentryOptions
                //{
                //    Dsn = new Sentry.Dsn(_configuration.GetSection("Sentry").GetValue(typeof(string),"Dsn").ToString())
                //});

                //client.CaptureEvent(new Sentry.SentryEvent(ex));

                //File.AppendAllText(@"C:\ERP_SYSTEM\Log\log.txt", ex.ToString() + " >>INNER " + ex.InnerException);
                throw;
            }
        }

        public override void SetData(DevExpress.XtraReports.UI.XtraReport report, string url)
        {
            //base.SetData(report, url);

            try
            {
                int id = Convert.ToInt32(url);
                var rep = _db.SelectById<ErpReport>(int.Parse(url));

                MemoryStream str = new MemoryStream();
                report.SaveLayoutToXml(str);
                rep.Content = str.ToArray();
                rep.UpdateDateTime = DateTime.Now;
                rep.Status = 1;
                rep.UpdateUserId = 1;
                rep.UpdateDateTime = DateTime.Now;

                _db.Update(rep);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {

                SentryExceptionLogger.WriteToLog(ex);
                //File.AppendAllText(@"C:\ERP_SYSTEM\Log\log.txt", ex.ToString() + " >>INNER " + ex.InnerException);
                throw;
            }
        }


        public override string SetNewData(DevExpress.XtraReports.UI.XtraReport report, string defaultUrl)
        {
            //  return base.SetNewData(report, defaultUrl);


            try
            {
                ErpReport rep = new ErpReport();
                rep.Name = defaultUrl;
                MemoryStream str = new MemoryStream();



                report.SaveLayoutToXml(str);

                rep.Status = 1;
                rep.Content = str.ToArray();
                rep.InsertDateTime = DateTime.Now;
                _db.Insert(rep);
                _db.SaveChanges();
                return rep.Id.ToString();
            }
            catch (Exception ex)
            {
                SentryExceptionLogger.WriteToLog(ex);
                //File.AppendAllText(@"C:\ERP_SYSTEM\Log\log.txt", ex.ToString() + " >>INNER " + ex.InnerException);
                throw;
            }
            //return base.SetNewData(report, defaultUrl);

            //  base.SetData(report, defaultUrl);
        }
    }
}
