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
    public class LakeController : GenericController<ObjLake>
    {
        public LakeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Göllər";
            KeyField = "IdLake";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ObjLake>().Select(x => new { Id = x.IdLake, x.Name }), loadOptions);
        }


        [HttpPost]
        public List<int> Filter(LakeFilterModel data)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            return _db.Asqueryable<ObjLake>()
                .Where(x =>
                userRegionIds.Contains((int)x.FkIdAdminUnit)&&
                    (x.Name.Contains(data.name) || data.name == null) &&
                    (data.fk_id_admin_unit == null || data.fk_id_admin_unit.Contains(x.FkIdAdminUnit)) &&

                    (x.Volume >= data.VolumeMin || data.VolumeMin == null) &&
                    (x.Volume <= data.VolumeMax || data.VolumeMax == null)
                )
                .Select(x => x.Objectid).Distinct().ToList();
        }

        [HttpPost]
        public Task<LoadResult> LoadFilter(DataSourceLoadOptions loadOptions, string objectIds)
        {
            var userRegionIds = UserRegions.Select(x => x.Id).ToList();
            List<int> ids = new List<int>();
            if (!string.IsNullOrEmpty(objectIds))
            {
                ids = objectIds.Split(",").Select(int.Parse).ToList();
            }
            //
            return DataSourceLoader.LoadAsync(_db.Asqueryable<ObjLake>().Where(x => userRegionIds.Contains((int)x.FkIdAdminUnit) && (ids.Contains(x.Objectid))), loadOptions);
        }


        [HttpPost]
        public object Get(int id)
        {
            return _db.Asqueryable<ObjLake>().Where(x => x.IdLake == id).ToList().Select(x => new
            {
                x.IdLake,
                Name = x.Name ?? "məlumat yoxdur",
                AdminUnit = _db.Asqueryable<SAdminUnit>().FirstOrDefault(z => z.IdAdminUnit == (int?)x.FkIdAdminUnit)?.Name ?? "məlumat yoxdur",
                Volume = x.Volume ?? 0,
            }).FirstOrDefault();
        }

    }
}
