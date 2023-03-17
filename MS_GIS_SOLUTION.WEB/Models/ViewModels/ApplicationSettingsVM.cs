using MS_GIS_SOLUTION.Entities;
using System.Collections.Generic;

namespace MS_GIS_SOLUTION.WEB.Models
{
    public partial class ApplicationSettingsVM
    {
        public ApplicationSettingsVM()
        {
            ErpSettings = new HashSet<ErpSettings>();
            ErpSettingsFieldType = new HashSet<ErpSettingsFieldType>();
            ErpSettingsGroup = new HashSet<ErpSettingsGroup>();
            ApplicationThemes = new List<string>();
        }
        public virtual ICollection<ErpSettings> ErpSettings { get; set; }
        public virtual ICollection<ErpSettingsFieldType> ErpSettingsFieldType { get; set; }
        public virtual ICollection<ErpSettingsGroup> ErpSettingsGroup { get; set; }
        public virtual List<string> ApplicationThemes { get; set; }
        public string Theme { get; set; }
    }
}
