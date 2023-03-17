using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Models.FilterVM;
using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ReservoirSystemController : BaseController
    {
        public ReservoirSystemController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Su anbarları / Su tutarlar";
        }

        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj04ReservoirSystem>().Where(x => x.IdReservoirSystem != null).Select(x => new
            {
                x.IdReservoirSystem,
                x.Description,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.FkIdProtectionType,
                x.FkIdSource,
                x.FkIdSourceType,
                x.FkIdAppointment,
                x.FkIdAppointmentAddition,
                x.FkIdTechnicalStatus,
                x.Name,
                x.NormalPressure,
                x.Objectid,
                x.ProtectionZoneArea,
                x.MinorAreaA,
                x.MinorAreaB,
                x.FkIdWrRegulationType,
                x.ServiceArea,
                x.FkIdWrLocationType,
                x.DeadVolume,
                x.VolumeA,
                x.VolumeB

            }), loadOptions);
        }

        public IActionResult CreateEdit(int? id, bool mode = false)
        {
            Obj04ReservoirSystemViewModel model = new Obj04ReservoirSystemViewModel();

            if (id != null)
            {
                Obj04ReservoirSystem reservoirSystem = _db.Asqueryable<Obj04ReservoirSystem>().FirstOrDefault(x => x.IdReservoirSystem == id);
                model.Name = reservoirSystem.Name;
                model.FkIdAdminUnit = reservoirSystem.FkIdAdminUnit;
                model.FkIdActivityStatus = reservoirSystem.FkIdActivityStatus;
                model.FkIdOwnershipType = reservoirSystem.FkIdOwnershipType;
                model.FkIdOwner = reservoirSystem.FkIdOwner;
                model.FkIdAppointment = reservoirSystem.FkIdAppointment;
                model.FkIdAppointmentAddition = reservoirSystem.FkIdAppointmentAddition;
                model.FkIdProtectionType = reservoirSystem.FkIdProtectionType;
                model.FkIdSourceType = reservoirSystem.FkIdSourceType;
                model.FkIdSource = reservoirSystem.FkIdSource;
                model.FkIdTechnicalStatus = reservoirSystem.FkIdTechnicalStatus;
                model.ExploitationDate = reservoirSystem.ExploitationDate;
                model.Description = reservoirSystem.Description;
                model.MinorAreaA = reservoirSystem.MinorAreaA;
                model.MinorAreaB = reservoirSystem.MinorAreaB;
                model.FkIdWrRegulationType = reservoirSystem.FkIdWrRegulationType;
                model.VolumeA = reservoirSystem.VolumeA;
                model.Objectid = reservoirSystem.Objectid;
                model.VolumeB = reservoirSystem.VolumeB;
                model.IdReservoirSystem = reservoirSystem.IdReservoirSystem;
                model.DeadVolume = reservoirSystem.DeadVolume;
                model.ServiceArea = reservoirSystem.ServiceArea;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Save(Obj04ReservoirSystemViewModel model)
        {
            Obj04ReservoirSystem reservoirSystem = _db.Asqueryable<Obj04ReservoirSystem>().FirstOrDefault(x => x.Objectid == model.Objectid && x.IdReservoirSystem == model.IdReservoirSystem) ?? new Obj04ReservoirSystem();

            if (reservoirSystem != null)
            {
                reservoirSystem.Name = model.Name;
                reservoirSystem.FkIdAdminUnit = model.FkIdAdminUnit;
                reservoirSystem.FkIdActivityStatus = model.FkIdActivityStatus;
                reservoirSystem.FkIdOwnershipType = model.FkIdOwnershipType;
                reservoirSystem.FkIdOwner = model.FkIdOwner;
                reservoirSystem.FkIdAppointmentAddition = model.FkIdAppointmentAddition;
                reservoirSystem.FkIdAppointment = model.FkIdAppointment;
                reservoirSystem.FkIdProtectionType = model.FkIdProtectionType;
                reservoirSystem.FkIdSourceType = model.FkIdSourceType;
                reservoirSystem.FkIdSource = model.FkIdSource;
                reservoirSystem.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
                reservoirSystem.ExploitationDate = model.ExploitationDate;
                reservoirSystem.Description = model.Description;
                reservoirSystem.MinorAreaA = model.MinorAreaA;
                reservoirSystem.MinorAreaB = model.MinorAreaB;
                reservoirSystem.VolumeA = model.VolumeA;
                reservoirSystem.Objectid = model.Objectid ?? 0;
                reservoirSystem.IdReservoirSystem = model.IdReservoirSystem;
                reservoirSystem.VolumeB = model.VolumeB;
                reservoirSystem.DeadVolume = model.DeadVolume;
                reservoirSystem.ServiceArea = model.ServiceArea;


                if (model.Objectid > 0)
                {
                    _db.Update(reservoirSystem);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public List<int> Filter(ReservoirSystemFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();

            return _db.Asqueryable<Obj04ReservoirSystem>()
                .Where(x => userRegionIds.Contains((int)x.FkIdAdminUnit) &&
                    (x.Name.Contains(data.Name) || data.Name == null) &&
                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkIdAdminUnit)) &&
                    (data.FkIdSource == null || data.FkIdSource.Contains(x.FkIdSource)) &&
                    (data.FkIdSourceType == null || data.FkIdSourceType.Contains(x.FkIdSourceType)) &&
                    (data.FkIdActivityStatus == null || data.FkIdActivityStatus.Contains(x.FkIdActivityStatus)) &&
                    (data.FkIdTechnicalStatus == null || data.FkIdTechnicalStatus.Contains(x.FkIdTechnicalStatus)) &&
                    (data.FkIdOwnershipType == null || data.FkIdOwnershipType.Contains(x.FkIdOwnershipType)) &&
                    (data.FkIdOwner == null || data.FkIdOwner.Contains(x.FkIdOwner)) &&
                    (data.FkIdProtectionType == null || data.FkIdProtectionType.Contains(x.FkIdProtectionType)) &&
                    (data.FkIdWrLocationType == null || data.FkIdWrLocationType.Contains(x.FkIdWrLocationType)) &&
                    (data.FkIdWrRegulationType == null || data.FkIdWrRegulationType.Contains(x.FkIdWrRegulationType)) &&
                    (data.FkIdAppointmentAddition == null || data.FkIdAppointmentAddition.Contains(x.FkIdAppointmentAddition)) &&
                    (x.Dam.Contains(data.Dam) || data.Dam == null) &&

                    (x.ExploitationDate >= data.ExploitationDateMin || data.ExploitationDateMin == null) &&
                    (x.ExploitationDate <= data.ExploitationDateMax || data.ExploitationDateMax == null) &&

                    (x.ProtectionZoneArea >= data.ProtectionZoneAreaMin || data.ProtectionZoneAreaMax == null) &&
                    (x.ProtectionZoneArea <= data.ProtectionZoneAreaMax || data.ProtectionZoneAreaMax == null) &&

                    (x.ServiceArea >= data.ServiceAreaMin || data.ServiceAreaMin == null) &&
                    (x.ServiceArea <= data.ServiceAreaMax || data.ServiceAreaMax == null) &&

                    (x.VolumeA >= data.VolumeAMin || data.VolumeAMin == null) &&
                    (x.VolumeA <= data.VolumeAMax || data.VolumeAMax == null) &&

                    (x.DeadVolume >= data.DeadVolumeMin || data.DeadVolumeMin == null) &&
                    (x.DeadVolume <= data.DeadVolumeMax || data.DeadVolumeMax == null) &&

                    (x.VolumeB >= data.VolumeBMin || data.VolumeBMin == null) &&
                    (x.VolumeB <= data.VolumeBMax || data.VolumeBMax == null) &&

                    (x.MinorAreaA >= data.MinorAreaAMin || data.MinorAreaAMin == null) &&
                    (x.MinorAreaA <= data.MinorAreaAMax || data.MinorAreaAMax == null) &&

                    (x.MinorAreaB >= data.MinorAreaBMin || data.MinorAreaBMin == null) &&
                    (x.MinorAreaB <= data.MinorAreaBMax || data.MinorAreaBMax == null) &&

                    (x.NormalPressure >= data.NormalPressureMin || data.NormalPressureMin == null) &&
                    (x.NormalPressure <= data.NormalPressureMax || data.NormalPressureMax == null)

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
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj04ReservoirSystem>().Where(x => (ids.Contains(x.Objectid))).Select(x => new
            {
                x.Name,
                x.FkIdAdminUnit,
                x.FkIdActivityStatus,
                x.FkIdOwnershipType,
                x.FkIdOwner,
                x.FkIdAppointmentAddition,
                x.FkIdAppointment,
                x.FkIdProtectionType,
                x.FkIdSourceType,
                x.FkIdSource,
                x.FkIdTechnicalStatus,
                x.ExploitationDate,
                x.Description,
                x.MinorAreaA,
                x.MinorAreaB,
                x.VolumeA,
                x.Objectid,
                x.IdReservoirSystem,
                x.VolumeB,
                x.DeadVolume,
                x.ServiceArea
            }), loadOptions);
        }

        [HttpPost]
        public object Get(int id)
        {
            return _db.Asqueryable<Obj04ReservoirSystem>().Where(x => x.IdReservoirSystem == id).ToList().Select(x => new
            {
                Name = x.Name ?? "məlumat yoxdur",
                Source = _db.Asqueryable<ObjViewSource>().FirstOrDefault(z => z.Id == x.FkIdSource)?.Name ?? "məlumat yoxdur",
                Appointment = _db.Asqueryable<SAppointment>().FirstOrDefault(z => z.IdAppointment == x.FkIdAppointment)?.Name ?? "məlumat yoxdur",
                SubAppointment = _db.Asqueryable<SAppointment>().FirstOrDefault(z => z.IdAppointment == x.FkIdAppointmentAddition)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == x.FkIdOwner)?.Name ?? "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Type = _db.Asqueryable<SWaterRetentionLocationType>().FirstOrDefault(z => z.IdWRetLocationType == x.FkIdWrLocationType)?.Name ?? "məlumat yoxdur"
            }).FirstOrDefault();
        }
    }
}
