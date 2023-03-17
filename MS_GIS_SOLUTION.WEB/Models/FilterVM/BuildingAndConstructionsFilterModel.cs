using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models
{
    public class BuildingAndConstructionsFilterModel
    {
        public int Objectid { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> FkIdAppointment { get; set; }
        public List<int?> FkIdTechnicalStatus { get; set; }
        public List<int?> FkIdActivityStatus { get; set; }
        public List<int?> FkIdOwnershipType { get; set; }
        public List<int?> FkIdOwner { get; set; }
        public List<int?> FkIdProtectionType { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ReyestrNumber { get; set; }

        public decimal? AreaMin { get; set; }
        public decimal? AreaMax { get; set; }

        public decimal? AreaAMin { get; set; }
        public decimal? AreaAMax { get; set; }


        public decimal? AreaBMin { get; set; }
        public decimal? AreaBMax { get; set; }
        
        public string AdditionalAdminUnit { get; set; }
        public decimal? ServiceAreaMin { get; set; }
        public decimal? ServiceAreaMax { get; set; }
        public string UsageMode { get; set; }
        public int? ExploitationDateMin { get; set; }
        public int? ExploitationDateMax { get; set; }
    }
}
