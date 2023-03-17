using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class AppointmentGroupController : GenericController<SAppointmentGroup>
    {
        public AppointmentGroupController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Təyinat qrupları";
            KeyField = "IdAppointmentGroup";
        }
    }
}
