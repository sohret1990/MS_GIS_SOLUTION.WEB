using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using Newtonsoft.Json;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ApplicationSettingsController : BaseController
    {
        public ApplicationSettingsController(MsGisDbContext db) : base(db)
        {

        }

        public object Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpSettings>().Where(x => x.ErpSettingsFieldType.IsDeleted == 0 && x.ErpSettingsFieldType.Status == 1 && x.ErpSettingsGroup.IsDeleted == 0 && x.ErpSettingsGroup.Status == 1), loadOptions);
        }

        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var sg = _db.Find<ErpSettings>(x => x.Id == key);
            if (key == 0)
            {
                sg = new ErpSettings();
            }

            if (sg == null)
            {
                return BadRequest("Məlumat tapılmadı!");
            }

            JsonConvert.PopulateObject(values, sg);

            //if (_db.Find<ErpSettings>(x => (x.Name == sg.Name || x.Code == sg.Code) && x.Id != sg.Id) != null)
            //{
            //    return BadRequest("Eyni adla və ya kodla məlumat bazada mövcuddur!");
            //}

            if (sg.Id == 0)
            {
                _db.Insert(sg);
            }
            else
            {
                _db.Update(sg);
            }
            _db.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            _db.DeleteById<ErpSettings>(key);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult ChangeSettingsValue(int key, bool value)
        {
            var setting = _db.Find<ErpSettings>(x => x.Id == key);
            setting.Value = value.ToString();
            _db.Update(setting);
            try
            {
                _db.SaveChanges();
            }
            catch
            {
                return BadRequest("Əməliyyat zamanı xəta baş verdi!");
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult ChangeSettingsValueByCode(string code, string value)
        {
            var setting = _db.Find<ErpSettings>(x => x.Code == code);
            setting.Value = value;
            _db.Update(setting);
            try
            {
                _db.SaveChanges();
                BaseController.ThemeName = value;
            }
            catch
            {
                return BadRequest("Əməliyyat zamanı xəta baş verdi!");
            }
            return Ok();
        }
    }
}
