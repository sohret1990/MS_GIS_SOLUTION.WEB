using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System;
using System.Collections.Generic;

namespace MS_GIS_SOLUTION.WEB.Models.ViewModels
{
    public class Obj03CollectorViewModel
    {
        public Obj03CollectorViewModel()
        {
            ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true};
        }
        public int Objectid { get; set; }
        public int? IdCollector { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? FkIdObjectType { get; set; }
        public int? FkIdAppointment { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public int? FkIdUser { get; set; }
        public short? IsDeleted { get; set; }
        public Guid GlobalId { get; set; }
        public int? FkReceivingSourceType { get; set; }
        public int? ServiceAdminUnit { get; set; }
        public int? FkIdReceivingSource { get; set; }
        public int? ExploitationDate { get; set; }

        public decimal? SumOfCapacity { get; set; }
        public decimal? MinCapacity { get; set; }
        public decimal? MaxCapacity { get; set; }
        public string Controlled { get; set; }
        public int FromSave { get; set; }
        public bool Mode { get; set; }
        public List<Obj03CollectorAdminUnit> Obj03CollectorAdminUnit { get; set; }
        public ButtonPanelViewModel ButtonPanel { get; set; }
    }
}
