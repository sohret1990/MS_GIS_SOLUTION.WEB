using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System;
using System.Collections.Generic;

namespace MS_GIS_SOLUTION.WEB.Models.ViewModels
{
    public class Obj01ChannelViewModel
    {
        public Obj01ChannelViewModel()
        {
            ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true };
        }
        public int Objectid { get; set; }
        public int? IdChannel { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FkIdObjectType { get; set; }
        public int? FkIdSourceType { get; set; }
        public int? FkIdSource { get; set; }
        public int? FkIdMissingSourceType { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public int? FkIdUser { get; set; }
        public short? IsDeleted { get; set; }
        public Guid GlobalId { get; set; }
        public int? FkIdAppointment { get; set; }
        public int? ExploitationDate { get; set; }
        public string Controlled { get; set; }
        public int FromSave { get; set; }
        public bool Mode { get; set; }
        public List<Obj01ChannelsAdminUnit> Obj01ChannelsAdminUnit { get; set; }
        public ButtonPanelViewModel ButtonPanel { get; set; }
    }
}
