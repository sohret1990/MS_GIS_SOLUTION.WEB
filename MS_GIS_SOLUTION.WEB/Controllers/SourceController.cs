using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class SourceController : GenericController<ObjViewSource>
    {
        public SourceController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Sugötürmə mənbələri";
            KeyField = "Id";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions, string sourceType)
        {
            // return DataSourceLoader.Load(new List<IdNameHelper>(), loadOptions);

            List<int> sourceTypeIds = new List<int>();
            if (!string.IsNullOrEmpty(sourceType))
                sourceTypeIds = sourceType.Split(',').Select(int.Parse).ToList();

            // return DataSourceLoader.Load(sourceTypeIds, loadOptions);

            return DataSourceLoader.Load(_db.Asqueryable<ObjViewSource>().Where(x => (string.IsNullOrEmpty(sourceType) || sourceTypeIds.Contains((int)x.Type))).Select(x => new { Id = x.Id, x.Name }), loadOptions);
        }

        public virtual LoadResult LoadIdNameSingle(DataSourceLoadOptions loadOptions, string sourceType)
        {
            if (sourceType != null)
            {
                return DataSourceLoader.Load(_db.Asqueryable<ObjViewSource>().Where(x => x.Type == int.Parse(sourceType)).Select(x => new { Id = x.Id, x.Name }), loadOptions);
            }
            else
            {
                return DataSourceLoader.Load(_db.Asqueryable<ObjViewSource>().Where(x => x.Type == null).Select(x => new { Id = x.Id, x.Name }), loadOptions);
            }
        }
    }
}
