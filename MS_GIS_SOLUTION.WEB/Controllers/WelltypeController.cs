using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class WellTypeController : GenericController<SWellType>
    {
        public WellTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Quyu növləri";
            KeyField = "IdWellType";
        }
    }
}
