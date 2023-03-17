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
    public class ClosedChannelAdminUnitPipeInfoController : BaseController
    {
        public ClosedChannelAdminUnitPipeInfoController(MsGisDbContext db) : base(db)
        {

        }

        [HttpGet]
        public object LoadPipeTypesByAdminUnit(int AdminUnitId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj02ClosedChannelAdminUnitPipeInfo>().Where(x => x.FkIdClosedChannelAdminUnit == AdminUnitId), loadOptions);
        }

        [HttpGet]
        public object Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj02ClosedChannelAdminUnitPipeInfo>(), loadOptions);
        }



        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var hd = _db.Asqueryable<Obj02ClosedChannelAdminUnitPipeInfo>().FirstOrDefault(x => x.Objectid == key);

            if (hd == null)
            {
                hd = new Obj02ClosedChannelAdminUnitPipeInfo();
            }
            JsonConvert.PopulateObject(values, hd);


            if (key == 0)
            {
                hd.GlobalId = Guid.NewGuid();
                hd.Objectid = (_db.AsqueryableWithoutIsDeleted<Obj02ClosedChannelAdminUnitPipeInfo>().Max(x => x.Objectid)) + 1;
                hd.IsDeleted = 0;

                _db.Insert<Obj02ClosedChannelAdminUnitPipeInfo>(hd);

            }
            else
            {

                _db.Update<Obj02ClosedChannelAdminUnitPipeInfo>(hd);

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
                var closedChannelAdminUnitPipeInfo = _db.Asqueryable<Obj02ClosedChannelAdminUnitPipeInfo>().First(x => x.IdCcAdminUnitPipeInfo == key);
                _db.Delete(closedChannelAdminUnitPipeInfo);
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
