using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public class ClosedChannelFilterModel
    {
        public int channel_objectid { get; set; }
        public string name { get; set; }
        public List<int?> fk_id_object_type { get; set; }
        public List<int?> fk_id_source_type { get; set; }
        public List<int?> fk_id_source { get; set; }
        public List<int?> fk_id_missing_source_type { get; set; }
        public List<int?> fk_id_appointment { get; set; }

        public int? exploitation_date_min { get; set; }
        public int? exploitation_date_max { get; set; }

        public int? OBJECTID { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> fk_id_technical_status { get; set; }

        public decimal? service_area_min { get; set; }
        public decimal? service_area_max { get; set; }

        public decimal? capacity_min { get; set; }
        public decimal? capacity_max { get; set; }

        public decimal? protection_zone_area_min { get; set; }
        public decimal? protection_zone_area_max { get; set; }

        public decimal? length_min { get; set; }
        public decimal? length_max { get; set; }

        public List<int?> fk_id_ownership_type { get; set; }
        public List<int?> fk_id_owner { get; set; }

        public int? device_count_min { get; set; }
        public int? device_count_max { get; set; }

        public int? water_meters_count_min { get; set; }
        public int? water_meters_count_max { get; set; }

        public List<int?> fk_id_activity_status { get; set; }

        public string pipe_name { get; set; }

        public string additional_admin_unit { get; set; }

        public List<int?> fk_id_object_type_pipe { get; set; }
        public List<int?> fk_id_service_admin_unit { get; set; }
        public List<int?> fk_id_pipe_type { get; set; }
        
        public List<int?> fk_id_object_type_admin_unit { get; set; }

        public int? pipe_length_min { get; set; }
        public int? pipe_length_max { get; set; }

        public int? exploitation_date_pipe_min { get; set; }
        public int? exploitation_date_pipe_max { get; set; }

        public List<int?> fk_cover_id_barrier_type { get; set; }
        public decimal? cover_length_min { get; set; }
        public decimal? cover_length_max { get; set; }
    }
}
