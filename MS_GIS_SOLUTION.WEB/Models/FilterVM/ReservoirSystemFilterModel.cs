using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public class ReservoirSystemFilterModel
    {
        public string Name { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> FkIdWrLocationType { get; set; }
        public List<int?> FkIdAppointment { get; set; }
        public List<int?> FkIdAppointmentAddition { get; set; }
        public List<int?> FkIdSourceType { get; set; }
        public List<int?> FkIdSource { get; set; }
        public List<int?> FkIdActivityStatus { get; set; }
        public List<int?> FkIdTechnicalStatus { get; set; }
        public List<int?> FkIdOwner { get; set; }
        public List<int?> FkIdProtectionType { get; set; }
        public List<int?> FkIdWrRegulationType { get; set; }
        public List<int?> FkIdFedObjects { get; set; }
        public List<int?> FkIdOwnershipType { get; set; }


        public decimal? ProtectionZoneAreaMin { get; set; }
        public decimal? ProtectionZoneAreaMax { get; set; }
        
        public decimal? ServiceAreaMin { get; set; }
        public decimal? ServiceAreaMax { get; set; }

        public decimal? NormalPressureMin { get; set; }
        public decimal? NormalPressureMax { get; set; }

        public decimal? DeadVolumeMin { get; set; }
        public decimal? DeadVolumeMax { get; set; }

        public decimal? MinorAreaAMin { get; set; }
        public decimal? MinorAreaAMax { get; set; }

        public decimal? MinorAreaBMin { get; set; }
        public decimal? MinorAreaBMax { get; set; }

        public decimal? VolumeAMin { get; set; }
        public decimal? VolumeAMax { get; set; }

        public decimal? VolumeBMin { get; set; }
        public decimal? VolumeBMax { get; set; }
        
        public int? ExploitationDateMin { get; set; }
        public int? ExploitationDateMax { get; set; }


        public string Dam { get; set; }
    }
}
