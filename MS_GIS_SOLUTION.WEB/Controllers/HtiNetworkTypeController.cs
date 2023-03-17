using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class HtiNetworkTypeController : GenericController<SHydrotechnicalInstallationNetworkTypeBo>
    {
        public HtiNetworkTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Şəbəkənin növü";
            KeyField = "IdHtInstallationNetworkType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SHydrotechnicalInstallationNetworkTypeBo>().ToList().Select(x => new { Id = x.IdHtInstallationNetworkType, x.Name }).Distinct(), loadOptions);
        }

    }
}
