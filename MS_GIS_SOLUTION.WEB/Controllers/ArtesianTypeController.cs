using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using System.Linq;


namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ArtesianTypeController : GenericController<SArtesianType>
    {
        public ArtesianTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Artezian növləri";
            KeyField = "IdArtesianType";
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SArtesianType>().Select(x => new { Id = x.IdArtesianType, x.Name }).Distinct(), loadOptions);
        }
    }
}
