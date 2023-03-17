using MS_GIS_SOLUTION.Core;
using System.ComponentModel.DataAnnotations;

namespace MS_GIS_SOLUTION.WEB.Models.ViewModels
{
    public class Obj10ProtectionBarrierViewModel
    {
        public Obj10ProtectionBarrierViewModel()
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
        public int? IdProtectionBarriers { get; set; }
        public int? FkIdAppointment { get; set; }
        public int? FkIdTechnicalStatus { get; set; }
        public int? FkIdActivityStatus { get; set; }
        public int? FkIdOwnershipType { get; set; }
        public int? FkIdOwner { get; set; }
        public int? FkIdBarrierType { get; set; }
        public int? FkIdRiver { get; set; }
        public decimal? BarrierLength { get; set; }
        public decimal? BarrierHeight { get; set; }
        public int? RiverName { get; set; }
        public int? ExploitationDate { get; set; }
        public bool Mode { get; set; }
        public ButtonPanelViewModel ButtonPanel { get; set; }
    }
}
