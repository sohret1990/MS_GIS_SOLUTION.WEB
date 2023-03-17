
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Models;
using System.IO;
using System.Linq;

namespace MS_GIS_SOLUTION.Components
{
    public class ApplicationSettingsViewComponent : ViewComponent
    {
        public ApplicationSettingsVM SettingsVM { get; set; }
        public ApplicationSettingsViewComponent(MsGisDbContext db, IHostingEnvironment hostingEnvironment)
        {
            SettingsVM = new ApplicationSettingsVM();
            SettingsVM.ErpSettingsFieldType = db.Asqueryable<ErpSettingsFieldType>().Where(x => x.Code == "Boolean").ToList();
            var fieldIds = SettingsVM.ErpSettingsFieldType.Select(x => x.Id).Distinct().ToList();
            SettingsVM.ErpSettingsGroup = db.Asqueryable<ErpSettingsGroup>().Where(x => x.Status == 1 && x.ErpSettings.Any(z => z.IsDeleted == 0
            && z.Status == 1 && z.ErpSettingsFieldType.Code == "Boolean")).ToList();
            var groupIds = SettingsVM.ErpSettingsGroup.Select(x => x.Id).Distinct().ToList();
            SettingsVM.ErpSettings = db.Asqueryable<ErpSettings>().Where(x => x.Status == 1 && groupIds.Contains(x.GroupId) && fieldIds.Contains(x.FieldTypeId)).ToList();


            string path = Path.Combine(hostingEnvironment.WebRootPath, "css", "devextreme");
            SettingsVM.ApplicationThemes = Directory.GetFiles(path).ToArray().Select(x => x.Remove(0, x.LastIndexOf(@"\") + 1)).ToList();
            SettingsVM.Theme = db.Find<ErpSettings>(x => x.Code == "#APPLICATION_THEME#" && x.ErpSettingsGroup.Code == "#ERP#")?.Value ?? "dx.light.compact.css";
        }

        public IViewComponentResult Invoke()
        {
            return View(SettingsVM);
        }
    }
}
