using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;


namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class CoverTypeController : GenericController<SCoverType>
    {
        public CoverTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Örtük növləri";
            KeyField = "IdCoverType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SCoverType>().Select(x => new { Id = x.IdCoverType, x.Name }).Distinct(), loadOptions);
        }
    }
}
