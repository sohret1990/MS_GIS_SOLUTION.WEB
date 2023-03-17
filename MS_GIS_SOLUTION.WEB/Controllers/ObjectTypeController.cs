using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.DevExtremeControlLibrary;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class ObjectTypeController : GenericController<SObjectType>
    {
        public ObjectTypeController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Obyekt tipləri";
            KeyField = "IdObjectType";
            _comboColumns.Add(new GridComboColumn
            {
                ColumnName = "FkIdObjectTypeGroup",
                DisplayField = "Name",
                Key = "IdObjectTypeGroup",
                LoadAction = "Load",
                LoadController = "ObjectTypeGroup",
                ValueField = "IdObjectTypeGroup"
            });
        }

        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SObjectType>().Select(x => new { Id = x.IdObjectType, x.Name }).Distinct(), loadOptions);
        }

        public virtual LoadResult LoadIdNameByGroup(DataSourceLoadOptions loadOptions, int groupId)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SObjectType>().Where(x => x.FkIdObjectTypeGroup == groupId).Select(x => new { Id = x.IdObjectType, x.Name }).Distinct(), loadOptions);
        }
    }
}
