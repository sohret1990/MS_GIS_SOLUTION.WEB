using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models
{
    public class HydroJunctionsFilterModel
    {
        public string name { get; set; }

        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> fk_id_appointment { get; set; }
        public List<int?> fk_id_source_type { get; set; }
        public List<int?> fk_id_source { get; set; }
        public List<int?> fk_id_missing_source_type { get; set; }
        public List<int?> fk_id_activity_status { get; set; }
        public List<int?> fk_id_technical_status { get; set; }
        public List<int?> fk_id_hydro_junction_type { get; set; }
        public List<int?> fk_id_ownership_type { get; set; }
        public List<int?> fk_id_owner { get; set; }
        public List<int?> fk_id_protection_type { get; set; }
        public List<int?> fk_id_fed_objects { get; set; }
        public List<int?> service_admin_unit { get; set; }

        public decimal? protection_zone_area_min { get; set; }
        public decimal? protection_zone_area_max { get; set; }

        public decimal? service_area_min { get; set; }
        public decimal? service_area_max { get; set; }

        public decimal? max_water_release_capacity_min { get; set; }
        public decimal? max_water_release_capacity_max { get; set; }

        public decimal? dam_length_min { get; set; }
        public decimal? dam_length_max { get; set; }

        public decimal? EXPLOITATION_DATE_min { get; set; }
        public decimal? EXPLOITATION_DATE_max { get; set; }
    }
}
