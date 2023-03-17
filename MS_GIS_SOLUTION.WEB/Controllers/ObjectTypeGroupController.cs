using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ObjectTypeGroupController : GenericController<SObjectTypeGroup>
    {
        public ObjectTypeGroupController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Obyekt tip qrupları";
            KeyField = "IdObjectTypeGroup";
        }
    }
}
