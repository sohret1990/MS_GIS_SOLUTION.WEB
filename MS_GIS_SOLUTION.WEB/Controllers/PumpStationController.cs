using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Models;
using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Z.EntityFramework.Plus;
using MS_GIS_SOLUTION.WEB.Helpers;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class PumpStationController : BaseController
    {
        public PumpStationController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Nasos stansiyaları";
        }

        public virtual Task<LoadResult> Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj08PumpStation>().Where(x => x.IdPumpStation != null).Select(x => new
            {
                x.IdPumpStation,
                x.Controlled,
                x.Description,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.FkIdTechnicalStatus,
                x.Name,
                x.FkIdAdminUnit,
                x.FkIdAppointment,
                x.FkIdPumpStationEngineType,
                x.FkIdPumpStationType,
                x.FkIdSource,
                x.FkIdSourceType,
                x.ServiceArea,
                x.TotalCapacity,
                x.AdditionalAdminUnit,
                x.Productivity,
                x.AggregatesCount,
                x.Objectid,
            }), loadOptions);
        }

        public virtual Task<LoadResult> LoadPumpStationEngines(DataSourceLoadOptions loadOptions, int pumpStationId)
        {
            return DataSourceLoader.LoadAsync(_db.Asqueryable<DPumpStationsDevice>().Where(x => x.FkIdPumpStations != null && x.FkIdPumpStations == pumpStationId), loadOptions);
        }
        public virtual Task<LoadResult> LoadFullData(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj08PumpStation>().Where(x => x.IdPumpStation != null).Select(x => new
            {
                x.IdPumpStation,
                x.Controlled,
                x.Description,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.FkIdTechnicalStatus,
                x.Name,
                x.FkIdAdminUnit,
                x.FkIdAppointment,
                x.FkIdPumpStationEngineType,
                x.FkIdPumpStationType,
                x.FkIdSource,
                x.FkIdSourceType,
                x.ServiceArea,
                x.TotalCapacity,
                x.AdditionalAdminUnit,
                x.Productivity,
                x.AggregatesCount,
                x.Objectid,
                x.DPumpStationsDevice
            }), loadOptions);
        }

        public IActionResult CreateEdit(int? id, int fromSave = 0, bool mode = false)
        {
            Obj08PumpStationViewModel model = new Obj08PumpStationViewModel();

            if (id != null)
            {
                Obj08PumpStation pumpStation = _db.Asqueryable<Obj08PumpStation>().First(x => x.IdPumpStation == id);
                model.IdPumpStation = pumpStation.IdPumpStation;
                model.Name = pumpStation.Name;
                model.AggregatesCount = pumpStation.AggregatesCount;
                model.AdditionalAdminUnit = pumpStation.AdditionalAdminUnit;
                model.TotalCapacity = pumpStation.TotalCapacity;
                model.ServiceArea = pumpStation.ServiceArea;
                model.FkIdAppointment = pumpStation.FkIdAppointment;
                model.FkIdAdminUnit = pumpStation.FkIdAdminUnit;
                model.Productivity = pumpStation.Productivity;
                model.FkIdPumpStationType = pumpStation.FkIdPumpStationType;
                model.FkIdPumpStationEngineType = pumpStation.FkIdPumpStationEngineType;
                model.FkIdActivityStatus = pumpStation.FkIdActivityStatus;
                model.FkIdOwnershipType = pumpStation.FkIdOwnershipType;
                model.FkIdOwner = pumpStation.FkIdOwner;
                model.FkIdSourceType = pumpStation.FkIdSourceType;
                model.FkIdSource = pumpStation.FkIdSource;
                model.FkIdTechnicalStatus = pumpStation.FkIdTechnicalStatus;
                model.ExploitationDate = pumpStation.ExploitationDate;
                model.Description = pumpStation.Description;
                model.GlobalId = pumpStation.GlobalId;
                model.Objectid = pumpStation.Objectid;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
                model.DPumpStationsDevice = _db.Asqueryable<DPumpStationsDevice>().Where(x => x.FkIdPumpStations == id).ToList();

            }

            model.FromSave = fromSave;

            return View(model);
        }

        public async Task<IActionResult> GetPhotoPartial(Guid? globalId)
        {
            List<string> ImageList = new List<string>();

            if (globalId != null)
            {
                List<byte[]> ImageBytes = await Task.Run(() => _db.Asqueryable<Obj08PumpStationsAttach>().Where(c => c.RelGlobalid == globalId).Select(c => c.Data).ToList());

                foreach (byte[] image in ImageBytes)
                {
                    var newimage = Convert.ToBase64String(image);
                    ImageList.Add("data:image/png;base64," + newimage);
                }
            }

            return PartialView("_PhotoPartial", ImageList);
        }

        [HttpPost]
        public IActionResult Save(Obj08PumpStationViewModel model)
        {
            Obj08PumpStation pumpStation = _db.Asqueryable<Obj08PumpStation>().FirstOrDefault(x => x.Objectid == model.Objectid) ?? new Obj08PumpStation();

            if (model.Objectid > 0)
            {
                pumpStation.IdPumpStation = model.IdPumpStation;
                pumpStation.Name = model.Name;
                pumpStation.AggregatesCount = model.AggregatesCount;
                pumpStation.AdditionalAdminUnit = model.AdditionalAdminUnit;
                pumpStation.TotalCapacity = model.TotalCapacity;
                pumpStation.ServiceArea = model.ServiceArea;
                pumpStation.FkIdAppointment = model.FkIdAppointment;
                pumpStation.FkIdAdminUnit = model.FkIdAdminUnit;
                pumpStation.Productivity = model.Productivity;
                pumpStation.FkIdActivityStatus = model.FkIdActivityStatus;
                pumpStation.FkIdOwnershipType = model.FkIdOwnershipType;
                pumpStation.FkIdPumpStationType = model.FkIdPumpStationType;
                pumpStation.FkIdPumpStationEngineType = model.FkIdPumpStationEngineType;
                pumpStation.FkIdSource = model.FkIdSource;
                pumpStation.FkIdSourceType = model.FkIdSourceType;
                pumpStation.FkIdOwner = model.FkIdOwner;
                pumpStation.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
                pumpStation.Objectid = model.Objectid ?? 0;
                pumpStation.ExploitationDate = model.ExploitationDate;
                pumpStation.Description = model.Description;


                if (model.Objectid > 0)
                {
                    _db.Update(pumpStation);
                }

                _db.SaveChanges();
            }

            return RedirectToActionPermanent("CreateEdit", "PumpStation", new { id = pumpStation.IdPumpStation, fromSave = 1 });
        }

        [HttpPost]
        public List<int> Filter(PumpStationFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            return _db.Asqueryable<VwObj08PumpStationsAndDevices>()
                .Where(x => userRegionIds.Contains((int)x.fk_id_admin_unit) &&
                    (x.name.Contains(data.name) || data.name == null) &&

                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.fk_id_admin_unit)) &&
                    (data.fk_id_source_type == null || data.fk_id_source_type.Contains(x.fk_id_source_type)) &&
                    (data.fk_id_source == null || data.fk_id_source.Contains(x.fk_id_source)) &&
                    (data.fk_id_activity_status == null || data.fk_id_activity_status.Contains(x.fk_id_activity_status)) &&
                    (data.fk_id_technical_status == null || data.fk_id_technical_status.Contains(x.fk_id_technical_status)) &&
                    (data.fk_id_appointment == null || data.fk_id_appointment.Contains(x.fk_id_appointment)) &&
                    (data.fk_id_pump_station_type == null || data.fk_id_pump_station_type.Contains(x.fk_id_pump_station_type)) &&
                    (data.fk_id_pump_station_engine_type == null || data.fk_id_pump_station_engine_type.Contains(x.fk_id_pump_station_engine_type)) &&
                    (data.fk_id_ownership_type == null || data.fk_id_ownership_type.Contains(x.fk_id_ownership_type)) &&
                    (data.fk_id_owner == null || data.fk_id_owner.Contains(x.fk_id_owner)) &&
                    (data.fk_id_protection_type == null || data.fk_id_protection_type.Contains(x.fk_id_protection_type)) &&
                    (data.fk_id_loc_water_source_type == null || data.fk_id_loc_water_source_type.Contains(x.fk_id_loc_water_source_type)) &&
                    (data.fk_id_located_water_source == null || data.fk_id_located_water_source.Contains(x.fk_id_located_water_source)) &&
                    (data.fk_id_pse_synchronize_type == null || data.fk_id_pse_synchronize_type.Contains(x.fk_id_pse_synchronize_type)) &&
                    (data.EXPLOITATION_DATE_min == null || x.EXPLOITATION_DATE >= data.EXPLOITATION_DATE_min) &&
                    (data.EXPLOITATION_DATE_max == null || x.EXPLOITATION_DATE <= data.EXPLOITATION_DATE_max) &&
                    (data.aggregates_count_min == null || x.aggregates_count >= data.aggregates_count_min) &&
                    (data.aggregates_count_max == null || x.aggregates_count <= data.aggregates_count_max) &&
                    (data.aggregates_model == null || x.aggregates_model.Contains(data.aggregates_model)) &&
                    (data.controlled == null || x.controlled.Contains(data.controlled)) &&
                    (data.last_installation_date_min == null || x.last_installation_date >= data.last_installation_date_min) &&
                    (data.last_installation_date_max == null || x.last_installation_date <= data.last_installation_date_max) &&

                    (data.protection_zone_area_min == null || x.protection_zone_area >= data.protection_zone_area_min) &&
                    (data.protection_zone_area_max == null || x.protection_zone_area <= data.protection_zone_area_max) &&

                    (data.total_capacity_min == null || x.total_capacity >= data.total_capacity_min) &&
                    (data.total_capacity_max == null || x.total_capacity <= data.total_capacity_max) &&

                    (data.service_area_min == null || x.service_area >= data.service_area_min) &&
                    (data.service_area_max == null || x.service_area <= data.service_area_max) &&

                    (data.productivity_min == null || x.productivity >= data.productivity_min) &&
                    (data.productivity_max == null || x.productivity <= data.productivity_max) &&

                    //Devices filter
                    //pump area
                    (data.pump_model == null || x.pump_model.Contains(data.pump_model)) &&
                    (data.pump_last_repair_date_min == null || x.pump_last_repair_date >= data.pump_last_repair_date_min) &&
                    (data.pump_last_repair_date_max == null || x.pump_last_repair_date <= data.pump_last_repair_date_max) &&

                    (data.pump_productivity_min == null || x.pump_productivity >= data.pump_productivity_min) &&
                    (data.pump_productivity_max == null || x.pump_productivity <= data.pump_productivity_max) &&

                    (data.pump_pressure_min == null || x.pump_pressure >= data.pump_pressure_min) &&
                    (data.pump_pressure_max == null || x.pump_pressure <= data.pump_pressure_max) &&

                    //engine area
                    (data.engine_model == null || x.engine_model.Contains(data.engine_model)) &&
                    (data.engine_last_repair_date_min == null || x.engine_last_repair_date >= data.engine_last_repair_date_min) &&
                    (data.engine_last_repair_date_max == null || x.engine_last_repair_date <= data.engine_last_repair_date_max) &&
                    (data.engine_horsepower_min == null || x.engine_horsepower >= data.engine_horsepower_min) &&
                    (data.engine_horsepower_max == null || x.engine_horsepower <= data.engine_horsepower_max) &&
                    (data.engine_last_installation_date_min == null || x.engine_last_installation_date >= data.engine_last_installation_date_min) &&
                    (data.engine_last_installation_date_max == null || x.engine_last_installation_date <= data.engine_last_installation_date_max) &&
                    (data.engine_pipeline_diameter_min == null || x.engine_pipeline_diameter >= data.engine_pipeline_diameter_min) &&
                    (data.engine_pipeline_diameter_max == null || x.engine_pipeline_diameter <= data.engine_pipeline_diameter_max) &&
                    (data.engine_pipeline_length_min == null || x.engine_pipeline_length >= data.engine_pipeline_length_min) &&
                    (data.engine_pipeline_length_max == null || x.engine_pipeline_length <= data.engine_pipeline_length_max) &&
                    (data.engine_power_min == null || x.engine_power >= data.engine_power_min) &&
                    (data.engine_power_max == null || x.engine_power <= data.engine_power_max) &&
                    (x.engine_pipeline_material.Contains(data.engine_pipeline_material) || data.engine_pipeline_material == null)

                    )
                .Select(x => x.OBJECTID).Distinct().ToList();
        }

        [HttpPost]
        public Task<LoadResult> LoadFilter(DataSourceLoadOptions loadOptions, string objectIds)
        {
            List<int> ids = new List<int>();
            if (!string.IsNullOrEmpty(objectIds))
            {
                ids = objectIds.Split(",").Select(int.Parse).ToList();
            }
            //
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj08PumpStation>().Where(x => (ids.Contains(x.Objectid))).Select(x => new
            {
                x.IdPumpStation,
                x.Controlled,
                x.Description,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.FkIdTechnicalStatus,
                x.Name,
                x.FkIdAdminUnit,
                x.FkIdAppointment,
                x.FkIdPumpStationEngineType,
                x.FkIdPumpStationType,
                x.FkIdSource,
                x.FkIdSourceType,
                x.ServiceArea,
                x.TotalCapacity,
                x.AdditionalAdminUnit,
                x.Productivity,
                x.AggregatesCount,
                x.Objectid,
            }), loadOptions);
        }

        public object LoadPumpStationFiles(DataSourceLoadOptions options, Guid RelGlobalId)
        {
            var list = _db.Asqueryable<Obj08PumpStationsAttach>().Where(x => x.RelGlobalid == RelGlobalId).Select(x => new { x.RelGlobalid, x.Attachmentid, x.ContentType, x.AttName, DataSize = Helper.FormatSize(x.DataSize) });
            return DataSourceLoader.Load(list, options);
        }

        public IActionResult DeletePumpStationFile(int key)
        {
            if (key == 0)
            {
                return BadRequest("Sənəd tapılmadı");
            }
            try
            {
                _db.DeleteById<Obj08PumpStationsAttach>(key);
                _db.SaveChanges();
            }
            catch
            {
                return BadRequest("Əlaqəli bağlantı var və öncə onu silməyiniz gərəklidir.");
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult UploadFile(Guid globalId)
        {
            var file = Request.Form.Files["File"];
            if (file == null) return BadRequest("Fayl mövcud deyil!");

            MemoryStream str = new MemoryStream();
            file.CopyTo(str);
            int attachmentId = _db.Asqueryable<Obj08PumpStationsAttach>().Max(x => x.Attachmentid) ?? 0;

            Obj08PumpStationsAttach fl = new Obj08PumpStationsAttach();
            fl.Attachmentid = attachmentId + 1;
            fl.RelGlobalid = globalId;
            fl.Globalid = Guid.NewGuid();
            fl.AttName = file.FileName;
            fl.ContentType = Path.GetExtension(file.FileName).ToLower();
            fl.DataSize = (int)file.Length;
            fl.Data = str.GetBuffer();

            _db.Insert(fl);
            _db.SaveChanges();


            return Ok(true);
        }

        public IActionResult DownloadFile(int attachmentId)
        {
            Obj08PumpStationsAttach doc = _db.Asqueryable<Obj08PumpStationsAttach>().FirstOrDefault(x => x.Attachmentid == attachmentId);
            if (doc.Data == null) { return BadRequest("Fayl mövcud deyil!"); }

            var fileName = doc.AttName;
            byte[] buffer = doc.Data;
            return File(buffer, "application/x-msdownload", fileName);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int key)
        {
            bool result = true;
            var obj = _db.Asqueryable<Obj08PumpStation>().First(x => x.IdPumpStation == key);
            _db.Delete(obj);
            result = _db.SaveChanges() > 0;

            if (result)
                return Ok();
            else
                return BadRequest("Əməliyyat zamanı xəta baş verdi!");
        }

        [HttpPost]
        public object Get(int id)
        {
            return _db.Asqueryable<Obj08PumpStation>().Where(x => x.IdPumpStation == id).ToList().Select(x => new
            {
                Name = x.Name ?? "məlumat yoxdur",
                AggregatesCount = _db.Asqueryable<DPumpStationsDevice>().Count(z => z.FkIdPumpStations == id),
                Power = _db.Asqueryable<DPumpStationsDevice>().Where(x => x.FkIdPumpStations == id).Sum(x => x.EnginePower) ?? 0,
                ServiceArea = x.ServiceArea > 0 ? decimal.Round(x.ServiceArea ?? 0, 2).ToString() : "məlumat yoxdur",
                ExploitationDate = x.ExploitationDate > 0 ? x.ExploitationDate.ToString() : "məlumat yoxdur",
                AdditionalAdminUnit = x.AdditionalAdminUnit ?? "məlumat yoxdur",
                Type = _db.Asqueryable<SPumpStationType>().FirstOrDefault(z => z.IdPumpStationType == x.FkIdPumpStationType)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == x.FkIdOwner)?.Name ?? "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur"
            }).FirstOrDefault();
        }

        public IActionResult GetReport()
        {
            return View();
        }
    }
}
