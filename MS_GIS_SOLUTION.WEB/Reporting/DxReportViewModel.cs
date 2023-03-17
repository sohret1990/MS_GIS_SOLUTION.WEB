using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Parameter
    {
        public List<string> Value { get; set; }
        public string Key { get; set; }
        public string TypeName { get; set; }
    }

    public class DxReportViewModel
    {
        public string reportId { get; set; }
        public string reportUrl { get; set; }
        public List<object> drillDownKeys { get; set; }
        public List<object> sortingState { get; set; }
        public int timeZoneOffset { get; set; }
        public List<Parameter> parameters { get; set; }
    }
}
