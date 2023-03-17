using System.Collections.Generic;

namespace MS_GIS_SOLUTION.WEB.Models
{
    public class PumpStationFilterModel : FilterModelBase
    {
        public string name { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> fk_id_source_type { get; set; }
        public List<int?> fk_id_source { get; set; }
        public List<int?> fk_id_missing_source_type { get; set; }
        public List<int?> fk_id_activity_status { get; set; }
        public List<int?> fk_id_technical_status { get; set; }
        public List<int?> fk_id_appointment { get; set; }
        public List<int?> fk_id_pump_station_type { get; set; }
        public List<int?> fk_id_pump_station_engine_type { get; set; }
        public List<int?> fk_id_ownership_type { get; set; }
        public List<int?> fk_id_owner { get; set; }
        public List<int?> fk_id_protection_type { get; set; }
        public decimal? protection_zone_area_min { get; set; }
        public decimal? protection_zone_area_max { get; set; }
        public decimal? total_capacity_min { get; set; }
        public decimal? total_capacity_max { get; set; }
        public decimal? service_area_min { get; set; }

        public decimal? service_area_max { get; set; }



        public decimal? productivity_min { get; set; }

        public decimal? productivity_max { get; set; }



        public int? aggregates_count_min { get; set; }

        public int? aggregates_count_max { get; set; }

        public string aggregates_model { get; set; }



        public int? EXPLOITATION_DATE_min { get; set; }

        public int? EXPLOITATION_DATE_max { get; set; }

        public List<int?> fk_id_loc_water_source_type { get; set; }
        public List<int?> fk_id_located_water_source { get; set; }
        public List<int?> additional_admin_unit { get; set; }


        public int? last_installation_date_min { get; set; }

        public int? last_installation_date_max { get; set; }

        public string controlled { get; set; }

        public string pump_model { get; set; }


        public int? pump_last_repair_date_min { get; set; }

        public int? pump_last_repair_date_max { get; set; }



        public decimal? pump_productivity_min { get; set; }

        public decimal? pump_productivity_max { get; set; }

        public string engine_model { get; set; }


        public int? engine_last_installation_date_min { get; set; }

        public int? engine_last_installation_date_max { get; set; }



        public int? engine_last_repair_date_min { get; set; }

        public int? engine_last_repair_date_max { get; set; }



        public decimal? engine_pipeline_diameter_min { get; set; }


        public decimal? engine_pipeline_diameter_max { get; set; }


        public decimal? engine_pipeline_length_min { get; set; }

        public decimal? engine_pipeline_length_max { get; set; }

        public string engine_pipeline_material { get; set; }



        public decimal? engine_horsepower_min { get; set; }

        public decimal? engine_horsepower_max { get; set; }



        public decimal? engine_power_min { get; set; }

        public decimal? engine_power_max { get; set; }



        public decimal? pump_pressure_min { get; set; }

        public decimal? pump_pressure_max { get; set; }
        public List<int?> fk_id_pse_synchronize_type { get; set; }
    }
}
