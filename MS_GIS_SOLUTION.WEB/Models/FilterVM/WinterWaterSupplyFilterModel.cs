using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public class WinterWaterSupplyFilterModel
    {
        public int Objectid { get; set; }
        public string Name { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> FkIdActivityStatus { get; set; }
        public List<int?> FkIdOwnershipType { get; set; }
        public List<int?> FkIdOwner { get; set; }

        public decimal? ServiceAreaMin { get; set; }
        public decimal? ServiceAreaMax { get; set; }

        public decimal? ClosedIrrigationNetworksMin { get; set; }
        public decimal? ClosedIrrigationNetworksMax { get; set; }

        public int? DeviceCountMin { get; set; }
        public int? DeviceCountMax { get; set; }

        public int? ExploitationDateMin { get; set; }
        public int? ExploitationDateMax { get; set; }

        
    }
}
