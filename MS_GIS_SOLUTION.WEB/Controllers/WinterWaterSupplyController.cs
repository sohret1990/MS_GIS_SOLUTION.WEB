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
    public class WinterWaterSupplyController : BaseController
    {
        public WinterWaterSupplyController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Qış otlaqlarının su təminatı sistemləri";
        }

        public IActionResult CreateEdit(int? id, bool mode = false)
        {
            Obj09WinterWaterSupplyViewModel model = new Obj09WinterWaterSupplyViewModel();

            if (id > 0)
            {
                Obj09WinterWaterSupplySystem waterSupplySystem = _db.Asqueryable<Obj09WinterWaterSupplySystem>().FirstOrDefault(x => x.IdWinterWaterSupplySystems == id);
                model.Name = waterSupplySystem.Name;
                model.Objectid = waterSupplySystem.Objectid;
                model.IdWinterWaterSupplySystems = waterSupplySystem.IdWinterWaterSupplySystems;
                model.ServiceArea = waterSupplySystem.ServiceArea;
                model.DeviceCount = waterSupplySystem.DeviceCount;
                model.ClosedIrrigationNetworks = waterSupplySystem.ClosedIrrigationNetworks;
                model.ExploitationDate = waterSupplySystem.ExploitationDate;
                model.FkIdAdminUnit = waterSupplySystem.FkIdAdminUnit;
                model.FkIdActivityStatus = waterSupplySystem.FkIdActivityStatus;
                model.FkIdOwnershipType = waterSupplySystem.FkIdOwnershipType;
                model.FkIdOwner = waterSupplySystem.FkIdOwner;
                model.Description = waterSupplySystem.Description;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
            }

            return View(model);
        }

        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj09WinterWaterSupplySystem>().Select(x => new
            {
                x.Name,
                x.Objectid,
                x.IdWinterWaterSupplySystems,
                x.ServiceArea,
                x.DeviceCount,
                x.ClosedIrrigationNetworks,
                x.ExploitationDate,
                x.FkIdAdminUnit,
                x.FkIdActivityStatus,
                x.FkIdOwnershipType,
                x.FkIdOwner,
                x.Description
            }), loadOptions);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int key)
        {
            bool result = true;
            _db.DeleteById<Obj09WinterWaterSupplySystem>(key);
            result = _db.SaveChanges() > 0;

            if (result)
                return Ok();
            else
                return BadRequest("Əməliyyat zamanı xəta baş verdi!");
        }

        public IActionResult Save(Obj09WinterWaterSupplyViewModel model)
        {
            Obj09WinterWaterSupplySystem winterWaterSupplySystem = _db.Asqueryable<Obj09WinterWaterSupplySystem>().FirstOrDefault(x => x.Objectid == model.Objectid && x.IdWinterWaterSupplySystems == model.IdWinterWaterSupplySystems) ?? new Obj09WinterWaterSupplySystem();

            if (winterWaterSupplySystem != null)
            {
                winterWaterSupplySystem.Name = model.Name;
                winterWaterSupplySystem.Objectid = model.Objectid ?? 0;
                winterWaterSupplySystem.IdWinterWaterSupplySystems = model.IdWinterWaterSupplySystems;
                winterWaterSupplySystem.ServiceArea = model.ServiceArea;
                winterWaterSupplySystem.DeviceCount = model.DeviceCount;
                winterWaterSupplySystem.ClosedIrrigationNetworks = model.ClosedIrrigationNetworks;
                winterWaterSupplySystem.ExploitationDate = model.ExploitationDate;
                winterWaterSupplySystem.FkIdAdminUnit = model.FkIdAdminUnit;
                winterWaterSupplySystem.FkIdActivityStatus = model.FkIdActivityStatus;
                winterWaterSupplySystem.FkIdOwnershipType = model.FkIdOwnershipType;
                winterWaterSupplySystem.FkIdOwner = model.FkIdOwner;
                winterWaterSupplySystem.Description = model.Description;


                if (model.Objectid > 0)
                {
                    _db.Update(winterWaterSupplySystem);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }




        [HttpPost]
        public List<int> Filter(WinterWaterSupplyFilterModel data)
        {
            return _db.Asqueryable<Obj09WinterWaterSupplySystem>()
                .Where(x =>
                    (x.Name.Contains(data.Name) || data.Name == null) &&
                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkIdAdminUnit)) &&
                    (data.FkIdActivityStatus == null || data.FkIdActivityStatus.Contains(x.FkIdActivityStatus)) &&

                    (data.FkIdOwnershipType == null || data.FkIdOwnershipType.Contains(x.FkIdOwnershipType)) &&
                    (data.FkIdOwner == null || data.FkIdOwner.Contains(x.FkIdOwner)) &&

                    (x.ExploitationDate >= data.ExploitationDateMin || data.ExploitationDateMin == null) &&
                    (x.ExploitationDate <= data.ExploitationDateMax || data.ExploitationDateMax == null) &&

                    (x.ClosedIrrigationNetworks >= data.ClosedIrrigationNetworksMin || data.ClosedIrrigationNetworksMin == null) &&
                    (x.ClosedIrrigationNetworks <= data.ClosedIrrigationNetworksMax || data.ClosedIrrigationNetworksMax == null) &&

                    (x.DeviceCount >= data.DeviceCountMin || data.DeviceCountMin == null) &&
                    (x.DeviceCount <= data.DeviceCountMax || data.DeviceCountMax == null) &&

                    (x.ServiceArea >= data.ServiceAreaMin || data.ServiceAreaMin == null) &&
                    (x.ServiceArea <= data.ServiceAreaMax || data.ServiceAreaMax == null)

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
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj09WinterWaterSupplySystem>().Where(x => (ids.Contains(x.Objectid))).Select(x => new
            {
                x.IdWinterWaterSupplySystems,
                x.Objectid,
                x.ClosedIrrigationNetworks,
                x.Name,
                x.ExploitationDate,
                x.Controlled,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdOwnershipType,
                x.FkIdOwner,
                x.Description,
                x.ServiceArea,
                x.DeviceCount,
            }), loadOptions);
        }

        [HttpPost]
        public object Get(int id)
        {

            return _db.Asqueryable<Obj09WinterWaterSupplySystem>().Where(x => x.IdWinterWaterSupplySystems == id).ToList().Select(x => new
            {
                x.IdWinterWaterSupplySystems,
                Name = x.Name ?? "məlumat yoxdur",
                ServiceArea = x.ServiceArea > 0 ? decimal.Round(x.ServiceArea ?? 0, 2).ToString() : "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int)x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == (int)x.FkIdOwner)?.Name ?? "məlumat yoxdur"
            }).FirstOrDefault();
        }
    }
}
