using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MS_GIS_SOLUTION.WEB.Models.ViewModels
{
    public class Obj08PumpStationViewModel
    {
        public Obj08PumpStationViewModel()
        {
            ButtonPanel = new ButtonPanelViewModel { Add = true, Update = true, Delete = true, View = true, ShowMap = true };
        }


        [Required(ErrorMessage = "Obyekt seçilməyib")]
        public int? Objectid { get; set; }
        public int? IdPumpStation { get; set; }

        [Required(ErrorMessage = "Ad seçilməyib")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Yerləşdiyi ərazi seçilməyib")]
        public int? FkIdAdminUnit { get; set; }
        public int? FkIdSourceType { get; set; }
        public int? FkIdSource { get; set; }
        public int? FkIdActivityStatus { get; set; }
        public int? FkIdTechnicalStatus { get; set; }
        public int? FkIdAppointment { get; set; }
        public int? FkIdPumpStationType { get; set; }
        public int? FkIdPumpStationEngineType { get; set; }
        public int? FkIdOwnershipType { get; set; }
        public int? FkIdOwner { get; set; }
        public decimal? TotalCapacity { get; set; }
        public decimal? ServiceArea { get; set; }
        public decimal? Productivity { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string InsertUser { get; set; }
        public string UpdateUser { get; set; }
        public int? FkIdUser { get; set; }
        public short? IsDeleted { get; set; }
        public byte[] GdbGeomattrData { get; set; }
        public Guid GlobalId { get; set; }
        public int? AggregatesCount { get; set; }
        public int? ExploitationDate { get; set; }
        public string AdditionalAdminUnit { get; set; }
        public int? LastInstallationDate { get; set; }
        public string Controlled { get; set; }
        public int FromSave { get; set; }

        public bool Mode { get; set; }
        public List<DPumpStationsDevice> DPumpStationsDevice { get; set; }
        public ButtonPanelViewModel ButtonPanel { get; set; }
    }
}
