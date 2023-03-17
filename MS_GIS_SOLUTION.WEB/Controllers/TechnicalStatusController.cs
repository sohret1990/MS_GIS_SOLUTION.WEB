using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class TechnicalStatusController : GenericController<STechnicalStatus>
    {
        public TechnicalStatusController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Texniki vəziyyətlər";
            KeyField = "IdTechnicalStatus";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<STechnicalStatus>().Select(x => new { Id = x.IdTechnicalStatus, x.Name }).Distinct(), loadOptions);
        }
    }
}
