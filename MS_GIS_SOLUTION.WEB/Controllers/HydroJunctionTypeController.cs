using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class HydroJunctionTypeController : GenericController<SHydroJunctionType>
    {
        public HydroJunctionTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Hidroqovşaq növləri";
            KeyField = "IdHydroJunctionType";
        }


        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SHydroJunctionType>().Select(x => new { Id = x.IdHydroJunctionType, x.Name }).Distinct(), loadOptions);
        }
    }
}
