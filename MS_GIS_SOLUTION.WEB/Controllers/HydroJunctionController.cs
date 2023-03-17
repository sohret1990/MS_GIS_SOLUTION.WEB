using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Helpers;
using MS_GIS_SOLUTION.WEB.Models;
using MS_GIS_SOLUTION.WEB.Models.FilterVM;
using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class HydroJunctionController : BaseController
    {
        public HydroJunctionController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Hidroqovşaqlar";
        }



        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj05HydroJunction>().Where(x => x.IdHydroJunctions != null).Select(x => new
            {
                x.IdHydroJunctions,
                x.Controlled,
                x.Description,
                x.FkIdAppointment,
                x.FkIdHydroJunctionType,
                x.ServiceArea,
                x.ServiceAdminUnit,
                x.MaxWaterReleaseCapacity,
                x.DamLength,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.Objectid,
                x.FkIdProtectionType,
                x.FkIdSource,
                x.FkIdSourceType,
                x.FkIdTechnicalStatus,
                x.Name,
                x.ProtectionZoneArea
            }), loadOptions);
        }

        public IActionResult CreateEdit(int? id, bool mode = false)
        {
            Obj05HydroJunctionViewModel model = new Obj05HydroJunctionViewModel();

            if (id != null)
            {
                Obj05HydroJunction hydrotechnicalInstallation = _db.Asqueryable<Obj05HydroJunction>().FirstOrDefault(x => x.IdHydroJunctions == id);

                model.Name = hydrotechnicalInstallation.Name;
                model.FkIdAdminUnit = hydrotechnicalInstallation.FkIdAdminUnit;
                model.FkIdActivityStatus = hydrotechnicalInstallation.FkIdActivityStatus;
                model.FkIdOwnershipType = hydrotechnicalInstallation.FkIdOwnershipType;
                model.FkIdOwner = hydrotechnicalInstallation.FkIdOwner;
                model.FkIdProtectionType = hydrotechnicalInstallation.FkIdProtectionType;
                model.ProtectionZoneArea = hydrotechnicalInstallation.ProtectionZoneArea;
                model.FkIdSourceType = hydrotechnicalInstallation.FkIdSourceType;
                model.FkIdSource = hydrotechnicalInstallation.FkIdSource;
                model.FkIdTechnicalStatus = hydrotechnicalInstallation.FkIdTechnicalStatus;
                model.MaxWaterReleaseCapacity = hydrotechnicalInstallation.MaxWaterReleaseCapacity;
                model.DamLength = hydrotechnicalInstallation.DamLength;
                model.ExploitationDate = hydrotechnicalInstallation.ExploitationDate;
                model.Description = hydrotechnicalInstallation.Description;
                model.Objectid = hydrotechnicalInstallation.Objectid;
                model.IdHydroJunctions = hydrotechnicalInstallation.IdHydroJunctions;
                model.FkIdAppointment = hydrotechnicalInstallation.FkIdAppointment;
                model.FkIdHydroJunctionType = hydrotechnicalInstallation.FkIdHydroJunctionType;
                model.ServiceArea = hydrotechnicalInstallation.ServiceArea;
                model.ServiceAdminUnit = hydrotechnicalInstallation.ServiceAdminUnit;
                model.GlobalId = hydrotechnicalInstallation.GlobalId;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
            }

            return View(model);
        }

        public async Task<IActionResult> GetPhotoPartial(Guid? globalId)
        {
            List<string> ImageList = new List<string>();

            if (globalId != null)
            {
                List<byte[]> ImageBytes = await Task.Run(() => _db.Asqueryable<Obj05HydroJunctionsAttach>().Where(c => c.RelGlobalid == globalId).Select(c => c.Data).ToList());

                foreach (byte[] image in ImageBytes)
                {
                    var newimage = Convert.ToBase64String(image);
                    ImageList.Add("data:image/png;base64," + newimage);
                }
            }

            return PartialView("_PhotoPartial", ImageList);
        }
        [HttpPost]

        public IActionResult Save(Obj05HydroJunctionViewModel model)
        {
            Obj05HydroJunction hydroJunction = _db.Asqueryable<Obj05HydroJunction>().FirstOrDefault(x => x.Objectid == model.Objectid && x.IdHydroJunctions == model.IdHydroJunctions) ?? new Obj05HydroJunction();

            if (hydroJunction != null)
            {
                hydroJunction.Name = model.Name;
                hydroJunction.FkIdAdminUnit = model.FkIdAdminUnit;
                hydroJunction.FkIdActivityStatus = model.FkIdActivityStatus;
                hydroJunction.FkIdOwnershipType = model.FkIdOwnershipType;
                hydroJunction.FkIdOwner = model.FkIdOwner;
                hydroJunction.FkIdProtectionType = model.FkIdProtectionType;
                hydroJunction.ProtectionZoneArea = model.ProtectionZoneArea;
                hydroJunction.FkIdSourceType = model.FkIdSourceType;
                hydroJunction.FkIdSource = model.FkIdSource;
                hydroJunction.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
                hydroJunction.MaxWaterReleaseCapacity = model.MaxWaterReleaseCapacity;
                hydroJunction.DamLength = model.DamLength;
                hydroJunction.ExploitationDate = model.ExploitationDate;
                hydroJunction.Description = model.Description;
                hydroJunction.FkIdAppointment = model.FkIdAppointment;
                hydroJunction.FkIdHydroJunctionType = model.FkIdHydroJunctionType;
                hydroJunction.ServiceArea = model.ServiceArea;
                hydroJunction.ServiceAdminUnit = model.ServiceAdminUnit;
                hydroJunction.IdHydroJunctions = model.IdHydroJunctions;
                hydroJunction.Objectid = model.Objectid ?? 0;

                if (model.Objectid > 0)
                {
                    _db.Update(hydroJunction);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public object LoadHydroJunctionFiles(DataSourceLoadOptions options, Guid RelGlobalId)
        {
            var list = _db.Asqueryable<Obj05HydroJunctionsAttach>().Where(x => x.RelGlobalid == RelGlobalId).Select(x => new { x.RelGlobalid, x.Attachmentid, x.ContentType, x.AttName, DataSize = Helper.FormatSize(x.DataSize) });
            return DataSourceLoader.Load(list, options);
        }


        public IActionResult DeleteHydroJunctionFile(int key)
        {
            if (key == 0)
            {
                return BadRequest("Sənəd tapılmadı");
            }
            try
            {
                _db.DeleteById<Obj05HydroJunctionsAttach>(key);
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
            int attachmentId = _db.Asqueryable<Obj05HydroJunctionsAttach>().Max(x => x.Attachmentid);

            Obj05HydroJunctionsAttach fl = new Obj05HydroJunctionsAttach();
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
            Obj05HydroJunctionsAttach doc = _db.Asqueryable<Obj05HydroJunctionsAttach>().FirstOrDefault(x => x.Attachmentid == attachmentId);
            if (doc.Data == null) { return BadRequest("Fayl mövcud deyil!"); }

            var fileName = doc.AttName;
            byte[] buffer = doc.Data;
            return File(buffer, "application/x-msdownload", fileName);
        }




        [HttpPost]
        public List<int> Filter(HydroJunctionsFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            return _db.Asqueryable<Obj05HydroJunction>()
                .Where(x =>
                userRegionIds.Contains((int)x.FkIdAdminUnit) &&
                    (x.Name.Contains(data.name) || data.name == null) &&
                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkIdAdminUnit)) &&
                    (data.fk_id_source_type == null || data.fk_id_source_type.Contains(x.FkIdSourceType)) &&
                    (data.fk_id_source == null || data.fk_id_source.Contains(x.FkIdSource)) &&
                    (data.fk_id_activity_status == null || data.fk_id_activity_status.Contains(x.FkIdActivityStatus)) &&
                    (data.fk_id_technical_status == null || data.fk_id_technical_status.Contains(x.FkIdTechnicalStatus)) &&
                    (data.fk_id_ownership_type == null || data.fk_id_ownership_type.Contains(x.FkIdOwnershipType)) &&
                    (data.fk_id_owner == null || data.fk_id_owner.Contains(x.FkIdOwner)) &&
                    (data.fk_id_protection_type == null || data.fk_id_protection_type.Contains(x.FkIdProtectionType)) &&
                    (data.service_admin_unit == null || data.service_admin_unit.Contains(x.ServiceAdminUnit)) &&
                    (data.fk_id_appointment == null || data.fk_id_appointment.Contains(x.FkIdAppointment)) &&
                    (data.fk_id_hydro_junction_type == null || data.fk_id_hydro_junction_type.Contains(x.FkIdHydroJunctionType)) &&

                    (x.ExploitationDate >= data.EXPLOITATION_DATE_min || data.EXPLOITATION_DATE_min == null) &&
                    (x.ExploitationDate <= data.EXPLOITATION_DATE_max || data.EXPLOITATION_DATE_max == null) &&

                    (x.ProtectionZoneArea >= data.protection_zone_area_min || data.protection_zone_area_min == null) &&
                    (x.ProtectionZoneArea <= data.protection_zone_area_max || data.protection_zone_area_max == null) &&

                    (x.DamLength >= data.dam_length_min || data.dam_length_min == null) &&
                    (x.DamLength <= data.dam_length_max || data.dam_length_max == null) &&

                    (x.MaxWaterReleaseCapacity >= data.max_water_release_capacity_min || data.max_water_release_capacity_min == null) &&
                    (x.MaxWaterReleaseCapacity <= data.max_water_release_capacity_max || data.max_water_release_capacity_max == null) &&

                    (x.ServiceArea >= data.service_area_min || data.service_area_min == null) &&
                    (x.ServiceArea <= data.service_area_max || data.service_area_max == null)

                )
                .Select(x => x.Objectid).Distinct().ToList();
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
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj05HydroJunction>().Where(x => (ids.Contains(x.Objectid))).Select(x => new
            {
                x.IdHydroJunctions,
                x.Objectid,
                x.Name,
                x.ExploitationDate,
                x.Controlled,
                x.FkIdTechnicalStatus,
                x.FkIdAppointment,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdProtectionType,
                x.FkIdOwnershipType,
                x.FkIdOwner,
                x.FkIdSource,
                x.FkIdSourceType,
                x.FkIdHydroJunctionType,
                x.DamLength,
                x.Description,
                x.ProtectionZoneArea,
                x.MaxWaterReleaseCapacity,
                x.ServiceArea,
                x.ServiceAdminUnit
            }), loadOptions);
        }

        [HttpPost]
        public object Get(int id)
        {
            return _db.Asqueryable<Obj05HydroJunction>().Where(x => x.IdHydroJunctions == id).ToList().Select(x => new
            {
                x.IdHydroJunctions,
                Name = x.Name ?? "məlumat yoxdur",
                ExploitationDate = x.ExploitationDate > 0 ? x.ExploitationDate.ToString() : "məlumat yoxdur",
                Source = _db.Asqueryable<ObjViewSource>().FirstOrDefault(z => z.Id == x.FkIdSource)?.Name ?? "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int?)x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Appointment = _db.Asqueryable<SAppointment>().FirstOrDefault(z => z.IdAppointment == (int?)x.FkIdAppointment)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == (int?)x.FkIdOwner)?.Name ?? "məlumat yoxdur"
            }).FirstOrDefault();
        }


    }
}
