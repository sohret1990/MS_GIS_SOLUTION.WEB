using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class MissingSourceTypeController : GenericController<SMissingSourceType>
    {
        public MissingSourceTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Naməlum su mənbələri";
            KeyField = "IdMissingSourceType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SMissingSourceType>().ToList().Select(x => new { Id = x.IdMissingSourceType, x.Name }).Distinct(), loadOptions);
        }
    }
}
