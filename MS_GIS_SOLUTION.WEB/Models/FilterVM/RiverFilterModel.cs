using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Models.FilterVM
{
    public class RiverFilterModel
    {
        public string Name { get; set; }
        public int? LenghtMin { get; set; }
        public int? LenghtMax { get; set; }
    }
}
