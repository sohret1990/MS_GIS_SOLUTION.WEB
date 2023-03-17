using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class AdditionalAdminUnitController : GenericController<SAdminUnit>
    {
        public AdditionalAdminUnitController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Yaşayış məntəqələri";
            KeyField = "IdAdditionallAdminUnit";
        }
    }
}
