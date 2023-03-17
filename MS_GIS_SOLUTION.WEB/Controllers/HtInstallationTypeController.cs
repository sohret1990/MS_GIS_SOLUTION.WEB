using System.Linq;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class HtInstallationTypeController : GenericController<SHtInstallationType>
    {
        public HtInstallationTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Hidrotexniki qurğu tipləri";
            KeyField = "IdHtInstallationType";
        }


        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SHtInstallationType>().ToList().Select(x => new { Id = x.IdHtInstallationType, x.Name }).Distinct(), loadOptions);
        }

    }
}
