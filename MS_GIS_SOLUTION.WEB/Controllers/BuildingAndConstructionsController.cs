using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Models;
using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class BuildingAndConstructionsController : BaseController
    {
        public BuildingAndConstructionsController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Bina və tikililər";
        }

        public IActionResult CreateEdit(int? id, bool mode = false)
        {
            Obj11BuildingsConstructionViewModel model = new Obj11BuildingsConstructionViewModel();

            if (id > 0)
            {
                Obj11BuildingsConstruction building = _db.Asqueryable<Obj11BuildingsConstruction>().FirstOrDefault(x => x.IdBuildingsConstructions == id);
                model.Name = building.Name;
                model.Objectid = building.Objectid;
                model.ReyestrNumber = building.ReyestrNumber;
                model.ServiceArea = building.ServiceArea;
                model.IdBuildingsConstructions = building.IdBuildingsConstructions;
                model.UsageMode = building.UsageMode;
                model.Area = building.Area;
                model.AreaA = building.AreaA;
                model.AreaB = building.AreaB;
                model.FkIdAdminUnit = building.FkIdAdminUnit;
                model.FkIdActivityStatus = building.FkIdActivityStatus;
                model.FkIdAppointment = building.FkIdAppointment;
                model.AdditionalAdminUnit = building.AdditionalAdminUnit;
                model.FkIdOwnershipType = building.FkIdOwnershipType;
                model.FkIdProtectionType = building.FkIdProtectionType;
                model.FkIdOwner = building.FkIdOwner;
                model.ExploitationDate = building.ExploitationDate;
                model.FkIdTechnicalStatus = building.FkIdTechnicalStatus;
                model.Description = building.Description;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
            }

            return View(model);
        }

        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj11BuildingsConstruction>().Select(x => new
            {
                x.IdBuildingsConstructions,
                x.Description,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.Area,
                x.Objectid,
                x.AreaA,
                x.AreaB,
                x.ServiceArea,
                x.Name,
                x.ReyestrNumber,
            }), loadOptions);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int key)
        {
            bool result = true;
            _db.DeleteById<Obj11BuildingsConstruction>(key);
            result = _db.SaveChanges() > 0;

            if (result)
                return Ok();
            else
                return BadRequest("Əməliyyat zamanı xəta baş verdi!");
        }

        [HttpPost]

        public IActionResult Save(Obj11BuildingsConstructionViewModel model)
        {
            Obj11BuildingsConstruction buildingsConstruction = _db.Asqueryable<Obj11BuildingsConstruction>().FirstOrDefault(x => x.Objectid == model.Objectid && x.IdBuildingsConstructions == model.IdBuildingsConstructions) ?? new Obj11BuildingsConstruction();

            if (buildingsConstruction != null)
            {
                buildingsConstruction.Name = model.Name;
                buildingsConstruction.Objectid = model.Objectid ?? 0;
                buildingsConstruction.ReyestrNumber = model.ReyestrNumber;
                buildingsConstruction.ServiceArea = model.ServiceArea;
                buildingsConstruction.IdBuildingsConstructions = model.IdBuildingsConstructions;
                buildingsConstruction.UsageMode = model.UsageMode;
                buildingsConstruction.Area = model.Area;
                buildingsConstruction.AreaA = model.AreaA;
                buildingsConstruction.AreaB = model.AreaB;
                buildingsConstruction.FkIdAdminUnit = model.FkIdAdminUnit;
                buildingsConstruction.FkIdActivityStatus = model.FkIdActivityStatus;
                buildingsConstruction.FkIdOwnershipType = model.FkIdOwnershipType;
                buildingsConstruction.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
                buildingsConstruction.FkIdAppointment = model.FkIdAppointment;
                buildingsConstruction.FkIdProtectionType = model.FkIdProtectionType;
                buildingsConstruction.FkIdOwner = model.FkIdOwner;
                buildingsConstruction.ExploitationDate = model.ExploitationDate;
                buildingsConstruction.Description = model.Description;

                if (model.Objectid > 0)
                {
                    _db.Update(buildingsConstruction);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public object Get(int id)
        {

            return _db.Asqueryable<Obj11BuildingsConstruction>().Where(x => x.IdBuildingsConstructions == id).ToList().Select(x => new
            {
                x.IdBuildingsConstructions,
                Name = x.Name ?? "məlumat yoxdur",
                Area = x.Area > 0 ? decimal.Round(x.Area ?? 0, 2).ToString() : "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int)x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Appointment = _db.Asqueryable<SAppointment>().FirstOrDefault(z => z.IdAppointment == (int)x.FkIdAppointment)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == (int)x.FkIdOwner)?.Name ?? "məlumat yoxdur",
            }).FirstOrDefault();
        }


        [HttpPost]
        public List<int> Filter(BuildingAndConstructionsFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            return _db.Asqueryable<Obj11BuildingsConstruction>()
                .Where(x =>
                userRegionIds.Contains((int)x.FkIdAdminUnit) &&
                (data.FkIdActivityStatus == null || data.FkIdActivityStatus.Contains(x.FkIdActivityStatus)) &&
                (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkIdAdminUnit)) &&
                (data.FkIdAppointment == null || data.FkIdAppointment.Contains(x.FkIdAppointment)) &&
                (data.FkIdOwner == null || data.FkIdOwner.Contains(x.FkIdOwner)) &&
                (data.FkIdOwnershipType == null || data.FkIdOwnershipType.Contains(x.FkIdOwnershipType)) &&
                //(data.FkIdProtectionType == null || data.FkIdProtectionType.Contains(x.FkIdProtectionType)) &&
                (data.FkIdTechnicalStatus == null || data.FkIdTechnicalStatus.Contains(x.FkIdTechnicalStatus)) &&
                
                (data.Name == null || x.Name.Contains(data.Name) ) &&
                (data.ReyestrNumber== null || x.ReyestrNumber.Contains(data.ReyestrNumber) ) &&
                (data.UsageMode== null || x.UsageMode.Contains(data.UsageMode) ) &&
                (data.AdditionalAdminUnit== null || x.AdditionalAdminUnit.Contains(data.AdditionalAdminUnit) ) &&
               
                (data.AreaMin== null || x.Area >= data.AreaMin) &&
                (data.AreaMax == null || x.Area <= data.AreaMax) &&

                (data.AreaAMin == null || x.AreaA >= data.AreaAMin) &&
                (data.AreaAMax == null || x.AreaA <= data.AreaAMax) &&

                (data.AreaBMin == null || x.AreaB >= data.AreaBMin) &&
                (data.AreaBMax == null || x.AreaB <= data.AreaBMax) &&

                (data.ServiceAreaMin == null || x.ServiceArea >= data.ServiceAreaMin) &&
                (data.ServiceAreaMax == null || x.ServiceArea <= data.ServiceAreaMax) &&
                
                (data.ExploitationDateMin == null || x.ExploitationDate >= data.ExploitationDateMin) &&
                (data.ExploitationDateMax == null || x.ExploitationDate <= data.ExploitationDateMax) 
                
                )
                .Select(x => x.Objectid).ToList();
        }



        [HttpPost]
        public List<int> GetObjectIds(int channelId, List<int?> regionIds)
        {
            return _db.Asqueryable<Obj01ChannelsAdminUnit>(x => (regionIds.Count == 0 || regionIds.Contains(x.FkIdAdminUnit)) && x.FkIdChannel == channelId).Select(x => x.Objectid).ToList();
        }

        [HttpPost]
        public Task<LoadResult> LoadFilter(DataSourceLoadOptions loadOptions, string objectIds)
        {
            List<int> ids = new List<int>();
            if (!string.IsNullOrEmpty(objectIds))
            {
                ids = objectIds.Split(",").Select(int.Parse).ToList();
            }

            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj11BuildingsConstruction>().Where(x => x.IsDeleted == 0 && (ids.Contains(x.Objectid))).Select(x => new
            {
                x.IdBuildingsConstructions,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdAppointment,
                x.FkIdOwner,
                x.FkIdOwnershipType,
                x.FkIdTechnicalStatus,
                x.Name,
                x.ReyestrNumber,
                x.ServiceArea,
                x.UsageMode,
                x.AdditionalAdminUnit,
                x.Area,
                x.AreaA,
                x.AreaB,
                x.Description,
                x.Objectid
            }), loadOptions);
        }

    }
}
