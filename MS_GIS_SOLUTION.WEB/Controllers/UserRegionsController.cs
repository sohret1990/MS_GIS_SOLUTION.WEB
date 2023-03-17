using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class UserRegionsController : BaseController
    {
        public UserRegionsController(MsGisDbContext db) : base(db)
        {
            PageTitle = "İstifadəçinin rayonları";
        }

        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpUserRegion>(), loadOptions);
        }

        public virtual LoadResult LoadRegions(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SAdminUnit>().Select(x => new
            {
                Id = x.IdAdminUnit,
                Name = x.Name
            }).OrderBy(x => x.Name), loadOptions);
        }

        public virtual LoadResult LoadUserRegions(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(UserRegions, loadOptions);
        }
    }
}
