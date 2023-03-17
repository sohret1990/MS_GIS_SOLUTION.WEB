using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class PumpStationEngineSynchronizeTypeController : GenericController<SPumpStationEngineSynchronizeType>
    {
        public PumpStationEngineSynchronizeTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Mühərrik növləri";
            KeyField = "IdPumpsEngSynchronizeType";
        }


        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions, int groupId)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SPumpStationEngineSynchronizeType>().Select(x => new { Id = x.IdPumpsEngSynchronizeType, x.Name }).Distinct(), loadOptions);
        }
    }
}
