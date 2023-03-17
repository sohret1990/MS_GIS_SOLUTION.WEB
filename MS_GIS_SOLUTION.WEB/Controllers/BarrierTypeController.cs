using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class BarrierTypeController : GenericController<SBarrierType>
    {
        public BarrierTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Bənd növləri";
            KeyField = "IdBarrierType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SBarrierType>().Select(x => new { Id = x.IdBarrierType, x.Name }).Distinct(), loadOptions);
        }
    }
}
