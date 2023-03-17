using MS_GIS_SOLUTION.Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace MS_GIS_SOLUTION.WEB.Models.ViewModels
{
    public class Obj04ReservoirSystemViewModel
    {
        public Obj04ReservoirSystemViewModel()
        {
            ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true };
        }

        [Required(ErrorMessage = "Obyekt seçilməyib")]
        public int? Objectid { get; set; }
        public int? IdReservoirSystem { get; set; }
        [Required(ErrorMessage = "Ad seçilməyib")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Yerləşdiyi ərazi seçilməyib")]
        public int? FkIdAdminUnit { get; set; }
        public int? FkIdWrLocationType { get; set; }
        public int? FkIdAppointment { get; set; }
        public int? FkIdAppointmentAddition { get; set; }
        public int? FkIdSourceType { get; set; }
        public int? FkIdSource { get; set; }
        public int? FkIdActivityStatus { get; set; }
        public int? FkIdTechnicalStatus { get; set; }
        public int? FkIdOwner { get; set; }
        public decimal? ProtectionZoneArea { get; set; }
        public int? FkIdProtectionType { get; set; }
        public int? FkIdWrRegulationType { get; set; }
        public decimal? ServiceArea { get; set; }
        public decimal? NormalPressure { get; set; }
        public decimal? DeadVolume { get; set; }
        public decimal? MinorAreaA { get; set; }
        public decimal? MinorAreaB { get; set; }
        public decimal? VolumeA { get; set; }
        public decimal? VolumeB { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public int? FkIdUser { get; set; }
        public short? IsDeleted { get; set; }
        public byte[] GdbGeomattrData { get; set; }
        public Guid GlobalId { get; set; }
        public int? FkIdFedObjects { get; set; }
        public int? ExploitationDate { get; set; }
        public string Dam { get; set; }
        public int? FkIdOwnershipType { get; set; }
        public bool Mode { get; set; }
        public ButtonPanelViewModel ButtonPanel { get; set; }
    }
}
