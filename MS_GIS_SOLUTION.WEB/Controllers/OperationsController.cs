using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class OperationsController : BaseController
    {
        public OperationsController(MsGisDbContext db) : base(db)
        {

        }

        public object Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<ErpOperation>(_db.Asqueryable<ErpOperation>(), loadOptions);
        }

        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var role = _db.Find<ErpOperation>(x => x.Id == key);

            if (role == null)
            {
                role = new ErpOperation();
            }

            JsonConvert.PopulateObject(values, role);

            if (key == 0)
            {
                if (_db.Asqueryable<ErpOperation>().Any(x => x.Name.ToUpper() == role.Name.Trim().ToUpper()))
                {
                    return BadRequest("Eyni adlı əməliyyat bazada mövcuddur");
                }
                else
                {
                    _db.Insert<ErpOperation>(role);
                }
            }
            else
            {
                if (_db.Asqueryable<ErpOperation>().Any(x => x.Name.ToUpper() == role.Name.Trim().ToUpper() && x.Id != role.Id))
                {
                    return BadRequest("Eyni adlı əməliyyat bazada mövcuddur");
                }
                else
                {
                    _db.Update<ErpOperation>(role);
                }

            }

            _db.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            _db.DeleteById<ErpOperation>(key);

            try
            {
                _db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

    }
}
