using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class WaterRetentionRegulationTypeController : GenericController<SWaterRetentionRegulationType>
    {
        public WaterRetentionRegulationTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Su anbarları / Su tutarları nizamlanma növləri";
            KeyField = "IdWRetRegulationType";
        }
        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SWaterRetentionRegulationType>().Select(x => new { Id = x.IdWRetRegulationType, x.Name }).Distinct(), loadOptions);
        }
    }
}
