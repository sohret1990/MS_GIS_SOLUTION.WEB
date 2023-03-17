using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ObjectAdminUnitBarrierTypeController : BaseController
    {
        public ObjectAdminUnitBarrierTypeController(MsGisDbContext db) : base(db)
        {

        }

        [HttpGet]
        public object LoadBarrierTypesByAdminUnit(int AdminUnitId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<DObjectAdminUnitBarrierType>().Where(x => x.FkIdObjectAdminUnit == AdminUnitId), loadOptions);
        }

        [HttpGet]
        public object Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<DObjectAdminUnitBarrierType>(), loadOptions);
        }



        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var hd = _db.Asqueryable<DObjectAdminUnitBarrierType>().FirstOrDefault(x => x.IdOauBarrierType == key);

            if (hd == null)
            {
                hd = new DObjectAdminUnitBarrierType();
            }
            JsonConvert.PopulateObject(values, hd);

            if (key == 0)
            {
                hd.GlobalId = Guid.NewGuid();
                hd.Objectid = (_db.AsqueryableWithoutIsDeleted<DObjectAdminUnitBarrierType>().Max(x => x.Objectid) + 1);

                _db.Insert<DObjectAdminUnitBarrierType>(hd);

            }
            else
            {

                _db.Update<DObjectAdminUnitBarrierType>(hd);

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
                var dObjectAdminUnitBarrier = _db.Asqueryable<DObjectAdminUnitBarrierType>().First(x => x.IdOauBarrierType == key);
                _db.Delete(dObjectAdminUnitBarrier);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Əlaqəli bağlantı var və öncə onu silməyiniz gərəklidir.");
            }

            return Ok();
        }

    }
}
