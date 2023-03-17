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
    public class PumpStationDeviceController : BaseController
    {
        public PumpStationDeviceController(MsGisDbContext db) : base(db)
        {

        }

        [HttpGet]
        public object LoadDevicesByPumpStation(int PumpStationId, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<DPumpStationsDevice>().Where(x => x.FkIdPumpStations == PumpStationId), loadOptions);
        }

        [HttpGet]
        public object Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<DPumpStationsDevice>(), loadOptions);
        }



        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var hd = _db.Asqueryable<DPumpStationsDevice>().FirstOrDefault(x => x.Objectid == key);

            if (hd == null)
            {
                hd = new DPumpStationsDevice();
            }
            JsonConvert.PopulateObject(values, hd);


            if (key == 0)
            {
                hd.GlobalId = Guid.NewGuid();
                hd.Objectid = (_db.AsqueryableWithoutIsDeleted<DPumpStationsDevice>().Max(x => x.Objectid)) + 1;
                hd.IsDeleted = 0;

                _db.Insert<DPumpStationsDevice>(hd);

            }
            else
            {

                _db.Update<DPumpStationsDevice>(hd);

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
                var dPumpStationsDevice = _db.Asqueryable<DPumpStationsDevice>().First(x => x.IdPumpStationsDevices == key);
                _db.Delete(dPumpStationsDevice);
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
