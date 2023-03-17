using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class SourceTypeController : GenericController<SSourceType>
    {
        public SourceTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Sugötürmə mənbə tipləri";
            KeyField = "IdSourceType";

        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SSourceType>().Select(x => new { Id = x.IdSourceType, x.Name }).OrderBy(x => x.Name), loadOptions);
        }
    }
}
