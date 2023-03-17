using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public partial class CollectorFilterModel
    {
        public int channel_objectid { get; set; }
        public string name { get; set; }
        public List<int?> fk_id_object_type { get; set; }
        public List<int?> fk_id_appointment { get; set; }
        public List<int?> fk_id_missing_source_type { get; set; }
        public List<int?> fk_receiving_source_type { get; set; }
        public List<int?> fk_id_receiving_source { get; set; }
        public List<int?> fk_id_source_type { get; set; }
        public List<int?> fk_id_source { get; set; }
        public List<int?> service_admin_unit { get; set; }
        public List<int?> fk_id_water_use_association { get; set; }

        public int? exploitation_date_min { get; set; }
        public int? exploitation_date_max { get; set; }

        public decimal? min_capacity_min { get; set; }
        public decimal? min_capacity_max { get; set; }

        public decimal? max_capacity_min { get; set; }
        public decimal? max_capacity_max { get; set; }
        
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> fk_id_technical_status { get; set; }
        public List<int?> fk_id_ownership_type { get; set; }
        public List<int?> fk_id_owner { get; set; }
        public List<int?> fk_id_activity_status { get; set; }
        public List<int?> fk_id_object_type_au { get; set; }


        public decimal? drained_area_min { get; set; }
        public decimal? drained_area_max { get; set; }

        public decimal? capacity_min { get; set; }
        public decimal? capacity_max { get; set; }

        public decimal? protection_zone_area_min { get; set; }
        public decimal? protection_zone_area_max { get; set; }

        public decimal? length_min { get; set; }
        public decimal? length_max { get; set; }

        public decimal? length_a_min { get; set; }
        public decimal? length_a_max { get; set; }

        public decimal? length_b_min { get; set; }
        public decimal? length_b_max { get; set; }

        public decimal? device_count_min { get; set; }
        public decimal? device_count_max { get; set; }

        public decimal? water_consumption_min { get; set; }
        public decimal? water_consumption_max { get; set; }

        public decimal? exploitation_date_au_min { get; set; }
        public decimal? exploitation_date_au_max { get; set; }

        public decimal? actual_length_min { get; set; }
        public decimal? actual_length_max { get; set; }
    }
}
