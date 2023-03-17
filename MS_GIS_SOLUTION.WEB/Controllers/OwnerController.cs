using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class OwnerController : GenericController<SOwner>
    {
        public OwnerController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Mülkiyyətçilər";
            KeyField = "IdOwner";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SOwner>().Select(x => new { Id = x.IdOwner, x.Name }).Distinct(), loadOptions);
        }
    }
}
