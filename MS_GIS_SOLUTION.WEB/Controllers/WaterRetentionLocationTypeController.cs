using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class WaterRetentionLocationTypeController : GenericController<SWaterRetentionLocationType>
    {
        public WaterRetentionLocationTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Su anbarları / Su tutarları yerləşmə növləri";
            KeyField = "IdWRetLocationType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SWaterRetentionLocationType>().Select(x => new { Id = x.IdWRetLocationType, x.Name }).Distinct(), loadOptions);
        }
    }
}
