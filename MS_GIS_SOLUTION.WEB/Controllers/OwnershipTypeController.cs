using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class OwnershipTypeController : GenericController<SOwnershipType>
    {
        public OwnershipTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Mülkiyyət növləri";
            KeyField = "IdOwnershipType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SOwnershipType>().Select(x => new { Id = x.IdOwnershipType, x.Name }).Distinct(), loadOptions);
        }
    }
}
