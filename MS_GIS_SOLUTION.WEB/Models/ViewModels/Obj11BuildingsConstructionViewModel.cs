using MS_GIS_SOLUTION.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace MS_GIS_SOLUTION.WEB.Models.ViewModels
{
    public class Obj11BuildingsConstructionViewModel
    {
        public Obj11BuildingsConstructionViewModel()
        {
            ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true };
        }
        [Required(ErrorMessage = "Obyekt seçilməyib")]
        public int? Objectid { get; set; }

        [Required(ErrorMessage = "Ad seçilməyib")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Yerləşdiyi ərazi seçilməyib")]
        public int? FkIdAdminUnit { get; set; }
        public int? IdBuildingsConstructions { get; set; }
        public string ReyestrNumber { get; set; }
        public int? FkIdAppointment { get; set; }
        public int? FkIdTechnicalStatus { get; set; }
        public int? FkIdActivityStatus { get; set; }
        public int? FkIdOwnershipType { get; set; }
        public int? FkIdOwner { get; set; }
        public decimal? Area { get; set; }
        public decimal? AreaA { get; set; }
        public decimal? AreaB { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public int? FkIdUser { get; set; }
        public short? IsDeleted { get; set; }
        public byte[] GdbGeomattrData { get; set; }
        public Guid GlobalId { get; set; }
        public string AdditionalAdminUnit { get; set; }
        public decimal? ServiceArea { get; set; }
        public string FkIdProtectionType { get; set; }
        public string UsageMode { get; set; }
        public int? ExploitationDate { get; set; }
        public bool Mode { get; set; }
        public ButtonPanelViewModel ButtonPanel { get; set; }
    }
}
