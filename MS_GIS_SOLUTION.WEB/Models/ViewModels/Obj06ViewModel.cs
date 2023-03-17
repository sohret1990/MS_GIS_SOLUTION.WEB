using MS_GIS_SOLUTION.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MS_GIS_SOLUTION.WEB.Models.ViewModels
{
    public class Obj06ViewModel
    {
        public Obj06ViewModel()
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
        public int? IdHydrotechnicalInstallations { get; set; }
        public int? FkIdHtiNetworkType { get; set; }
        public int? FkIdActivityStatus { get; set; }
        public int? FkIdTechnicalStatus { get; set; }
        public int? FkIdHtInstallationType { get; set; }
        public int? FkIdOwnershipType { get; set; }
        public int? FkIdOwner { get; set; }
        public int? FkIdSourceType { get; set; }
        public int? FkIdSource { get; set; }
        public int? Depth { get; set; }
        public decimal? WaterPermeability { get; set; }
        public string TechnicalParameters { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public int? FkIdUser { get; set; }
        public short? IsDeleted { get; set; }
        public byte[] GdbGeomattrData { get; set; }
        public Guid GlobalId { get; set; }
        public int? ExploitationDate { get; set; }
        public string Controlled { get; set; }
        public int? IsPhoto { get; set; }
        public bool Mode { get; set; }
        public ButtonPanelViewModel ButtonPanel { get; set; }
        public List<string> ImagesForGallery { get; set; }
    }
}
