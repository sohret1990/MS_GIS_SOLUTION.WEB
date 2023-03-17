using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.DevExtremeControlLibrary;
using MS_GIS_SOLUTION.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class WaterUseAssociationController : GenericController<SWaterUseAssociation>
    {
        public WaterUseAssociationController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Sudan istifadə birlikləri barədə məlumatlar";
            KeyField = "IdWaterUseAssociation";
            _comboColumns.Add(new GridComboColumn
            {
                ColumnName = "FkIdAdminUnit",
                DisplayField = "Name",
                Key = "IdAdminUnit",
                LoadAction = "Load",
                LoadController = "AdminUnit",
                ValueField = "IdAdminUnit"
            });
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions, List<int> adminUnitIds)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SWaterUseAssociation>().Where(x =>( adminUnitIds.Count == 0 || adminUnitIds.Contains((int)x.FkIdAdminUnit)) && x.IdWaterUseAssociation != null && x.IsDeleted == 0).ToList().Select(x => new { Id = x.IdWaterUseAssociation, x.Name }).Distinct().OrderBy(x => x.Name), loadOptions);
        }

        public virtual LoadResult LoadIdNameByAdminUnit(DataSourceLoadOptions loadOptions, int AdminUnitId)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SWaterUseAssociation>().Where(x => x.IdWaterUseAssociation != null && x.FkIdAdminUnit == AdminUnitId ).ToList().Select(x => new { Id = x.IdWaterUseAssociation, x.Name }).Distinct().OrderBy(x=>x.Name), loadOptions);
        }
    }
}
