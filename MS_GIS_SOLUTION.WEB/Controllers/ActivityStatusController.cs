using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ActivityStatusController : GenericController<SActivityStatus>
    {
        public ActivityStatusController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Aktivlik statusları";
            KeyField = "IdActivityStatus";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SActivityStatus>().Select(x => new { Id = x.IdActivityStatus, x.Name }), loadOptions);
        }

    }
}
