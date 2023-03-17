using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MS_GIS_SOLUTION.WEB.Models.FilterVM;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class CollectorController : BaseController
    {
        public CollectorController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Kollektor drenaj şəbəkələri";
        }

        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj03CollectorView>().Where(x => x.IdCollector != null).OrderByDescending(x => x.Objectid).Select(x => new
            {
                x.IdCollector,
                x.ExploitationDate,
                x.Controlled,
                x.Description,
                x.FkIdObjectType,
                x.Name,
                //x.SumOfCapacity,
                x.MinCapacity,
                x.MaxCapacity,
                x.FkIdAppointment,
                x.FkReceivingSourceType,
                x.FkIdReceivingSource,
                x.Objectid

            }), loadOptions);
        }

        public IActionResult CreateEdit(int? id, int fromSave = 0, bool mode = false)
        {
            Obj03CollectorViewModel model = new Obj03CollectorViewModel();

            if (id != null)
            {
                Obj03Collector collector = _db.Asqueryable<Obj03Collector>().FirstOrDefault(x => x.IdCollector == id);
                model.Name = collector.Name;
                model.IdCollector = collector.IdCollector;
                model.FkIdObjectType = collector.FkIdObjectType;
                model.FkIdAppointment = collector.FkIdAppointment;
                model.ExploitationDate = collector.ExploitationDate;
                model.FkIdReceivingSource = collector.FkIdReceivingSource;
                model.FkReceivingSourceType = collector.FkReceivingSourceType;
                model.Description = collector.Description;
                //model.SumOfCapacity = collector.SumOfCapacity;
                model.MinCapacity = collector.MinCapacity;
                model.MaxCapacity = collector.MaxCapacity;
                model.Objectid = collector.Objectid;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
                model.Obj03CollectorAdminUnit = _db.Asqueryable<Obj03CollectorAdminUnit>().Where(x => x.FkIdCollector == id).OrderBy(x => x.OrderNo).ToList();
            }
            model.FromSave = fromSave;

            return View(model);
        }

        public IActionResult CreateEditCollectorAdminUnit(int? id, int collectorId = 0, bool mode = false)
        {
            Obj03CollectorAdminUnitViewModel model = new Obj03CollectorAdminUnitViewModel();

            if (collectorId > 0)
            {
                Obj03Collector collector = _db.Asqueryable<Obj03Collector>().First(x => x.IdCollector == collectorId);
                model.Name = collector.Name;
            }

            if (id != null)
            {
                Obj03CollectorAdminUnit collectorAdminUnit = _db.Asqueryable<Obj03CollectorAdminUnit>().First(x => x.IdCollectorAdminUnit == id && (collectorId == 0 || x.FkIdCollector == collectorId));
                model.FkIdCollector = collectorAdminUnit.FkIdCollector;
                model.FkIdAdminUnit = collectorAdminUnit.FkIdAdminUnit;
                model.IdCollectorAdminUnit = collectorAdminUnit.IdCollectorAdminUnit;
                model.FkIdOwnershipType = collectorAdminUnit.FkIdOwnershipType;
                model.Objectid = collectorAdminUnit.Objectid;
                model.FkIdOwner = collectorAdminUnit.FkIdOwner;
                model.DeviceCount = collectorAdminUnit.DeviceCount;
                model.ProtectionZoneArea = collectorAdminUnit.ProtectionZoneArea;
                model.Capacity = collectorAdminUnit.Capacity;
                model.OrderNo = collectorAdminUnit.OrderNo;
                model.ActualLength = collectorAdminUnit.ActualLength;
                model.FkIdActivityStatus = collectorAdminUnit.FkIdActivityStatus;
                model.DrainedArea = collectorAdminUnit.DrainedArea;
                model.FkIdTechnicalStatus = collectorAdminUnit.FkIdTechnicalStatus;
                model.Length = collectorAdminUnit.Length;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
                model.WUAIdList = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => x.FkIdObjectAdminUnit == collectorAdminUnit.IdCollectorAdminUnit).Select(x => x.FkIdWaterUseAssociation).ToList();
            }
            else
            {
                model.FkIdCollector = collectorId;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Save(Obj03CollectorViewModel model)
        {
            Obj03Collector collector = _db.Asqueryable<Obj03Collector>().FirstOrDefault(x => x.IdCollector == model.IdCollector) ?? new Obj03Collector();
            int objectId = _db.AsqueryableWithoutIsDeleted<Obj03Collector>().Max(x => x.Objectid);
            int idCollector = 0;
            collector.Name = model.Name;
            collector.IdCollector = model.IdCollector;
            collector.FkIdObjectType = model.FkIdObjectType;
            collector.FkIdAppointment = model.FkIdAppointment;
            collector.ExploitationDate = model.ExploitationDate;
            collector.FkIdReceivingSource = model.FkIdReceivingSource;
            collector.FkReceivingSourceType = model.FkReceivingSourceType;
            collector.Description = model.Description;
            collector.MinCapacity = model.MinCapacity;
            collector.MaxCapacity = model.MaxCapacity;
            collector.Objectid = model.Objectid;
            collector.IsDeleted = 0;

            if (collector.IdCollector > 0)
            {
                _db.Update(collector);
            }
            else
            {
                collector.IdCollector = 0;
                collector.GlobalId = Guid.NewGuid();
                collector.Objectid = (objectId + 1);
                _db.Insert(collector);
            }

            _db.SaveChanges();
            idCollector = _db.AsqueryableWithoutIsDeleted<Obj03Collector>().Max(x => x.IdCollector) ?? 0;

            return RedirectToActionPermanent("CreateEdit", "Collector", new { id = collector.IdCollector > 0 ? collector.IdCollector : idCollector, fromSave = 1 });
        }

        [HttpPost]
        public IActionResult SaveCollectorAdminUnit(Obj03CollectorAdminUnitViewModel model)
        {
            Obj03Collector collector = _db.Asqueryable<Obj03Collector>().First(x => x.IdCollector == model.FkIdCollector);
            Obj03CollectorAdminUnit s = _db.Asqueryable<Obj03CollectorAdminUnit>().FirstOrDefault(x => x.IdCollectorAdminUnit == model.IdCollectorAdminUnit && x.Objectid == model.Objectid) ?? new Obj03CollectorAdminUnit();

            s.FkIdCollector = model.FkIdCollector;
            s.FkIdAdminUnit = model.FkIdAdminUnit;
            s.IdCollectorAdminUnit = model.IdCollectorAdminUnit;
            s.FkIdOwnershipType = model.FkIdOwnershipType;
            s.Objectid = model.Objectid ?? 0;
            s.FkIdOwner = model.FkIdOwner;
            s.DeviceCount = model.DeviceCount;
            s.ProtectionZoneArea = model.ProtectionZoneArea;
            s.OrderNo = model.OrderNo ?? 0;
            s.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
            s.ActualLength = model.ActualLength;
            s.FkIdActivityStatus = model.FkIdActivityStatus;
            s.Capacity = model.Capacity;
            s.DrainedArea = model.DrainedArea;
            s.Name = collector.Name;
            s.FkIdObjectType = collector.FkIdObjectType;

            List<DObjectWaterUseAssociation> listWUAdb = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => x.FkIdObjectAdminUnit == s.IdCollectorAdminUnit).ToList();
            _db.DeleteRange(listWUAdb);
            _db.SaveChanges();

            if (model.WUAIdList != null)
            {
                List<SWaterUseAssociation> listWUAclient = _db.Asqueryable<SWaterUseAssociation>().Where(x => model.WUAIdList.Contains(x.IdWaterUseAssociation)).ToList();

                int objectId = _db.AsqueryableWithoutIsDeleted<DObjectWaterUseAssociation>().Max(x => x.Objectid);

                foreach (var swua in listWUAclient)
                {
                    objectId++;
                    DObjectWaterUseAssociation wua = new DObjectWaterUseAssociation();
                    wua.FkIdWaterUseAssociation = swua.IdWaterUseAssociation;
                    wua.Objectid = objectId;
                    wua.GlobalId = Guid.NewGuid();
                    wua.FkIdObjectTypeGroup = 1;
                    wua.IdObjectWua = 0;
                    wua.FkIdWaterUseAssociation = swua.IdWaterUseAssociation;
                    wua.FkIdAdminUnit = swua.FkIdAdminUnit;
                    wua.FkIdObjectAdminUnit = s.IdCollectorAdminUnit;

                    _db.Insert(wua);
                    _db.SaveChanges();
                }
            }

            if (s.IdCollectorAdminUnit > 0)
            {
                _db.Update(s);
            }


            _db.SaveChanges();

            return RedirectToAction("CreateEdit", new { id = model.FkIdCollector });
        }


        public Task<LoadResult> LoadCollectorAdminUnit(DataSourceLoadOptions loadOptions, int collectorId)
        {
            var caiuIds = _db.Asqueryable<Obj03CollectorAdminUnit>().Where(x => x.FkIdCollector == collectorId).Select(x => x.IdCollectorAdminUnit).ToList();
            var sibs = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => caiuIds.Contains((int)x.FkIdObjectAdminUnit)).Select(x => x.SWaterUseAssociation).Select(x => x.Name).Distinct().ToList();

            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj03CollectorAdminUnit>()
                .Where(x => x.FkIdCollector == collectorId && x.IsDeleted == 0).Select(x => new
                {
                    x.IdCollectorAdminUnit,
                    x.FkIdTechnicalStatus,
                    x.FkIdActivityStatus,
                    x.FkIdAdminUnit,
                    x.FkIdOwner,
                    x.FkIdOwnershipType,
                    x.FkIdCollector,
                    x.FkIdObjectType,
                    x.Name,
                    x.ProtectionZoneArea,
                    x.ActualLength,
                    x.DrainedArea,
                    x.Length,
                    x.LengthA,
                    x.LengthB,
                    x.Capacity,
                    x.DeviceCount,
                    x.Objectid,
                    x.OrderNo,
                    ServiceSib = string.Join(", ", sibs)
                }).OrderBy(x => x.OrderNo), loadOptions);
        }

        [HttpPost]
        public List<int> Filter(CollectorFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            return _db.Asqueryable<VwObj03CollectorAndDetails>()
                .Where(x =>
                userRegionIds.Contains((int)x.fk_id_admin_unit) &&
                    (x.name.Contains(data.name) || data.name == null) &&

                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.fk_id_admin_unit)) &&
                    (data.service_admin_unit == null || data.service_admin_unit.Contains(x.service_admin_unit)) &&
                    (data.fk_id_source_type == null || data.fk_id_source_type.Contains(x.fk_id_source_type)) &&
                    (data.fk_id_source == null || data.fk_id_source.Contains(x.fk_id_source)) &&
                    (data.fk_receiving_source_type == null || data.fk_receiving_source_type.Contains(x.fk_receiving_source_type)) &&
                    (data.fk_id_receiving_source == null || data.fk_id_receiving_source.Contains(x.fk_id_receiving_source)) &&
                    (data.fk_id_missing_source_type == null || data.fk_id_missing_source_type.Contains(x.fk_id_missing_source_type)) &&
                    (data.fk_id_activity_status == null || data.fk_id_activity_status.Contains(x.fk_id_activity_status)) &&
                    (data.fk_id_technical_status == null || data.fk_id_technical_status.Contains(x.fk_id_technical_status)) &&
                    (data.fk_id_appointment == null || data.fk_id_appointment.Contains(x.fk_id_appointment)) &&
                    (data.fk_id_ownership_type == null || data.fk_id_ownership_type.Contains(x.fk_id_ownership_type)) &&
                    (data.fk_id_owner == null || data.fk_id_owner.Contains(x.fk_id_owner)) &&
                    (data.fk_id_object_type == null || data.fk_id_object_type.Contains(x.fk_id_object_type)) &&
                    (data.fk_id_object_type_au == null || data.fk_id_object_type_au.Contains(x.fk_id_object_type_au)) &&
                    (data.fk_id_water_use_association == null || data.fk_id_water_use_association.Contains(x.fk_id_water_use_association)) &&

                    (x.exploitation_date >= data.exploitation_date_min || data.exploitation_date_min == null) &&
                    (x.exploitation_date <= data.exploitation_date_max || data.exploitation_date_max == null) &&

                    (x.exploitation_date_au >= data.exploitation_date_au_min || data.exploitation_date_au_min == null) &&
                    (x.exploitation_date_au <= data.exploitation_date_au_max || data.exploitation_date_au_max == null) &&

                    (x.device_count >= data.device_count_min || data.device_count_min == null) &&
                    (x.device_count <= data.device_count_max || data.device_count_max == null) &&

                    (x.protection_zone_area >= data.protection_zone_area_min || data.protection_zone_area_min == null) &&
                    (x.protection_zone_area <= data.protection_zone_area_max || data.protection_zone_area_max == null) &&

                    (x.capacity >= data.capacity_min || data.capacity_min == null) &&
                    (x.capacity <= data.capacity_max || data.capacity_max == null) &&

                    (x.min_capacity >= data.min_capacity_min || data.min_capacity_min == null) &&
                    (x.min_capacity <= data.min_capacity_max || data.min_capacity_max == null) &&

                    (x.max_capacity >= data.max_capacity_min || data.max_capacity_min == null) &&
                    (x.max_capacity <= data.max_capacity_max || data.max_capacity_max == null) &&

                    (x.length >= data.length_min || data.length_min == null) &&
                    (x.length <= data.length_max || data.length_max == null) &&

                    (x.length_a >= data.length_a_min || data.length_a_min == null) &&
                    (x.length_a <= data.length_a_max || data.length_a_max == null) &&

                    (x.actual_length >= data.actual_length_min || data.actual_length_min == null) &&
                    (x.actual_length <= data.actual_length_max || data.actual_length_max == null) &&

                    (x.length_b >= data.length_b_min || data.length_b_min == null) &&
                    (x.length_b <= data.length_b_max || data.length_b_max == null) &&

                    (x.drained_area >= data.drained_area_min || data.drained_area_min == null) &&
                    (x.drained_area <= data.drained_area_max || data.drained_area_max == null) &&

                    (x.water_consumption >= data.water_consumption_min || data.water_consumption_min == null) &&
                    (x.water_consumption <= data.water_consumption_max || data.water_consumption_max == null)

                )
                .Select(x => x.OBJECTID).Distinct().ToList();
        }


        public List<int> GetObjectIds(int collectorId, bool isChild)
        {
            if (isChild)
                return _db.Asqueryable<Obj03CollectorAdminUnit>(x => x.IdCollectorAdminUnit == collectorId).Select(x => x.Objectid).ToList();
            else
                return _db.Asqueryable<Obj03CollectorAdminUnit>(x => x.FkIdCollector == collectorId).Select(x => x.Objectid).ToList();
        }

        [HttpPost]
        public Task<LoadResult> LoadFilter(DataSourceLoadOptions loadOptions, string objectIds)
        {
            List<int> ids = new List<int>();
            if (!string.IsNullOrEmpty(objectIds))
            {
                ids = objectIds.Split(",").Select(int.Parse).ToList();
            }
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            //var adminUnitIds = _db.Asqueryable<Obj03CollectorAdminUnit>().Where(x => userRegionIds.Contains((int)x.FkIdAdminUnit) && (ids.Count == 0 || ids.Contains(x.Objectid))).Select(x => x.FkIdCollector).Distinct().ToList();
            //
            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj03CollectorView>().Where(x => userRegionIds.Contains((int)x.FkIdAdminUnit) && (ids.Count == 0 || ids.Contains((int)x.AUObjectId))).Select(x => new
            {
                x.IdCollector,
                x.ExploitationDate,
                x.Controlled,
                x.Description,
                x.FkIdObjectType,
                x.Name,
                x.ServiceAdminUnit,
                x.FkIdAppointment,
                x.FkReceivingSourceType,
                x.FkIdReceivingSource,
                x.Objectid,
                //x.SumOfCapacity  ,
                //SumOfCapacity = _db.Asqueryable<Obj03CollectorAdminUnit>(z => z.FkIdCollector == x.IdCollector && z.IsDeleted                 == 0).Sum(z => z.Capacity),
                x.MinCapacity,
                x.MaxCapacity
            }).Distinct(), loadOptions);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int key)
        {
            bool result = true;
            var obj = _db.Asqueryable<Obj03Collector>().First(x => x.IdCollector == key);
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
            string source = null;
            int collectorId = _db.Asqueryable<Obj03CollectorAdminUnit>().First(x => x.IdCollectorAdminUnit == id).FkIdCollector ?? 0;

            if (collectorId > 0)
            {
                int sourceId = _db.Asqueryable<Obj03Collector>().First(x => x.IdCollector == collectorId).FkIdReceivingSource ?? 0;

                if (sourceId > 0)
                {
                    source = _db.Asqueryable<ObjViewSource>().First(x => x.Id == sourceId).Name;
                }
            }
            return _db.Asqueryable<Obj03CollectorAdminUnit>().Where(x => x.IdCollectorAdminUnit == id).ToList().Select(x => new
            {
                x.FkIdCollector,
                Name = x.Name ?? "məlumat yoxdur",
                Length = x.Length > 0 ? decimal.Round(x.Length ?? 0, 2).ToString() : "məlumat yoxdur",
                Capacity = x.Capacity > 0 ? decimal.Round(x.Capacity ?? 0, 2).ToString() : "məlumat yoxdur",
                ExploitationDate = _db.Asqueryable<Obj03Collector>().FirstOrDefault(f => f.IdCollector == x.FkIdCollector)?.ExploitationDate > 0 ? _db.Asqueryable<Obj03Collector>().FirstOrDefault(f => f.IdCollector == x.FkIdCollector)?.ExploitationDate.ToString() : "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int?)x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Type = _db.Asqueryable<SObjectType>().FirstOrDefault(z => z.IdObjectType == (int?)x.FkIdObjectType)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == (int?)x.FkIdOwner)?.Name ?? "məlumat yoxdur",
                Source = source ?? "məlumat yoxdur"
            }).FirstOrDefault();
        }
    }
}
