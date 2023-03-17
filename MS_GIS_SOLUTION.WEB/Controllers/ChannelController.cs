using System.Collections.Generic;
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
using MS_GIS_SOLUTION.WEB.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ChannelController : BaseController
    {
        public ChannelController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Açıq suvarma şəbəkələri";
        }



        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<Obj01ChannelView>().Where(x => x.IdChannel != null).OrderByDescending(x => x.Objectid).Select(x => new
            {
                x.IdChannel,
                x.ExploitationDate,
                x.Controlled,
                x.Description,
                x.FkIdObjectType,
                x.Name,
                x.FkIdAppointment,
                x.FkIdSource,
                x.FkIdSourceType,
                x.SumOfCapacity,
                x.Objectid

            }), loadOptions);
        }

        public IActionResult CreateEdit(int? id, int fromSave = 0, bool mode = false)
        {
            Obj01ChannelViewModel model = new Obj01ChannelViewModel();

            if (id != null)
            {
                Obj01Channel channel = _db.Asqueryable<Obj01Channel>().FirstOrDefault(x => x.IdChannel == id);
                model.Name = channel.Name;
                model.IdChannel = channel.IdChannel;
                model.FkIdObjectType = channel.FkIdObjectType;
                model.FkIdAppointment = channel.FkIdAppointment;
                model.FkIdSourceType = channel.FkIdSourceType;
                model.FkIdSource = channel.FkIdSource;
                model.ExploitationDate = channel.ExploitationDate;
                model.Description = channel.Description;
                model.Objectid = channel.Objectid;
                model.Mode = mode;
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };
                model.Obj01ChannelsAdminUnit = _db.Asqueryable<Obj01ChannelsAdminUnit>().Where(x => x.FkIdChannel == id).OrderBy(x => x.OrderNo).ToList();
            }

            model.FromSave = fromSave;

            return View(model);
        }

        public IActionResult CreateEditChannelAdminUnit(int? id, int channelId = 0, bool mode = false)
        {
            Obj01ChannelAdminUnitViewModel model = new Obj01ChannelAdminUnitViewModel();

            if (channelId > 0)
            {
                Obj01Channel channel = _db.Asqueryable<Obj01Channel>().First(x => x.IdChannel == channelId);
                model.Name = channel.Name;
            }

            if (id != null)
            {
                Obj01ChannelsAdminUnit channelsAdminUnit = _db.Asqueryable<Obj01ChannelsAdminUnit>().FirstOrDefault(x => x.IdChannelAdminUnit == id && (channelId == 0 || x.FkIdChannel == channelId));
                model.FkIdChannel = channelsAdminUnit.FkIdChannel;
                model.FkIdAdminUnit = channelsAdminUnit.FkIdAdminUnit;
                model.IdChannelAdminUnit = channelsAdminUnit.IdChannelAdminUnit;
                model.FkIdOwnershipType = channelsAdminUnit.FkIdOwnershipType;
                model.FkIdOwner = channelsAdminUnit.FkIdOwner;
                model.DeviceCount = channelsAdminUnit.DeviceCount;
                model.Objectid = channelsAdminUnit.Objectid;
                model.FkIdTechnicalStatus = channelsAdminUnit.FkIdTechnicalStatus;
                model.IdChannelAdminUnit = channelsAdminUnit.IdChannelAdminUnit;
                model.ProtectionZoneArea = channelsAdminUnit.ProtectionZoneArea;
                model.FkIdActivityStatus = channelsAdminUnit.FkIdActivityStatus;
                model.OrderNo = channelsAdminUnit.OrderNo;
                model.AdditionalAdminUnit = channelsAdminUnit.AdditionalAdminUnit;
                model.Capacity = channelsAdminUnit.Capacity;
                model.ServiceArea = channelsAdminUnit.ServiceArea;
                model.FkIdTechnicalStatus = channelsAdminUnit.FkIdTechnicalStatus;
                model.Length = channelsAdminUnit.Length;
                model.Mode = mode;
                model.WUAIdList = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => x.FkIdObjectAdminUnit == channelsAdminUnit.IdChannelAdminUnit).Select(x => x.FkIdWaterUseAssociation).ToList();
                model.ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true, ReadOnly = mode };

            }
            else
            {
                model.FkIdChannel = channelId;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult SaveChannelAdminUnit(Obj01ChannelAdminUnitViewModel model)
        {
            Obj01Channel channel = _db.Asqueryable<Obj01Channel>().First(x => x.IdChannel == model.FkIdChannel);
            Obj01ChannelsAdminUnit s = _db.Asqueryable<Obj01ChannelsAdminUnit>().FirstOrDefault(x => x.IdChannelAdminUnit == model.IdChannelAdminUnit && x.Objectid == model.Objectid) ?? new Obj01ChannelsAdminUnit();

            s.FkIdAdminUnit = model.FkIdAdminUnit;
            s.FkIdOwnershipType = model.FkIdOwnershipType;
            s.FkIdOwner = model.FkIdOwner;
            s.FkIdTechnicalStatus = model.FkIdTechnicalStatus;
            s.FkIdActivityStatus = model.FkIdActivityStatus;
            s.AdditionalAdminUnit = model.AdditionalAdminUnit;
            s.Capacity = model.Capacity;
            s.OrderNo = model.OrderNo ?? 0;
            s.FkIdChannel = model.FkIdChannel;
            s.ProtectionZoneArea = model.ProtectionZoneArea;
            s.Objectid = model.Objectid ?? 0;
            s.DeviceCount = model.DeviceCount;
            s.ServiceArea = model.ServiceArea;
            s.Name = channel.Name;
            s.FkIdObjectType = channel.FkIdObjectType;

            List<DObjectWaterUseAssociation> listWUAdb = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => x.FkIdObjectAdminUnit == s.IdChannelAdminUnit).ToList();
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
                    wua.FkIdObjectAdminUnit = s.IdChannelAdminUnit;

                    _db.Insert(wua);
                    _db.SaveChanges();
                }
            }

            if (s.IdChannelAdminUnit > 0)
            {
                _db.Update(s);
            }

            _db.SaveChanges();

            return RedirectToAction("CreateEdit", new { id = model.FkIdChannel });
        }

        [HttpPost]
        public IActionResult Save(Obj01ChannelViewModel model)
        {
            Obj01Channel channel = _db.Asqueryable<Obj01Channel>().FirstOrDefault(x => x.IdChannel == model.IdChannel) ?? new Obj01Channel();
            int objectId = _db.AsqueryableWithoutIsDeleted<Obj01Channel>().Max(x => x.Objectid);
            int idChannel = 0;

            channel.Name = model.Name;
            channel.IdChannel = model.IdChannel;
            channel.FkIdObjectType = model.FkIdObjectType;
            channel.FkIdAppointment = model.FkIdAppointment;
            channel.ExploitationDate = model.ExploitationDate;
            channel.FkIdObjectType = model.FkIdObjectType;
            channel.FkIdSourceType = model.FkIdSourceType;
            channel.FkIdSource = model.FkIdSource;
            channel.Objectid = model.Objectid;
            channel.IsDeleted = 0;
            channel.Description = model.Description;

            if (channel.IdChannel > 0)
            {
                channel.Objectid = model.Objectid;
                _db.Update(channel);
            }
            else
            {
                channel.IdChannel = 0;
                channel.GlobalId = Guid.NewGuid();
                channel.Objectid = (objectId + 1);
                _db.Insert(channel);
            }

            _db.SaveChanges();

            idChannel = _db.AsqueryableWithoutIsDeleted<Obj01Channel>().Max(x => x.IdChannel) ?? 0;

            return RedirectToActionPermanent("CreateEdit", "Channel", new { id = channel.IdChannel > 0 ? channel.IdChannel : idChannel, fromSave = 1 });
        }

        public Task<LoadResult> LoadChannelAdminUnit(DataSourceLoadOptions loadOptions, int channelId)
        {
            var caiuIds = _db.Asqueryable<Obj01ChannelsAdminUnit>().Where(x => x.FkIdChannel == channelId).Select(x => x.IdChannelAdminUnit).ToList();
            var sibs = _db.Asqueryable<DObjectWaterUseAssociation>().Where(x => caiuIds.Contains((int)x.FkIdObjectAdminUnit)).Select(x => x.SWaterUseAssociation).Select(x => x.Name).Distinct().ToList();
            
            try
            {
                return DataSourceLoader.LoadAsync(_db.Asqueryable<Obj01ChannelsAdminUnit>()
                        .Where(x => x.FkIdChannel == channelId && x.IsDeleted == 0).Select(x => new
                        {
                            x.IdChannelAdminUnit,
                            x.FkIdTechnicalStatus,
                            x.FkIdActivityStatus,
                            x.FkIdAdminUnit,
                            x.FkIdOwner,
                            x.FkIdOwnershipType,
                            x.FkIdChannel,
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
                            ServiceSib = string.Join(", ", sibs),
                            //CoverIds = x.ChannelCover.Where(z => z.Type == 1 && x.IsDeleted == 0).ToList()
                            CoverIds = x.ChannelCover.Where(z => x.IsDeleted == 0).ToList()
                        }).OrderBy(x => x.OrderNo), loadOptions);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public List<int?> Filter(ChannelFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            return _db.Asqueryable<VwObj01ChannelsAndChannelsAdminUnit>()
                .Where(x =>
                userRegionIds.Contains((int)x.fk_id_admin_unit) &&
                    (data.name == null || x.name.Contains(data.name)) &&
                    (data.partial_channel_name == null || x.partial_channel_name.Contains(data.partial_channel_name)) &&

                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.fk_id_admin_unit)) &&
                    (data.fk_id_source_type == null || data.fk_id_source_type.Contains(x.fk_id_source_type)) &&
                    (data.fk_id_source == null || data.fk_id_source.Contains(x.fk_id_source)) &&
                    (data.fk_id_activity_status == null || data.fk_id_activity_status.Contains(x.fk_id_activity_status)) &&
                    (data.fk_id_technical_status == null || data.fk_id_technical_status.Contains(x.fk_id_technical_status)) &&
                    (data.fk_id_appointment == null || data.fk_id_appointment.Contains(x.fk_id_appointment)) &&
                    (data.fk_id_ownership_type == null || data.fk_id_ownership_type.Contains(x.fk_id_ownership_type)) &&
                    (data.fk_id_owner == null || data.fk_id_owner.Contains(x.fk_id_owner)) &&
                    (data.fk_id_object_type == null || data.fk_id_object_type.Contains(x.fk_id_object_type)) &&
                    (data.fk_id_object_admin_unit == null || data.fk_id_object_admin_unit.Contains(x.fk_id_object_type_admin_unit)) &&
                    (data.fk_id_water_use_association == null || data.fk_id_water_use_association.Contains(x.fk_id_water_use_association)) &&
                    (data.fk_cover_id_barrier_type == null || data.fk_cover_id_barrier_type.Contains(x.fk_cover_id_barrier_type)) &&


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

                    (x.exploitation_date_admin_unit >= data.exploitation_date_admin_unit_min || data.exploitation_date_admin_unit_min == null) &&
                    (x.exploitation_date_admin_unit <= data.exploitation_date_admin_unit_max || data.exploitation_date_admin_unit_max == null) &&

                    (x.water_meters_count >= data.water_meters_count_min || data.water_meters_count_min == null) &&
                    (x.water_meters_count <= data.water_meters_count_max || data.water_meters_count_max == null) &&


                    (x.cover_length >= data.cover_length_min || data.cover_length_min == null) &&
                    (x.cover_length <= data.cover_length_max || data.cover_length_max == null) &&

                    (x.additional_admin_unit.Contains(data.additional_admin_unit) || data.additional_admin_unit == null)
                )
                .Select(x => x.Objectid).ToList();
        }

        [HttpPost]
        public List<int> GetObjectIds(int channelId, bool isChild)
        {
            if (isChild)
                return _db.Asqueryable<Obj01ChannelsAdminUnit>(x => x.IdChannelAdminUnit == channelId).Select(x => x.Objectid).ToList();
            else
                return _db.Asqueryable<Obj01ChannelsAdminUnit>(x => x.FkIdChannel == channelId).Select(x => x.Objectid).ToList();
        }

        [HttpPost]
        public LoadResult LoadFilter(DataSourceLoadOptions loadOptions, string objectIds)
        {
            List<int> ids = new List<int>();
            if (!string.IsNullOrEmpty(objectIds))
            {
                ids = objectIds.Split(",").Select(int.Parse).ToList();
            }

            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            var adminUnitIds = _db.Asqueryable<Obj01ChannelsAdminUnit>().Where(x => userRegionIds.Contains((int)x.FkIdAdminUnit) && (ids.Contains(x.Objectid))).Select(x => x.FkIdChannel).ToList();
            
            return DataSourceLoader.Load(_db.Asqueryable<Obj01Channel>().Include(x => x.Obj01ChannelsAdminUnit).ThenInclude(x => x.Obj01Channel).Where(x => x.IsDeleted == 0 && (adminUnitIds.Contains(x.IdChannel))).Select(x => new
            {
                x.IdChannel,
                x.ExploitationDate,
                x.Controlled,
                x.Description,
                x.FkIdObjectType,
                x.Name,
                x.FkIdAppointment,
                x.FkIdSource,
                x.FkIdSourceType,
                x.Objectid,
                //CoverIds = x.Obj01ChannelsAdminUnit.SelectMany(z=>z.ChannelCover).Where(z=>z.Type == 1 && x.IsDeleted == 0).ToList()
                CoverIds = x.Obj01ChannelsAdminUnit.SelectMany(z=>z.ChannelCover).Where(z=>z.IsDeleted == 0).ToList()
            }), loadOptions);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int key)
        {
            bool result = true;
            var obj = _db.Asqueryable<Obj01Channel>().First(x => x.IdChannel == key);
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

            int channelId = _db.Asqueryable<Obj01ChannelsAdminUnit>().First(x => x.IdChannelAdminUnit == id).FkIdChannel ?? 0;
            
            if (channelId > 0)
            {
                int sourceId = _db.Asqueryable<Obj01Channel>().First(x => x.IdChannel == channelId).FkIdSource ?? 0;
                if (sourceId > 0)
                {
                    source = _db.Asqueryable<ObjViewSource>().First(x => x.Id == sourceId).Name;
                }
            }

            decimal allChannelLength = _db.Asqueryable<Obj01ChannelsAdminUnit>().Where(x => x.FkIdChannel == channelId).Sum(x => x.Length ?? 0);
            decimal adminUnitActualLength = _db.Asqueryable<DObjectAdminUnitBarrierType>().Where(x => x.FkIdObjectAdminUnit == id).First().Length ?? 0;
            List<int> allChannelAdminUnits = _db.Asqueryable<Obj01ChannelsAdminUnit>().Where(x => x.FkIdChannel == channelId).Select(x => x.IdChannelAdminUnit).ToList();
            decimal allChannelActualLength = _db.Asqueryable<DObjectAdminUnitBarrierType>().Where(x => allChannelAdminUnits.Contains(x.FkIdObjectAdminUnit ?? 0)).Sum(x => x.Length ?? 0);

            return _db.Asqueryable<Obj01ChannelsAdminUnit>().Where(x => x.IdChannelAdminUnit == id).ToList().Select(x => new
            {
                x.FkIdChannel,
                Name = x.Name ?? "məlumat yoxdur",
                ProtectionZoneArea = x.ProtectionZoneArea > 0 ? decimal.Round(x.ProtectionZoneArea ?? 0, 2).ToString() : "məlumat yoxdur",
                ServiceArea = x.ServiceArea > 0 ? decimal.Round(x.ServiceArea ?? 0, 2).ToString() : "məlumat yoxdur",
                WaterMetersCount = x.WaterMetersCount > 0 ? decimal.Round(x.WaterMetersCount ?? 0, 2).ToString() : "məlumat yoxdur",
                Length = x.Length > 0 ? decimal.Round(x.Length ?? 0, 2).ToString() : "məlumat yoxdur",
                Capacity = x.Capacity > 0 ? decimal.Round(x.Capacity ?? 0, 2).ToString() : "məlumat yoxdur",
                ExploitationDate = _db.Asqueryable<Obj01Channel>().FirstOrDefault(f => f.IdChannel == x.FkIdChannel)?.ExploitationDate > 0 ? _db.Asqueryable<Obj01Channel>().FirstOrDefault(f => f.IdChannel == x.FkIdChannel)?.ExploitationDate.ToString() : "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int?)x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Type = _db.Asqueryable<SObjectType>().FirstOrDefault(z => z.IdObjectType == (int?)x.FkIdObjectType)?.Name ?? "məlumat yoxdur",
                Owner = _db.Asqueryable<SOwner>().FirstOrDefault(z => z.IdOwner == (int?)x.FkIdOwner)?.Name ?? "məlumat yoxdur",
                Source = source ?? "məlumat yoxdur",
                AllLength = allChannelLength,
                ActualLength = adminUnitActualLength,
                AllActualLength = allChannelActualLength
            }).FirstOrDefault();
        }

    }
}
