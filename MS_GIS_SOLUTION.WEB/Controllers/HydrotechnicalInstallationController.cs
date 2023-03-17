using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Helpers;
using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MS_GIS_SOLUTION.WEB.Models.FilterVM;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class HydrotechnicalInstallationController : BaseController
    {
        public HydrotechnicalInstallationController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Hidrotexniki qurğular";
        }



        public virtual Task<LoadResult> Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj06HydrotechnicalInstallation>().Where(x => x.IdHydrotechnicalInstallations != null).Select(x => new
            {
                x.Controlled,
                x.Depth,
                x.Description,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdHtiNetworkType,
                x.FkIdHtInstallationType,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.FkIdSource,
                x.FkIdSourceType,
                x.FkIdTechnicalStatus,
                x.IdHydrotechnicalInstallations,
                x.IsPhoto,
                x.Objectid,
                x.Name,
                x.TechnicalParameters,
                x.WaterPermeability,
            }), loadOptions);
        }

        public IActionResult CreateEdit(int? id, bool mode = false)
        {
            Obj06ViewModel model = new Obj06ViewModel();

            if (id != null)
            {
                Obj06HydrotechnicalInstallation hydrotechnicalInstallation = _db.Asqueryable<Obj06HydrotechnicalInstallation>().FirstOrDefault(x => x.IdHydrotechnicalInstallations == id);
                model.Name = hydrotechnicalInstallation.Name;
                model.FkIdAdminUnit = hydrotechnicalInstallation.FkIdAdminUnit;
                model.FkIdActivityStatus = hydrotechnicalInstallation.FkIdActivityStatus;
                model.FkIdOwnershipType = hydrotechnicalInstallation.FkIdOwnershipType;
                model.FkIdOwner = hydrotechnicalInstallation.FkIdOwner;
                model.FkIdSourceType = hydrotechnicalInstallation.FkIdSourceType;
                model.FkIdHtiNetworkType = hydrotechnicalInstallation.FkIdHtiNetworkType;
                model.FkIdHtInstallationType = hydrotechnicalInstallation.FkIdHtInstallationType;
                model.FkIdSource = hydrotechnicalInstallation.FkIdSource;
                model.FkIdTechnicalStatus = hydrotechnicalInstallation.FkIdTechnicalStatus;
                model.TechnicalParameters = hydrotechnicalInstallation.TechnicalParameters;
                model.WaterPermeability = hydrotechnicalInstallation.WaterPermeability;
                model.ExploitationDate = hydrotechnicalInstallation.ExploitationDate;
                model.Description = hydrotechnicalInstallation.Description;
                model.Depth = hydrotechnicalInstallation.Depth;
                model.GlobalId = hydrotechnicalInstallation.GlobalId;
                model.Objectid = hydrotechnicalInstallation.Objectid;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
                model.IdHydrotechnicalInstallations = hydrotechnicalInstallation.IdHydrotechnicalInstallations;
            }

            return View(model);
        }

        public async Task<IActionResult> GetPhotoPartial(Guid? globalId)
        {
            List<string> ImageList = new List<string>();

            if (globalId != null)
            {
                List<byte[]> ImageBytes = await Task.Run(() => _db.Asqueryable<Obj06HydrotechnicalInstallationsAttach>().Where(c => c.RelGlobalid == globalId).Select(c => c.Data).ToList());

                foreach (byte[] image in ImageBytes)
                {
                    var newimage = Convert.ToBase64String(image);
                    ImageList.Add("data:image/png;base64," + newimage);
                }
            }

            return PartialView("_PhotoPartial", ImageList);
        }

        [HttpPost]

        public IActionResult Save(Obj06ViewModel model)
        {
            Obj06HydrotechnicalInstallation hydrotechnicalInstallation = _db.Asqueryable<Obj06HydrotechnicalInstallation>().FirstOrDefault(x => x.Objectid == model.Objectid && x.IdHydrotechnicalInstallations == model.IdHydrotechnicalInstallations) ?? new Obj06HydrotechnicalInstallation();

            if (hydrotechnicalInstallation != null)
            {
                hydrotechnicalInstallation.Name = model.Name;
                hydrotechnicalInstallation.FkIdAdminUnit = model.FkIdAdminUnit;
                hydrotechnicalInstallation.FkIdActivityStatus = model.FkIdActivityStatus;
                hydrotechnicalInstallation.FkIdOwnershipType = model.FkIdOwnershipType;
                hydrotechnicalInstallation.FkIdOwner = model.FkIdOwner;
                hydrotechnicalInstallation.FkIdSourceType = model.FkIdSourceType;
                hydrotechnicalInstallation.FkIdSource = model.FkIdSource;
                hydrotechnicalInstallation.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
                hydrotechnicalInstallation.FkIdHtInstallationType = model.FkIdHtInstallationType;
                hydrotechnicalInstallation.FkIdHtiNetworkType = model.FkIdHtiNetworkType;
                hydrotechnicalInstallation.TechnicalParameters = model.TechnicalParameters;
                hydrotechnicalInstallation.WaterPermeability = model.WaterPermeability;
                hydrotechnicalInstallation.ExploitationDate = model.ExploitationDate;
                hydrotechnicalInstallation.Description = model.Description;
                hydrotechnicalInstallation.Depth = model.Depth;
                hydrotechnicalInstallation.Objectid = model.Objectid ?? 0;
                hydrotechnicalInstallation.IdHydrotechnicalInstallations = model.IdHydrotechnicalInstallations;

                if (model.Objectid > 0)
                {
                    _db.Update(hydrotechnicalInstallation);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public object LoadHydroTechnicalInstallationFiles(DataSourceLoadOptions options, Guid RelGlobalId)
        {
            var list = _db.Asqueryable<Obj06HydrotechnicalInstallationsAttach>().Where(x => x.RelGlobalid == RelGlobalId).Select(x => new { x.RelGlobalid, x.Attachmentid, x.ContentType, x.AttName, DataSize = Helper.FormatSize(x.DataSize) });
            return DataSourceLoader.Load(list, options);
        }


        public IActionResult DeleteHydroTechnicalInstallationFile(int key)
        {
            if (key == 0)
            {
                return BadRequest("Sənəd tapılmadı");
            }
            try
            {
                _db.DeleteById<Obj06HydrotechnicalInstallationsAttach>(key);
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
            int attachmentId = _db.Asqueryable<Obj06HydrotechnicalInstallationsAttach>().Max(x => x.Attachmentid);

            Obj06HydrotechnicalInstallationsAttach fl = new Obj06HydrotechnicalInstallationsAttach();
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
            Obj06HydrotechnicalInstallationsAttach doc = _db.Asqueryable<Obj06HydrotechnicalInstallationsAttach>().FirstOrDefault(x => x.Attachmentid == attachmentId);
            if (doc.Data == null) { return BadRequest("Fayl mövcud deyil!"); }

            var fileName = doc.AttName;
            byte[] buffer = doc.Data;
            return File(buffer, "application/x-msdownload", fileName);
        }


        public List<int> GetObjectIds(int htiId)
        {
            return _db.Asqueryable<Obj06HydrotechnicalInstallation>(x => x.IdHydrotechnicalInstallations == htiId).Select(x => x.Objectid).Distinct().ToList();
        }

        [HttpPost]
        public List<int> Filter(HydrotechnicalInstallationFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();

            return _db.Asqueryable<VwObj06HydrotechnicalInstallation>()
                .Where(x =>
                userRegionIds.Contains((int)x.fk_id_admin_unit) &&
                   (x.name.Contains(data.name) || data.name == null) &&
                   (data.fk_id_admin_unit.Count == 0 || data.fk_id_admin_unit.Contains(x.fk_id_admin_unit)) &&
                   (data.fk_id_source_type.Count == 0 || data.fk_id_source_type.Contains(x.fk_id_source_type)) &&
                   (data.fk_id_source.Count == 0 || data.fk_id_source.Contains(x.fk_id_source)) &&
                   (data.fk_id_activity_status.Count == 0 || data.fk_id_activity_status.Contains(x.fk_id_activity_status)) &&
                   (data.fk_id_technical_status.Count == 0 || data.fk_id_technical_status.Contains(x.fk_id_technical_status)) &&
                   (data.fk_id_ownership_type.Count == 0 || data.fk_id_ownership_type.Contains(x.fk_id_ownership_type)) &&
                   (data.fk_id_owner.Count == 0 || data.fk_id_owner.Contains(x.fk_id_owner)) &&
                   (data.fk_id_protection_type.Count == 0 || data.fk_id_protection_type.Contains(x.fk_id_protection_type)) &&
                   (data.fk_id_hti_network_type.Count == 0 || data.fk_id_hti_network_type.Contains(x.fk_id_hti_network_type)) &&
                   (data.fk_id_ht_installation_type.Count == 0 || data.fk_id_ht_installation_type.Contains(x.fk_id_ht_installation_type)) &&
                   (data.fk_id_missing_source_type.Count == 0 || data.fk_id_missing_source_type.Contains(x.fk_id_missing_source_type)) &&
                   (x.technical_parameters.Contains(data.technical_parameters) || data.technical_parameters == null) &&

                   (x.EXPLOITATION_DATE >= data.EXPLOITATION_DATE_min || data.EXPLOITATION_DATE_min == null) &&
                   (x.EXPLOITATION_DATE <= data.EXPLOITATION_DATE_max || data.EXPLOITATION_DATE_max == null) &&

                   (x.protection_zone_area >= data.protection_zone_area_min || data.protection_zone_area_min == null) &&
                   (x.protection_zone_area <= data.protection_zone_area_max || data.protection_zone_area_max == null) &&

                   (x.depth >= data.depth_min || data.depth_min == null) &&
                   (x.depth <= data.depth_max || data.depth_max == null) &&

                   (x.water_permeability >= data.water_permeability_min || data.water_permeability_min == null) &&
                   (x.water_permeability <= data.water_permeability_max || data.water_permeability_max == null)

                )
                .Select(x => x.Objectid).Distinct().ToList();
        }

        [HttpPost]
        public LoadResult LoadFilter(DataSourceLoadOptions loadOptions, string data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            HydrotechnicalInstallationFilterModel d = new HydrotechnicalInstallationFilterModel();

            JsonConvert.PopulateObject(data, d);

            return DataSourceLoader.Load(_db.Asqueryable<VwObj06HydrotechnicalInstallation>()
               .Where(x =>
               userRegionIds.Contains((int)x.fk_id_admin_unit) &&
                   (x.name.Contains(d.name) || d.name == null) &&
                   (d.fk_id_admin_unit.Count == 0 || d.fk_id_admin_unit.Contains(x.fk_id_admin_unit)) &&
                   (d.fk_id_source_type.Count == 0 || d.fk_id_source_type.Contains(x.fk_id_source_type)) &&
                   (d.fk_id_source.Count == 0 || d.fk_id_source.Contains(x.fk_id_source)) &&
                   (d.fk_id_activity_status.Count == 0 || d.fk_id_activity_status.Contains(x.fk_id_activity_status)) &&
                   (d.fk_id_technical_status.Count == 0 || d.fk_id_technical_status.Contains(x.fk_id_technical_status)) &&
                   (d.fk_id_ownership_type.Count == 0 || d.fk_id_ownership_type.Contains(x.fk_id_ownership_type)) &&
                   (d.fk_id_owner.Count == 0 || d.fk_id_owner.Contains(x.fk_id_owner)) &&
                   (d.fk_id_protection_type.Count == 0 || d.fk_id_protection_type.Contains(x.fk_id_protection_type)) &&
                   (d.fk_id_hti_network_type.Count == 0 || d.fk_id_hti_network_type.Contains(x.fk_id_hti_network_type)) &&
                   (d.fk_id_ht_installation_type.Count == 0 || d.fk_id_ht_installation_type.Contains(x.fk_id_ht_installation_type)) &&
                   (d.fk_id_missing_source_type.Count == 0 || d.fk_id_missing_source_type.Contains(x.fk_id_missing_source_type)) &&
                   (x.technical_parameters.Contains(d.technical_parameters) || d.technical_parameters == null) &&

                   (x.EXPLOITATION_DATE >= d.EXPLOITATION_DATE_min || d.EXPLOITATION_DATE_min == null) &&
                   (x.EXPLOITATION_DATE <= d.EXPLOITATION_DATE_max || d.EXPLOITATION_DATE_max == null) &&

                   (x.protection_zone_area >= d.protection_zone_area_min || d.protection_zone_area_min == null) &&
                   (x.protection_zone_area <= d.protection_zone_area_max || d.protection_zone_area_max == null) &&

                   (x.depth >= d.depth_min || d.depth_min == null) &&
                   (x.depth <= d.depth_max || d.depth_max == null) &&

                   (x.water_permeability >= d.water_permeability_min || d.water_permeability_min == null) &&
                   (x.water_permeability <= d.water_permeability_max || d.water_permeability_max == null)

               ).Select(x => new
               {
                   IdHydrotechnicalInstallations = 1,
                   x.Objectid,
                   Name = x.name,
                   ExploitationDate = x.EXPLOITATION_DATE,
                   Controlled = x.controlled,
                   FkIdTechnicalStatus = x.fk_id_technical_status,
                   FkIdActivityStatus = x.fk_id_activity_status,
                   FkIdAdminUnit = x.fk_id_admin_unit,
                   FkIdHtInstallationType = x.fk_id_ht_installation_type,
                   FkIdHtiNetworkType = x.fk_id_hti_network_type,
                   FkIdOwnershipType = x.fk_id_ownership_type,
                   FkIdOwner = x.fk_id_owner,
                   FkIdSource = x.fk_id_source,
                   FkIdSourceType = x.fk_id_source_type,
                   Depth = x.depth,

                   TechnicalParameters = x.technical_parameters,
                   WaterPermeability = x.water_permeability
               }), loadOptions);


        }


        [HttpPost]
        public object Get(int id)
        {
            return _db.Asqueryable<Obj06HydrotechnicalInstallation>().Where(x => x.IdHydrotechnicalInstallations == id).ToList().Select(x => new
            {
                Name = x.Name ?? "məlumat yoxdur",
                WaterPermeability = x.WaterPermeability > 0 ? decimal.Round(x.WaterPermeability ?? 0, 2).ToString() : "məlumat yoxdur",
                Type = _db.Asqueryable<SHtInstallationType>().FirstOrDefault(z => z.IdHtInstallationType == x.FkIdHtInstallationType)?.Name ?? "məlumat yoxdur",
                Source = _db.Asqueryable<ObjViewSource>().FirstOrDefault(z => z.Id == x.FkIdSource)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == x.FkIdOwner)?.Name ?? "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
            }).FirstOrDefault();
        }
    }
}
