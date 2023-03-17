using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public class ArtesiaAndSubartesianFilterModel
    {
        public string Name { get; set; }
        public string No { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> FkIdActivityStatus { get; set; }
        public List<int?> FkIdTechnicalStatus { get; set; }
        public List<int?> FkIdArtesianType { get; set; }
        public List<int?> FkIdOwnershipType { get; set; }
        public List<int?> FkIdOwner { get; set; }
        public List<int?> FkIdWaterUseAssociation { get; set; }
        public List<int?> FkIdProtectionType { get; set; }
        public List<int?> FkIdAppointment { get; set; }

        public decimal? ProtectionZoneAreaMin { get; set; }
        public decimal? ProtectionZoneAreaMax { get; set; }


        public decimal? DepthMin { get; set; }
        public decimal? DepthMax { get; set; }


        public decimal? ServiceAreaMin { get; set; }
        public decimal? ServiceAreaMax { get; set; }


        public decimal? ProductivityMin { get; set; }
        public decimal? ProductivityMax { get; set; }

        public decimal? EnginePoverMin { get; set; }
        public decimal? EnginePoverMax { get; set; }

        public int? ExploitationDateMin { get; set; }
        public int? ExploitationDateMax { get; set; }

        public string ResidentialArea { get; set; }
        public int? ScadaInfo { get; set; }
        public string Description { get; set; }
        public string PumpModel { get; set; }
        public string PumpType { get; set; }
        public string RepairDate { get; set; }
    }
}
