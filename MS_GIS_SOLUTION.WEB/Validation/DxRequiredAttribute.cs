using System.ComponentModel.DataAnnotations;

namespace MS_GIS_SOLUTION.WEB.Validation
{
    public class DxRequiredAttribute : RequiredAttribute
    {
        public override string FormatErrorMessage(string name)
        {
            return "Məcburi xanadır";
        }
    }
}
