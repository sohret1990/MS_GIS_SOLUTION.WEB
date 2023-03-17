using System.Collections.Generic;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using MS_GIS_SOLUTION.WEB.Models.FilterVM;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ProtectionBarrierController : BaseController
    {
        public ProtectionBarrierController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Mühafizə bəndləri";
        }

        public IActionResult CreateEdit(int? id, bool mode = false)
        {
            Obj10ProtectionBarrierViewModel model = new Obj10ProtectionBarrierViewModel();

            if (id > 0)
            {
                Obj10ProtectionBarrier protectionBarrier = _db.Asqueryable<Obj10ProtectionBarrier>().FirstOrDefault(x => x.IdProtectionBarriers == id);
                model.Name = protectionBarrier.Name;
                model.FkIdAdminUnit = protectionBarrier.FkIdAdminUnit;
                model.FkIdActivityStatus = protectionBarrier.FkIdActivityStatus;
                model.FkIdOwnershipType = protectionBarrier.FkIdOwnershipType;
                model.FkIdOwner = protectionBarrier.FkIdOwner;
                model.FkIdRiver = protectionBarrier.FkIdRiver;
                model.IdProtectionBarriers = protectionBarrier.IdProtectionBarriers;
                model.FkIdTechnicalStatus = protectionBarrier.FkIdTechnicalStatus;
                model.ExploitationDate = protectionBarrier.ExploitationDate;
                model.Description = protectionBarrier.Description;
                model.BarrierHeight = protectionBarrier.BarrierHeight;
                model.BarrierLength = protectionBarrier.BarrierLength;
                model.FkIdAppointment = protectionBarrier.FkIdAppointment;
                model.Objectid = protectionBarrier.Objectid;
                model.FkIdBarrierType = protectionBarrier.FkIdBarrierType;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
            }

            return View(model);
        }

        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj10ProtectionBarrier>().Select(x => new
            {
                x.BarrierHeight,
                x.BarrierLength,
                x.Description,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdAppointment,
                x.FkIdBarrierType,
                x.FkIdOwner,
                x.FkIdRiver,
                x.FkIdOwnershipType,
                x.FkIdTechnicalStatus,
                x.Objectid,
                x.IdProtectionBarriers,
                x.Name,
                //x.RiverName
            }), loadOptions);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int key)
        {
            bool result = true;
            _db.DeleteById<Obj10ProtectionBarrier>(key);
            result = _db.SaveChanges() > 0;

            if (result)
                return Ok();
            else
                return BadRequest("Əməliyyat zamanı xəta baş verdi!");
        }


        [HttpPost]

        public IActionResult Save(Obj10ProtectionBarrierViewModel model)
        {
            Obj10ProtectionBarrier protectionBarrier = _db.Asqueryable<Obj10ProtectionBarrier>().FirstOrDefault(x => x.Objectid == model.Objectid && x.IdProtectionBarriers == model.IdProtectionBarriers) ?? new Obj10ProtectionBarrier();

            if (protectionBarrier != null)
            {
                protectionBarrier.Name = model.Name;
                protectionBarrier.FkIdAdminUnit = model.FkIdAdminUnit;
                protectionBarrier.FkIdActivityStatus = model.FkIdActivityStatus;
                protectionBarrier.FkIdOwnershipType = model.FkIdOwnershipType;
                protectionBarrier.FkIdOwner = model.FkIdOwner;
                protectionBarrier.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
                protectionBarrier.ExploitationDate = model.ExploitationDate;
                protectionBarrier.Description = model.Description;
                protectionBarrier.BarrierHeight = model.BarrierHeight;
                protectionBarrier.BarrierLength = model.BarrierLength;
                protectionBarrier.FkIdBarrierType = model.FkIdBarrierType;
                protectionBarrier.FkIdRiver = model.FkIdRiver;
                protectionBarrier.FkIdAppointment = model.FkIdAppointment;
                protectionBarrier.Objectid = model.Objectid ?? 0;
                protectionBarrier.IdProtectionBarriers = model.IdProtectionBarriers;


                if (model.Objectid > 0)
                {
                    _db.Update(protectionBarrier);
                }

                _db.SaveChanges();
            }

            return RedirectToAction("Index");
        }



        [HttpPost]
        public List<int> Filter(ProtectionBarrierFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            return _db.Asqueryable<Obj10ProtectionBarrier>()
                .Where(x =>
                userRegionIds.Contains((int)x.FkIdAdminUnit) &&
                    (x.Name.Contains(data.Name) || data.Name == null) &&
                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkIdAdminUnit)) &&
                    (data.FkIdOwner == null || data.FkIdOwner.Contains(x.FkIdOwner)) &&
                    (data.FkIdOwnershipType == null || data.FkIdOwnershipType.Contains(x.FkIdOwnershipType)) &&
                    (data.FkIdActivityStatus == null || data.FkIdActivityStatus.Contains(x.FkIdActivityStatus)) &&
                    (data.FkIdRiver == null || data.FkIdRiver.Contains(x.FkIdRiver)) &&
                    (data.FkIdBarrierType == null || data.FkIdBarrierType.Contains(x.FkIdBarrierType)) &&
                    (data.FkIdTechnicalStatus == null || data.FkIdTechnicalStatus.Contains(x.FkIdTechnicalStatus)) &&


                    (x.ExploitationDate >= data.ExploitationDateMin || data.ExploitationDateMin == null) &&
                    (x.ExploitationDate <= data.ExploitationDateMax || data.ExploitationDateMax == null) &&

                    (x.BarrierLength >= data.BarrierLengthMin || data.BarrierLengthMin == null) &&
                    (x.BarrierLength <= data.BarrierLengthMax || data.BarrierLengthMax == null) &&

                    (x.BarrierHeight >= data.BarrierHeightMin || data.BarrierHeightMin == null) &&
                    (x.BarrierHeight <= data.BarrierHeightMax || data.BarrierHeightMax == null)

                    
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
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj10ProtectionBarrier>().Where(x => (ids.Contains(x.Objectid))).Select(x => new
            {
                x.IdProtectionBarriers,
                x.Objectid,
                x.Name,
                x.ExploitationDate,
                x.FkIdActivityStatus,
                x.FkIdAdminUnit,
                x.FkIdOwnershipType,
                x.FkIdBarrierType,
                x.FkIdAppointment,
                x.FkIdOwner,
                x.BarrierLength,
                x.BarrierHeight,
                x.FkIdRiver,
                x.FkIdTechnicalStatus,
                x.Description
            }), loadOptions);
        }


        [HttpPost]
        public object Get(int id)
        {

            return _db.Asqueryable<Obj10ProtectionBarrier>().Where(x => x.IdProtectionBarriers == id).ToList().Select(x => new
            {
                x.IdProtectionBarriers,
                Name = x.Name ?? "məlumat yoxdur",
                Type = _db.Asqueryable<SBarrierType>().FirstOrDefault(z => z.IdBarrierType == (int?)x.FkIdBarrierType)?.Name ?? "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int?)x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == (int?)x.FkIdOwner)?.Name ?? "məlumat yoxdur"
            }).FirstOrDefault();
        }

    }
}
