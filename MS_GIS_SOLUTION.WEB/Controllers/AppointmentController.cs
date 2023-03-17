using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.DevExtremeControlLibrary;
using MS_GIS_SOLUTION.Entities;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class AppointmentController : GenericController<SAppointment>
    {
        public AppointmentController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Təyinatlar";
            KeyField = "IdAppointment";
            _comboColumns.Add(new GridComboColumn
            {
                ColumnName = "FkIdAppointmentGroup",
                DisplayField = "Name",
                Key = "IdAppointmentGroup",
                LoadAction = "Load",
                LoadController = "AppointmentGroup",
                ValueField = "IdAppointmentGroup"
            });
        }


        public virtual LoadResult LoadIdName(DataSourceLoadOptions loadOptions, int groupId)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SAppointment>().Where(x=>x.FkIdAppointmentGroup == groupId || groupId == 0).Select(x => new { Id = x.IdAppointment, x.Name }).Distinct(), loadOptions);
        }
    }
}
