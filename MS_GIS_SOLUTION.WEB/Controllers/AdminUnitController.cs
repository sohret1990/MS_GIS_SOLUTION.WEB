using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class AdminUnitController : BaseController
    {
        public AdminUnitController(MsGisDbContext db) : base(db)
        {

        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(UserRegions.OrderBy(x => x.Name), loadOptions);
        }

        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SAdminUnit>().ToList(), loadOptions);
        }

    }
}
