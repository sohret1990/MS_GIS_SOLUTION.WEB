using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class LayersController : GenericController<SdeLayer>
    {
        public LayersController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Laylar";
            KeyField = "LayerId";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SdeLayer>().Select(x => new { Id = x.LayerId, x.TableName }), loadOptions);
        }

    }
}
