using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class PumpStationEngineTypeController : GenericController<SPumpStationEngineType>
    {
        public PumpStationEngineTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Nasos stansiyasının növləri";
            KeyField = "IdPumpStationEngineType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SPumpStationEngineType>().Select(x => new { Id = x.IdPumpStationEngineType, x.Name }).Distinct(), loadOptions);
        }
    }
}
