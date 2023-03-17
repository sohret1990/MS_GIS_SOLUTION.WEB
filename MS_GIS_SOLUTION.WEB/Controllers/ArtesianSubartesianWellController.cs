using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Helpers;
using MS_GIS_SOLUTION.WEB.Models.FilterVM;
using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ArtesianSubartesianWellController : BaseController
    {
        public ArtesianSubartesianWellController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Artezian və subartezian quyuları";
        }



        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj07ArtesianSubartesianWell>().Where(x => x.IdArtesianSubartesianWell != null).Select(x => new
            {
                x.Controlled,
                x.Depth,
                x.Description,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.Objectid,
                x.FkIdProtectionType,
                x.FkIdTechnicalStatus,
                x.Name,
                x.ProtectionZoneArea,
                x.FkIdWaterUseAssociation,
                x.FkAdminUnit,
                x.No,
                x.FkIdArtesianType,
                x.ScadaInfo,
                x.ResidentialArea,
                x.FkIdAppointment,
                x.PumpModel,
                x.Productivity,
                x.PumpType,
                x.IdArtesianSubartesianWell,
                x.ServiceArea
            }), loadOptions);
        }

        public IActionResult CreateEdit(int? id, bool mode = false)
        {
            Obj07ArtesianSubartesianWellViewModel model = new Obj07ArtesianSubartesianWellViewModel();

            if (id != null)
            {
                Obj07ArtesianSubartesianWell well = _db.Asqueryable<Obj07ArtesianSubartesianWell>().First(x => x.IdArtesianSubartesianWell == id);
                model.Objectid = well.Objectid;
                model.IdArtesianSubartesianWell = well.IdArtesianSubartesianWell;
                model.Name = well.Name;
                model.No = well.No;
                model.PumpModel = well.PumpModel;
                model.PumpType = well.PumpType;
                model.Depth = well.Depth;
                model.FkIdArtesianType = well.FkIdArtesianType;
                model.EnginePover = well.EnginePover;
                model.ServiceArea = well.ServiceArea;
                model.ScadaInfo = well.ScadaInfo;
                model.FkIdAppointment = well.FkIdAppointment;
                model.FkAdminUnit = well.FkAdminUnit;
                model.Productivity = well.Productivity;
                model.ProtectionZoneArea = well.ProtectionZoneArea;
                model.FkIdActivityStatus = well.FkIdActivityStatus;
                model.FkIdWaterUseAssociation = well.FkIdWaterUseAssociation;
                model.FkIdOwnershipType = well.FkIdOwnershipType;
                model.FkIdOwner = well.FkIdOwner;
                model.FkIdProtectionType = well.FkIdProtectionType;
                model.FkIdTechnicalStatus = well.FkIdTechnicalStatus;
                model.ResidentialArea = well.ResidentialArea;
                model.RepairDate = well.RepairDate;
                model.ExploitationDate = well.ExploitationDate;
                model.Description = well.Description;
                model.GlobalId = well.GlobalId;
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
                List<byte[]> ImageBytes = await Task.Run(() => _db.Asqueryable<Obj07ArtesianSubartesianWellsAttach>().Where(c => c.RelGlobalid == globalId).Select(c => c.Data).ToList());

                foreach (byte[] image in ImageBytes)
                {
                    var newimage = Convert.ToBase64String(image);
                    ImageList.Add("data:image/png;base64," + newimage);
                }
            }

            return PartialView("_PhotoPartial", ImageList);
        }


        [HttpPost]

        public IActionResult Save(Obj07ArtesianSubartesianWellViewModel model)
        {
            Obj07ArtesianSubartesianWell well = _db.Asqueryable<Obj07ArtesianSubartesianWell>().FirstOrDefault(x => x.Objectid == model.Objectid && x.IdArtesianSubartesianWell == model.IdArtesianSubartesianWell) ?? new Obj07ArtesianSubartesianWell();

            if (well != null)
            {
                well.IdArtesianSubartesianWell = model.IdArtesianSubartesianWell;
                well.Name = model.Name;
                well.No = model.No;
                well.PumpModel = model.PumpModel;
                well.PumpType = model.PumpType;
                well.ServiceArea = model.ServiceArea;
                well.ScadaInfo = model.ScadaInfo;
                well.FkIdAppointment = model.FkIdAppointment;
                well.FkAdminUnit = model.FkAdminUnit;
                well.Objectid = model.Objectid ?? 0;
                well.FkIdArtesianType = model.FkIdArtesianType;
                well.Depth = model.Depth;
                well.EnginePover = model.EnginePover;
                well.Productivity = model.Productivity;
                well.ProtectionZoneArea = model.ProtectionZoneArea;
                well.FkIdActivityStatus = model.FkIdActivityStatus;
                well.FkIdWaterUseAssociation = model.FkIdWaterUseAssociation;
                well.FkIdOwnershipType = model.FkIdOwnershipType;
                well.FkIdOwner = model.FkIdOwner;
                well.FkIdProtectionType = model.FkIdProtectionType;
                well.RepairDate = model.RepairDate;
                well.ResidentialArea = model.ResidentialArea;
                well.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
                well.ExploitationDate = model.ExploitationDate;
                well.Description = model.Description;

                if (model.Objectid > 0)
                {
                    _db.Update(well);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public object LoadArtesianSubartesianWellFiles(DataSourceLoadOptions options, Guid RelGlobalId)
        {
            var list = _db.Asqueryable<Obj07ArtesianSubartesianWellsAttach>().Where(x => x.RelGlobalid == RelGlobalId).Select(x => new { x.RelGlobalid, x.Attachmentid, x.ContentType, x.AttName, DataSize = Helper.FormatSize(x.DataSize) });
            return DataSourceLoader.Load(list, options);
        }


        public IActionResult DeleteArtesianSubartesianWellFile(int key)
        {
            if (key == 0)
            {
                return BadRequest("Sənəd tapılmadı");
            }
            try
            {
                _db.DeleteById<Obj07ArtesianSubartesianWellsAttach>(key);
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
            int attachmentId = _db.Asqueryable<Obj07ArtesianSubartesianWellsAttach>().Max(x => x.Attachmentid);

            Obj07ArtesianSubartesianWellsAttach fl = new Obj07ArtesianSubartesianWellsAttach();
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
            Obj07ArtesianSubartesianWellsAttach doc = _db.Asqueryable<Obj07ArtesianSubartesianWellsAttach>().FirstOrDefault(x => x.Attachmentid == attachmentId);
            if (doc.Data == null) { return BadRequest("Fayl mövcud deyil!"); }

            var fileName = doc.AttName;
            byte[] buffer = doc.Data;
            return File(buffer, "application/x-msdownload", fileName);
        }

        public List<int> GetObjectIds(int artesianId)
        {
            return _db.Asqueryable<Obj07ArtesianSubartesianWell>(x => x.IdArtesianSubartesianWell == artesianId).Select(x => x.Objectid).ToList();
        }

        [HttpPost]
        public List<int> Filter(ArtesiaAndSubartesianFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            if (data.ScadaInfo == 1)
            {
                return _db.Asqueryable<Obj07ArtesianSubartesianWell>()
                    .Where(x => userRegionIds.Contains((int)x.FkAdminUnit) &&
                        (x.Name.Contains(data.Name) || data.Name == null) &&
                        (x.No.Contains(data.No) || data.No == null) &&
                        (data.FkIdAppointment == null || data.FkIdAppointment.Contains(x.FkIdAppointment)) &&
                        (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkAdminUnit)) &&
                        (data.FkIdActivityStatus == null || data.FkIdActivityStatus.Contains(x.FkIdActivityStatus)) &&
                        (data.FkIdTechnicalStatus == null || data.FkIdTechnicalStatus.Contains(x.FkIdTechnicalStatus)) &&
                        (data.FkIdOwnershipType == null || data.FkIdOwnershipType.Contains(x.FkIdOwnershipType)) &&
                        (data.FkIdOwner == null || data.FkIdOwner.Contains(x.FkIdOwner)) &&
                        (data.FkIdProtectionType == null || data.FkIdProtectionType.Contains(x.FkIdProtectionType)) &&
                        (data.FkIdArtesianType == null || data.FkIdArtesianType.Contains(x.FkIdArtesianType)) &&
                        (data.FkIdWaterUseAssociation == null || data.FkIdWaterUseAssociation.Contains(x.FkIdWaterUseAssociation)) &&


                        (x.ProtectionZoneArea >= data.ProtectionZoneAreaMin || data.ProtectionZoneAreaMin == null) &&
                        (x.ProtectionZoneArea <= data.ProtectionZoneAreaMax || data.ProtectionZoneAreaMax == null) &&

                        (x.Depth >= data.DepthMin || data.DepthMin == null) &&
                        (x.Depth <= data.DepthMax || data.DepthMax == null) &&

                        (x.ServiceArea >= data.ServiceAreaMin || data.ServiceAreaMin == null) &&
                        (x.ServiceArea <= data.ServiceAreaMax || data.ServiceAreaMax == null) &&

                        (x.Productivity >= data.ProductivityMin || data.ProductivityMin == null) &&
                        (x.Productivity <= data.ProductivityMax || data.ProductivityMax == null) &&

                        (x.EnginePover >= data.EnginePoverMin || data.EnginePoverMin == null) &&
                        (x.EnginePover <= data.EnginePoverMax || data.EnginePoverMax == null) &&

                        (x.ExploitationDate >= data.ExploitationDateMin || data.ExploitationDateMin == null) &&
                        (x.ExploitationDate <= data.ExploitationDateMax || data.ExploitationDateMax == null) &&

                        (x.ResidentialArea.Contains(data.ResidentialArea) || data.ResidentialArea == null) &&
                        (x.PumpModel.Contains(data.PumpModel) || data.PumpModel == null) &&
                        (x.PumpType.Contains(data.PumpType) || data.PumpType == null) &&
                        (x.RepairDate.Contains(data.RepairDate) || data.RepairDate == null) &&
                        (x.ScadaInfo == "1")
                    )
                    .Select(x => x.Objectid).Distinct().ToList();
            }
            else if (data.ScadaInfo == 2)
            {
                return _db.Asqueryable<Obj07ArtesianSubartesianWell>()
        .Where(x => userRegionIds.Contains((int)x.FkAdminUnit) &&
            (x.Name.Contains(data.Name) || data.Name == null) &&
            (x.No.Contains(data.No) || data.No == null) &&
            (data.FkIdAppointment == null || data.FkIdAppointment.Contains(x.FkIdAppointment)) &&
            (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkAdminUnit)) &&
            (data.FkIdActivityStatus == null || data.FkIdActivityStatus.Contains(x.FkIdActivityStatus)) &&
            (data.FkIdTechnicalStatus == null || data.FkIdTechnicalStatus.Contains(x.FkIdTechnicalStatus)) &&
            (data.FkIdOwnershipType == null || data.FkIdOwnershipType.Contains(x.FkIdOwnershipType)) &&
            (data.FkIdOwner == null || data.FkIdOwner.Contains(x.FkIdOwner)) &&
            (data.FkIdProtectionType == null || data.FkIdProtectionType.Contains(x.FkIdProtectionType)) &&
            (data.FkIdArtesianType == null || data.FkIdArtesianType.Contains(x.FkIdArtesianType)) &&
            (data.FkIdWaterUseAssociation == null || data.FkIdWaterUseAssociation.Contains(x.FkIdWaterUseAssociation)) &&


            (x.ProtectionZoneArea >= data.ProtectionZoneAreaMin || data.ProtectionZoneAreaMin == null) &&
            (x.ProtectionZoneArea <= data.ProtectionZoneAreaMax || data.ProtectionZoneAreaMax == null) &&

            (x.Depth >= data.DepthMin || data.DepthMin == null) &&
            (x.Depth <= data.DepthMax || data.DepthMax == null) &&

            (x.ServiceArea >= data.ServiceAreaMin || data.ServiceAreaMin == null) &&
            (x.ServiceArea <= data.ServiceAreaMax || data.ServiceAreaMax == null) &&

            (x.Productivity >= data.ProductivityMin || data.ProductivityMin == null) &&
            (x.Productivity <= data.ProductivityMax || data.ProductivityMax == null) &&

            (x.EnginePover >= data.EnginePoverMin || data.EnginePoverMin == null) &&
            (x.EnginePover <= data.EnginePoverMax || data.EnginePoverMax == null) &&

            (x.ExploitationDate >= data.ExploitationDateMin || data.ExploitationDateMin == null) &&
            (x.ExploitationDate <= data.ExploitationDateMax || data.ExploitationDateMax == null) &&

            (x.ResidentialArea.Contains(data.ResidentialArea) || data.ResidentialArea == null) &&
            (x.PumpModel.Contains(data.PumpModel) || data.PumpModel == null) &&
            (x.PumpType.Contains(data.PumpType) || data.PumpType == null) &&
            (x.RepairDate.Contains(data.RepairDate) || data.RepairDate == null) &&
                            (x.ScadaInfo == "2")
        )
        .Select(x => x.Objectid).Distinct().ToList();
            }
            else
            {
                return _db.Asqueryable<Obj07ArtesianSubartesianWell>()
        .Where(x => userRegionIds.Contains((int)x.FkAdminUnit) &&
            (x.Name.Contains(data.Name) || data.Name == null) &&
            (x.No.Contains(data.No) || data.No == null) &&
            (data.FkIdAppointment == null || data.FkIdAppointment.Contains(x.FkIdAppointment)) &&
            (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkAdminUnit)) &&
            (data.FkIdActivityStatus == null || data.FkIdActivityStatus.Contains(x.FkIdActivityStatus)) &&
            (data.FkIdTechnicalStatus == null || data.FkIdTechnicalStatus.Contains(x.FkIdTechnicalStatus)) &&
            (data.FkIdOwnershipType == null || data.FkIdOwnershipType.Contains(x.FkIdOwnershipType)) &&
            (data.FkIdOwner == null || data.FkIdOwner.Contains(x.FkIdOwner)) &&
            (data.FkIdProtectionType == null || data.FkIdProtectionType.Contains(x.FkIdProtectionType)) &&
            (data.FkIdArtesianType == null || data.FkIdArtesianType.Contains(x.FkIdArtesianType)) &&
            (data.FkIdWaterUseAssociation == null || data.FkIdWaterUseAssociation.Contains(x.FkIdWaterUseAssociation)) &&


            (x.ProtectionZoneArea >= data.ProtectionZoneAreaMin || data.ProtectionZoneAreaMin == null) &&
            (x.ProtectionZoneArea <= data.ProtectionZoneAreaMax || data.ProtectionZoneAreaMax == null) &&

            (x.Depth >= data.DepthMin || data.DepthMin == null) &&
            (x.Depth <= data.DepthMax || data.DepthMax == null) &&

            (x.ServiceArea >= data.ServiceAreaMin || data.ServiceAreaMin == null) &&
            (x.ServiceArea <= data.ServiceAreaMax || data.ServiceAreaMax == null) &&

            (x.Productivity >= data.ProductivityMin || data.ProductivityMin == null) &&
            (x.Productivity <= data.ProductivityMax || data.ProductivityMax == null) &&

            (x.EnginePover >= data.EnginePoverMin || data.EnginePoverMin == null) &&
            (x.EnginePover <= data.EnginePoverMax || data.EnginePoverMax == null) &&

            (x.ExploitationDate >= data.ExploitationDateMin || data.ExploitationDateMin == null) &&
            (x.ExploitationDate <= data.ExploitationDateMax || data.ExploitationDateMax == null) &&

            (x.ResidentialArea.Contains(data.ResidentialArea) || data.ResidentialArea == null) &&
            (x.PumpModel.Contains(data.PumpModel) || data.PumpModel == null) &&
            (x.PumpType.Contains(data.PumpType) || data.PumpType == null) &&
            (x.RepairDate.Contains(data.RepairDate) || data.RepairDate == null)
        )
        .Select(x => x.Objectid).Distinct().ToList();
            }
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
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj07ArtesianSubartesianWell>().Where(x => (ids.Contains(x.Objectid))).Select(x => new
            {
                x.IdArtesianSubartesianWell,
                x.Objectid,
                x.Name,
                x.ExploitationDate,
                x.Controlled,
                x.FkIdTechnicalStatus,
                x.FkIdActivityStatus,
                x.FkAdminUnit,
                x.FkIdProtectionType,
                x.FkIdAppointment,
                x.FkIdOwnershipType,
                x.FkIdOwner,
                x.FkIdArtesianType,
                x.FkIdWaterUseAssociation,
                x.Depth,
                x.Description,
                x.Productivity,
                x.ProtectionZoneArea,
                x.PumpModel,
                x.EnginePover,
                x.No,
                ScadaInfo = x.ScadaInfo ?? "2",
                x.PumpType,
                x.RepairDate,
                x.ServiceArea,
                x.ResidentialArea,
            }), loadOptions);
        }


        [HttpPost]
        public object Get(int id)
        {

            return _db.Asqueryable<Obj07ArtesianSubartesianWell>().Where(x => x.IdArtesianSubartesianWell == id).ToList().Select(x => new
            {
                x.IdArtesianSubartesianWell,
                Name = x.Name ?? "məlumat yoxdur",
                ExploitationDate = x.ExploitationDate > 0 ? x.ExploitationDate.ToString() : "məlumat yoxdur",
                No = x.No ?? "məlumat yoxdur",
                Productivity = x.Productivity > 0 ? decimal.Round(x.Productivity ?? 0, 2).ToString() : "məlumat yoxdur",
                Type = _db.Asqueryable<SArtesianType>().FirstOrDefault(z => z.IdArtesianType == (int?)x.FkIdArtesianType)?.Name ?? "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int?)x.FkAdminUnit)?.Name ?? "məlumat yoxdur",
                Appointment = _db.Asqueryable<SAppointment>().FirstOrDefault(z => z.IdAppointment == (int?)x.FkIdAppointment)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == (int?)x.FkIdOwner)?.Name ?? "məlumat yoxdur",
            }).FirstOrDefault();
        }

    }
}
