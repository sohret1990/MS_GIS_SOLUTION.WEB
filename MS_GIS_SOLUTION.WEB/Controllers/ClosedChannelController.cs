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
using System.Linq.Dynamic.Core;
using MS_GIS_SOLUTION.WEB.Models.FilterVM;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ClosedChannelController : BaseController
    {
        public ClosedChannelController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Qapalı suvarma şəbəkələri";
        }



        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj02ClosedChannel>().Where(x => x.IdClosedChannel != null).OrderByDescending(x => x.Objectid).Select(x => new
            {
                x.IdClosedChannel,
                x.ExploitationDate,
                x.Controlled,
                x.Description,
                x.FkIdObjectType,
                x.Name,
                x.FkIdAppointment,
                x.FkIdSource,
                x.FkIdSourceType,
                x.Objectid

            }), loadOptions);
        }

        public IActionResult CreateEdit(int? id, int fromSave = 0, bool mode = false)
        {
            Obj02ClosedChannelViewModel model = new Obj02ClosedChannelViewModel();

            if (id != null)
            {
                Obj02ClosedChannel channel = _db.Asqueryable<Obj02ClosedChannel>().FirstOrDefault(x => x.IdClosedChannel == id);
                model.Name = channel.Name;
                model.IdClosedChannel = channel.IdClosedChannel;
                model.FkIdObjectType = channel.FkIdObjectType;
                model.FkIdAppointment = channel.FkIdAppointment;
                model.FkIdSourceType = channel.FkIdSourceType;
                model.FkIdSource = channel.FkIdSource;
                model.ExploitationDate = channel.ExploitationDate;
                model.Description = channel.Description;
                model.Objectid = channel.Objectid;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
                model.Obj02ClosedChannelAdminUnit = _db.Asqueryable<Obj02ClosedChannelAdminUnit>().Where(x => x.FkIdClosedChannel == id).OrderBy(x => x.OrderNo).ToList();
            }

            model.FromSave = fromSave;

            return View(model);
        }

        public IActionResult CreateEditClosedChannelAdminUnit(int? id, int closedchannelId = 0, bool mode = false)
        {
            Obj02ClosedChannelAdminUnitViewModel model = new Obj02ClosedChannelAdminUnitViewModel();

            if (closedchannelId > 0)
            {
                Obj02ClosedChannel channel = _db.Asqueryable<Obj02ClosedChannel>().First(x => x.IdClosedChannel == closedchannelId);
                model.Name = channel.Name;
            }

            if (id != null)
            {
                Obj02ClosedChannelAdminUnit channelsAdminUnit = _db.Asqueryable<Obj02ClosedChannelAdminUnit>().First(x => x.IdClosedChannelAdminUnit == id && (closedchannelId == 0 || x.FkIdClosedChannel == closedchannelId));
                model.FkIdClosedChannel = channelsAdminUnit.FkIdClosedChannel;
                model.FkIdAdminUnit = channelsAdminUnit.FkIdAdminUnit;
                model.OrderNo = channelsAdminUnit.OrderNo;
                model.IdClosedChannelAdminUnit = channelsAdminUnit.IdClosedChannelAdminUnit;
                model.FkIdOwnershipType = channelsAdminUnit.FkIdOwnershipType;
                model.FkIdOwner = channelsAdminUnit.FkIdOwner;
                model.Objectid = channelsAdminUnit.Objectid;
                model.DeviceCount = channelsAdminUnit.DeviceCount;
                model.ServiceArea = channelsAdminUnit.ServiceArea;
                model.FkIdActivityStatus = channelsAdminUnit.FkIdActivityStatus;
                model.AdditionalAdminUnit = channelsAdminUnit.AdditionalAdminUnit;
                model.FkIdTechnicalStatus = channelsAdminUnit.FkIdTechnicalStatus;
                model.Length = channelsAdminUnit.Length;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
                model.WUAIdList = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => x.FkIdObjectAdminUnit == channelsAdminUnit.IdClosedChannelAdminUnit).Select(x => x.FkIdWaterUseAssociation).ToList();
            }
            else
            {
                model.FkIdClosedChannel = closedchannelId;
            }

            return View(model);
        }

        [HttpPost]

        public IActionResult SaveClosedChannelAdminUnit(Obj02ClosedChannelAdminUnitViewModel model)
        {
            Obj02ClosedChannel closedChannel = _db.Asqueryable<Obj02ClosedChannel>().First(x => x.IdClosedChannel == model.FkIdClosedChannel);
            Obj02ClosedChannelAdminUnit s = _db.Asqueryable<Obj02ClosedChannelAdminUnit>().FirstOrDefault(x => x.IdClosedChannelAdminUnit == model.IdClosedChannelAdminUnit && x.Objectid == model.Objectid) ?? new Obj02ClosedChannelAdminUnit();

            s.FkIdAdminUnit = model.FkIdAdminUnit;
            s.FkIdOwnershipType = model.FkIdOwnershipType;
            s.FkIdOwner = model.FkIdOwner;
            s.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
            s.FkIdClosedChannel = model.FkIdClosedChannel;
            s.ServiceArea = model.ServiceArea;
            s.Objectid = model.Objectid ?? 0;
            s.Name = closedChannel.Name;
            s.OrderNo = model.OrderNo ?? 0;
            s.FkIdObjectType = closedChannel.FkIdObjectType;
            s.FkIdActivityStatus = model.FkIdActivityStatus;
            s.AdditionalAdminUnit = model.AdditionalAdminUnit;

            List<DObjectWaterUseAssociation> listWUAdb = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => x.FkIdObjectAdminUnit == s.IdClosedChannelAdminUnit).ToList();
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
                    wua.IsDeleted = 0;
                    wua.IdObjectWua = 0;
                    wua.FkIdWaterUseAssociation = swua.IdWaterUseAssociation;
                    wua.FkIdAdminUnit = swua.FkIdAdminUnit;
                    wua.FkIdObjectAdminUnit = s.IdClosedChannelAdminUnit;

                    _db.Insert(wua);
                    _db.SaveChanges();
                }
            }



            if (s.IdClosedChannelAdminUnit > 0)
            {
                _db.Update(s);
            }


            _db.SaveChanges();


            return RedirectToAction("CreateEdit", new { id = model.FkIdClosedChannel });
        }


        [HttpPost]
        public IActionResult Save(Obj02ClosedChannelViewModel model)
        {
            Obj02ClosedChannel closedChannel = _db.Asqueryable<Obj02ClosedChannel>().FirstOrDefault(x => x.IdClosedChannel == model.IdClosedChannel) ?? new Obj02ClosedChannel();
            int objectId = _db.AsqueryableWithoutIsDeleted<Obj02ClosedChannel>().Max(x => x.Objectid);
            int idClosedChannel = 0;

            closedChannel.Name = model.Name;
            closedChannel.IdClosedChannel = model.IdClosedChannel;
            closedChannel.FkIdObjectType = model.FkIdObjectType;
            closedChannel.FkIdAppointment = model.FkIdAppointment;
            closedChannel.ExploitationDate = model.ExploitationDate;
            closedChannel.FkIdSource = model.FkIdSource;
            closedChannel.FkIdSourceType = model.FkIdSourceType;
            closedChannel.Description = model.Description;
            closedChannel.Objectid = model.Objectid;
            closedChannel.IsDeleted = 0;

            if (closedChannel.IdClosedChannel > 0)
            {
                _db.Update(closedChannel);
            }
            else
            {
                closedChannel.IdClosedChannel = 0;
                closedChannel.GlobalId = Guid.NewGuid();
                closedChannel.Objectid = (objectId + 1);
                _db.Insert(closedChannel);
            }

            _db.SaveChanges();

            idClosedChannel = _db.AsqueryableWithoutIsDeleted<Obj02ClosedChannel>().Max(x => x.IdClosedChannel) ?? 0;

            return RedirectToActionPermanent("CreateEdit", "ClosedChannel", new { id = closedChannel.IdClosedChannel > 0 ? closedChannel.IdClosedChannel : idClosedChannel, fromSave = 1 });
        }

        public Task<LoadResult> LoadClosedChannelAdminUnit(DataSourceLoadOptions loadOptions, int channelId)
        {
            try
            {
                var caiuIds = _db.Asqueryable<Obj02ClosedChannelAdminUnit>().Where(x => x.FkIdClosedChannel == channelId).Select(x => x.IdClosedChannelAdminUnit).ToList();
                var sibs = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => caiuIds.Contains((int)x.FkIdObjectAdminUnit)).Select(x => x.SWaterUseAssociation).Select(x => x.Name).Distinct().ToList();

                return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj02ClosedChannelAdminUnit>()
                    .Where(x => x.FkIdClosedChannel == channelId && x.IsDeleted == 0).Select(x => new
                    {
                        x.IdClosedChannelAdminUnit,
                        x.FkIdTechnicalStatus,
                        x.FkIdActivityStatus,
                        x.FkIdAdminUnit,
                        x.FkIdOwner,
                        x.FkIdOwnershipType,
                        x.FkIdClosedChannel,
                        x.Name,
                        x.ProtectionZoneArea,
                        x.ServiceArea,
                        x.WaterMetersCount,
                        x.AdditionalAdminUnit,
                        x.Capacity,
                        x.DeviceCount,
                        x.FkIdObjectType,
                        x.Length,
                        x.Objectid,
                        x.OrderNo,
                        ServiceSib = string.Join(", ", sibs)
                    }).OrderBy(x => x.OrderNo), loadOptions);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public List<int?> Filter(ClosedChannelFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            return _db.Asqueryable<VwObj02ClosedChannelAndDetails>()
                .Where(x =>
                userRegionIds.Contains((int)x.fk_id_admin_unit) &&
                    (x.name.Contains(data.name) || data.name == null) &&

                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.fk_id_admin_unit)) &&
                    (data.fk_id_source_type == null || data.fk_id_source_type.Contains(x.fk_id_source_type)) &&
                    (data.fk_id_source == null || data.fk_id_source.Contains(x.fk_id_source)) &&
                    (data.fk_id_activity_status == null || data.fk_id_activity_status.Contains(x.fk_id_activity_status)) &&
                    (data.fk_id_technical_status == null || data.fk_id_technical_status.Contains(x.fk_id_technical_status)) &&
                    (data.fk_id_appointment == null || data.fk_id_appointment.Contains(x.fk_id_appointment)) &&
                    (data.fk_id_ownership_type == null || data.fk_id_ownership_type.Contains(x.fk_id_ownership_type)) &&
                    (data.fk_id_owner == null || data.fk_id_owner.Contains(x.fk_id_owner)) &&
                    (data.fk_id_object_type == null || data.fk_id_object_type.Contains(x.fk_id_object_type)) &&
                    (data.fk_id_object_type_pipe == null || data.fk_id_object_type_pipe.Contains(x.fk_id_object_type_pipe)) &&
                    (data.fk_id_object_type_admin_unit == null || data.fk_id_object_type_admin_unit.Contains(x.fk_id_object_type_admin_unit)) &&
                    (data.fk_id_pipe_type == null || data.fk_id_pipe_type.Contains(x.fk_id_pipe_type)) &&

                    (x.exploitation_date >= data.exploitation_date_min || data.exploitation_date_min == null) &&
                    (x.exploitation_date <= data.exploitation_date_max || data.exploitation_date_max == null) &&

                    (x.device_count >= data.device_count_min || data.device_count_min == null) &&
                    (x.device_count <= data.device_count_max || data.device_count_max == null) &&

                    (x.protection_zone_area >= data.protection_zone_area_min || data.protection_zone_area_min == null) &&
                    (x.protection_zone_area <= data.protection_zone_area_max || data.protection_zone_area_max == null) &&

                    (x.capacity >= data.capacity_min || data.capacity_min == null) &&
                    (x.capacity <= data.capacity_max || data.capacity_max == null) &&

                    (x.service_area >= data.service_area_min || data.service_area_min == null) &&
                    (x.service_area <= data.service_area_max || data.service_area_max == null) &&

                    (x.length >= data.length_min || data.length_min == null) &&
                    (x.length <= data.length_max || data.length_max == null) &&

                    (x.exploitation_date_pipe >= data.exploitation_date_pipe_min || data.exploitation_date_pipe_min == null) &&
                    (x.exploitation_date_pipe <= data.exploitation_date_pipe_max || data.exploitation_date_pipe_max == null) &&

                    (x.water_meters_count >= data.water_meters_count_min || data.water_meters_count_min == null) &&
                    (x.water_meters_count <= data.water_meters_count_max || data.water_meters_count_max == null) &&

                    (x.pipe_length >= data.pipe_length_min || data.pipe_length_min == null) &&
                    (x.pipe_length <= data.pipe_length_max || data.pipe_length_max == null) &&

                    (x.additional_admin_unit.Contains(data.additional_admin_unit) || data.additional_admin_unit == null)
                )
                .Select(x => x.OBJECTID).Distinct().ToList();
        }


        public List<int> GetObjectIds(int channelId, bool isChild)
        {
            if (isChild)
                return _db.Asqueryable<Obj02ClosedChannelAdminUnit>(x => x.IdClosedChannelAdminUnit == channelId).Select(x => x.Objectid).ToList();
            else
                return _db.Asqueryable<Obj02ClosedChannelAdminUnit>(x => x.FkIdClosedChannel == channelId).Select(x => x.Objectid).ToList();

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
            var adminUnitIds = _db.Asqueryable<Obj02ClosedChannelAdminUnit>().Where(x => userRegionIds.Contains((int)x.FkIdAdminUnit) && (ids.Contains(x.Objectid))).Select(x => x.FkIdClosedChannel).ToList();

            return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj02ClosedChannel>().Where(x => x.IsDeleted == 0 && (adminUnitIds.Contains(x.IdClosedChannel))).Select(x => new
            {
                x.IdClosedChannel,
                x.ExploitationDate,
                x.Controlled,
                x.Description,
                x.FkIdObjectType,
                x.Name,
                x.FkIdAppointment,
                x.FkIdSource,
                x.FkIdSourceType,
                x.Objectid
            }), loadOptions);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int key)
        {
            bool result = true;
            var obj = _db.Asqueryable<Obj02ClosedChannel>().First(x => x.IdClosedChannel == key);
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
            int closedChannelId = _db.Asqueryable<Obj02ClosedChannelAdminUnit>().First(x => x.IdClosedChannelAdminUnit == id).FkIdClosedChannel ?? 0;
            if (closedChannelId > 0)
            {
                int sourceId = _db.Asqueryable<Obj02ClosedChannel>().First(x => x.IdClosedChannel == closedChannelId).FkIdSource ?? 0;
                if (sourceId > 0)
                {
                    source = _db.Asqueryable<ObjViewSource>().First(x => x.Id == sourceId).Name;
                }
            }
            decimal allClosedChannelLength = _db.Asqueryable<Obj02ClosedChannelAdminUnit>().Where(x => x.FkIdClosedChannel == closedChannelId).Sum(x => x.Length ?? 0);
            decimal adminUnitActualLength = _db.Asqueryable<Obj02ClosedChannelAdminUnitPipeInfo>().Where(x => x.FkIdClosedChannelAdminUnit == id).First().Lenght ?? 0;
            List<int> allClosedChannelAdminUnits = _db.Asqueryable<Obj02ClosedChannelAdminUnit>().Where(x => x.FkIdClosedChannel == closedChannelId).Select(x => x.IdClosedChannelAdminUnit).ToList();
            decimal allClosedChannelActualLength = _db.Asqueryable<Obj02ClosedChannelAdminUnitPipeInfo>().Where(x => allClosedChannelAdminUnits.Contains(x.FkIdClosedChannelAdminUnit ?? 0)).Sum(x => x.Lenght ?? 0);


            return _db.Asqueryable<Obj02ClosedChannelAdminUnit>().Where(x => x.IdClosedChannelAdminUnit == id).ToList().Select(x => new
            {
                x.FkIdClosedChannel,
                Name = x.Name ?? "məlumat yoxdur",
                ProtectionZoneArea = x.ProtectionZoneArea > 0 ? decimal.Round(x.ProtectionZoneArea ?? 0, 2).ToString() : "məlumat yoxdur",
                ServiceArea = x.ServiceArea > 0 ? decimal.Round(x.ServiceArea ?? 0, 2).ToString() : "məlumat yoxdur",
                WaterMetersCount = x.WaterMetersCount > 0 ? decimal.Round(x.WaterMetersCount ?? 0, 2).ToString() : "məlumat yoxdur",
                Length = x.Length > 0 ? decimal.Round(x.Length ?? 0, 2).ToString() : "məlumat yoxdur",
                Capacity = x.Capacity > 0 ? decimal.Round(x.Capacity ?? 0, 2).ToString() : "məlumat yoxdur",
                ExploitationDate = _db.Asqueryable<Obj02ClosedChannel>().FirstOrDefault(f => f.IdClosedChannel == x.FkIdClosedChannel)?.ExploitationDate > 0 ? _db.Asqueryable<Obj02ClosedChannel>().FirstOrDefault(f => f.IdClosedChannel == x.FkIdClosedChannel)?.ExploitationDate.ToString() : "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int?)x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Type = _db.Asqueryable<SObjectType>().FirstOrDefault(z => z.IdObjectType == (int?)x.FkIdObjectType)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == (int?)x.FkIdOwner)?.Name ?? "məlumat yoxdur",
                Source = source ?? "məlumat yoxdur",
                AllLength = allClosedChannelLength,
                ActualLength = adminUnitActualLength,
                AllActualLength = allClosedChannelActualLength
            }).FirstOrDefault();
        }
    }
}
