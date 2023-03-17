using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ProtectionTypeController : GenericController<SProtectionType>
    {
        public ProtectionTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Mühafizə növləri";
            KeyField = "IdProtectionType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SProtectionType>().Select(x => new { Id = x.IdProtectionType, x.Name }).Distinct(), loadOptions);
        }
    }
}
