using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public class HydrotechnicalInstallationFilterModel
    {
        public HydrotechnicalInstallationFilterModel()
        {
            fk_id_admin_unit = new List<int?>();
            fk_id_hti_network_type = new List<int?>();
            fk_id_activity_status = new List<int?>();
            fk_id_technical_status = new List<int?>();
            fk_id_ht_installation_type = new List<int?>();
            fk_id_ownership_type = new List<int?>();
            fk_id_ownership_type = new List<int?>();
            fk_id_owner = new List<int?>();
            fk_id_protection_type = new List<int?>();
            fk_id_source_type = new List<int?>();
            fk_id_source = new List<int?>();
            fk_id_missing_source_type = new List<int?>();
        }

        public string name { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> fk_id_hti_network_type { get; set; }
        public List<int?> fk_id_activity_status { get; set; }
        public List<int?> fk_id_technical_status { get; set; }
        public List<int?> fk_id_ht_installation_type { get; set; }
        public List<int?> fk_id_ownership_type { get; set; }
        public List<int?> fk_id_owner { get; set; }
        public List<int?> fk_id_protection_type { get; set; }
        public List<int?> fk_id_source_type { get; set; }
        public List<int?> fk_id_source { get; set; }
        public List<int?> fk_id_missing_source_type { get; set; }

        public decimal? protection_zone_area_min { get; set; }
        public decimal? protection_zone_area_max { get; set; }

        public decimal? depth_min { get; set; }
        public decimal? depth_max { get; set; }

        public decimal? water_permeability_min { get; set; }
        public decimal? water_permeability_max { get; set; }
        
        public string technical_parameters { get; set; }

        public decimal? EXPLOITATION_DATE_min { get; set; }
        public decimal? EXPLOITATION_DATE_max { get; set; }
        
    }
}
