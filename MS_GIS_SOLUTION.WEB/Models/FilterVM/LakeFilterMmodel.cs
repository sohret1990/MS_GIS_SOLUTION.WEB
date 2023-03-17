using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public class LakeFilterModel
    {
        public string name { get; set; }
        public List<int?> fk_id_admin_unit { get; set; }

        public decimal? VolumeMin { get; set; }
        public decimal? VolumeMax { get; set; }
    }
}
