using MS_GIS_SOLUTION.Entities;
using System;
using System.Collections.Generic;

namespace MS_GIS_SOLUTION.WEB.Models
{
    [Serializable]
    public class LeftMenuVM
    {
        public LeftMenuVM()
        {
            Menu = new List<ErpMenu>();
            Operation = new List<ErpOperation>();
            MenuOperation = new List<ErpMenuOperation>();

        }

        public List<ErpMenu> Menu { get; set; }
        public List<ErpOperation> Operation { get; set; }
        public List<ErpMenuOperation> MenuOperation { get; set; }
    }
}
