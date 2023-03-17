using System.Collections.Generic;

namespace MS_GIS_SOLUTION.WEB.Models
{
    public class ChannelFilterModel : FilterModelBase
    {
        public string name { get; set; }
        public List<int?> fk_id_object_type { get; set; }
        public List<int?> fk_id_appointment { get; set; }
        public List<int?> fk_id_source_type { get; set; }
        public List<int?> fk_id_source { get; set; }
        public List<int?> fk_id_missing_source_type { get; set; }
        public int? exploitation_date_min { get; set; }
        public int? exploitation_date_max { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> fk_id_technical_status { get; set; }


        public decimal? service_area_min { get; set; }
        public decimal? service_area_max { get; set; }


        public decimal? capacity_min { get; set; }
        public decimal? capacity_max { get; set; }

        public decimal? length_min { get; set; }
        public decimal? length_max { get; set; }


        public decimal? protection_zone_area_min { get; set; }
        public decimal? protection_zone_area_max { get; set; }
        
        public List<int?> fk_id_ownership_type { get; set; }
        public List<int?> fk_id_owner { get; set; }


        public int? device_count_min { get; set; }
        public int? device_count_max { get; set; }


        public int? water_meters_count_min { get; set; }
        public int? water_meters_count_max { get; set; }


        public List<int?> fk_id_activity_status { get; set; }
        public string partial_channel_name { get; set; }
        public List<int?> fk_id_object_admin_unit { get; set; }
        public List<int?> fk_id_water_use_association { get; set; }
        public string additional_admin_unit { get; set; }

        public int? exploitation_date_admin_unit_min { get; set; }
        public int? exploitation_date_admin_unit_max { get; set; }

        public List<int?> fk_cover_id_barrier_type { get; set; }
        public decimal? cover_length_min { get; set; }
        public decimal? cover_length_max { get; set; }
    }
}
