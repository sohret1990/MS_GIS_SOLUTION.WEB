using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class UserInRoleController : BaseController
    {
        public UserInRoleController(MsGisDbContext db) : base(db)
        {

        }


        [HttpGet]
        public object Load(DataSourceLoadOptions loadOptions, int UserId = 0)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpUserInRole>().Where(x => x.UserId == UserId || UserId == 0), loadOptions);
        }

        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var hd = _db.Asqueryable<ErpUserInRole>().FirstOrDefault(x => x.Id == key);

            if (hd == null)
            {
                hd = new ErpUserInRole();
            }

            JsonConvert.PopulateObject(values, hd);

            if (key == 0)
            {
                _db.Insert<ErpUserInRole>(hd);
            }
            else
            {
                _db.Update<ErpUserInRole>(hd);
            }

            _db.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            if (key == 0)
            {
                return BadRequest();
            }
            try
            {
                _db.DeleteById<ErpUserInRole>(key, true);
                _db.SaveChanges();
            }
            catch
            {
                return BadRequest("Əlaqəli bağlantı var və öncə onu silməyiniz gərəklidir.");
            }

            return Ok();
        }
    }
}