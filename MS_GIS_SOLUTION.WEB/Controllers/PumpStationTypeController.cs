using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class PumpStationTypeController : GenericController<SPumpStationType>
    {
        public PumpStationTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Nasos stansiyasının tipləri";
            KeyField = "IdPumpStationType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SPumpStationType>().Select(x => new { Id = x.IdPumpStationType, x.Name }).Distinct(), loadOptions);
        }
    }
}
