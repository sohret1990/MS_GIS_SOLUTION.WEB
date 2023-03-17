using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.WEB.Models.FilterVM;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class RiverController : GenericController<ObjRiver>
    {
        public RiverController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Çaylar";
            KeyField = "IdRiver";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ObjRiver>().Select(x => new { Id = x.IdRiver, x.Name }).Distinct(), loadOptions);
        }



        [HttpPost]
        public List<int> Filter(RiverFilterModel data)
        {
            return _db.Asqueryable<ObjRiver>()
                .Where(x =>
                    (x.Name.Contains(data.Name) || data.Name == null) &&

                    (x.Lenght >= data.LenghtMin || data.LenghtMin == null) &&
                    (x.Lenght <= data.LenghtMax || data.LenghtMax == null)
                )
                .Select(x => x.Objectid).Distinct().ToList();
        }

        [HttpPost]
        public Task<LoadResult> LoadFilter(DataSourceLoadOptions loadOptions, string objectIds)
        {
            List<int> ids = new List<int>();
            if (!string.IsNullOrEmpty(objectIds))
            {
                ids = objectIds.Split(",").Select(int.Parse).ToList();
            }
            //
            return DataSourceLoader.LoadAsync(_db.Asqueryable<ObjRiver>().Where(x => (ids.Contains(x.Objectid))), loadOptions);
        }



        [HttpPost]
        public object Get(int id)
        {
            return _db.Asqueryable<ObjRiver>().Where(x => x.IdRiver == id).ToList().Select(x => new
            {
                x.IdRiver,
                Name = x.Name ?? "məlumat yoxdur",
                Lenght = x.Lenght ?? 0
            }).FirstOrDefault();
        }
    }
}
