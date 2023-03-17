using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public class ProtectionBarrierFilterModel
    {
        public int Objectid { get; set; }
        public string Name { get; set; }

        public List<int?> fk_id_admin_unit { get; set; }
        public List<int?> FkIdAppointment { get; set; }
        public List<int?> FkIdTechnicalStatus { get; set; }
        public List<int?> FkIdActivityStatus { get; set; }
        public List<int?> FkIdOwnershipType { get; set; }
        public List<int?> FkIdOwner { get; set; }
        public List<int?> FkIdBarrierType { get; set; }

        public decimal? BarrierLengthMin { get; set; }
        public decimal? BarrierLengthMax { get; set; }

        public decimal? BarrierHeightMin { get; set; }
        public decimal? BarrierHeightMax { get; set; }

        public List<int?> FkIdRiver { get; set; }

        public int? ExploitationDateMin { get; set; }
        public int? ExploitationDateMax { get; set; }
    }
}
