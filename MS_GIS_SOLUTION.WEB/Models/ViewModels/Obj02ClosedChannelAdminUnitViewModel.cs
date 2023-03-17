using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MS_GIS_SOLUTION.WEB.Models.ViewModels
{
    public class Obj02ClosedChannelAdminUnitViewModel
    {
        public Obj02ClosedChannelAdminUnitViewModel()
        {
            ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true };
        }

        [Required(ErrorMessage = "Obyekt seçilməyib")]
        public int? Objectid { get; set; }
        public int IdClosedChannelAdminUnit { get; set; }
        public int? FkIdClosedChannel { get; set; }

        [Required(ErrorMessage = "Yerləşdiyi ərazi seçilməyib")]
        public int? FkIdAdminUnit { get; set; }
        public int? FkIdTechnicalStatus { get; set; }
        public decimal? ServiceArea { get; set; }
        public decimal? Capacity { get; set; }
        public decimal? ProtectionZoneArea { get; set; }
        public decimal? Length { get; set; }
        public int? FkIdOwnershipType { get; set; }
        public int? FkIdOwner { get; set; }
        public int? DeviceCount { get; set; }
        public int? WaterMetersCount { get; set; }
        public int? OrderNo { get; set; }
        public int? FkIdActivityStatus { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public int? FkIdUser { get; set; }
        public short? IsDeleted { get; set; }
        public string Name { get; set; }
        public byte[] GdbGeomattrData { get; set; }
        public Guid GlobalId { get; set; }
        public int? FkIdObjectType { get; set; }
        public string AdditionalAdminUnit { get; set; }
        public int? FkIdServiceAdminUnit { get; set; }
        public int? ExploitationDate { get; set; }
        public string Controlled { get; set; }
        public List<int?> WUAIdList { get; set; }
        public bool Mode { get; set; }


        public List<Obj02ClosedChannelAdminUnitPipeInfo> Obj02ClosedChannelAdminUnitPipeInfo { get; set; }
        public ButtonPanelViewModel ButtonPanel { get; set; }
    }
}
