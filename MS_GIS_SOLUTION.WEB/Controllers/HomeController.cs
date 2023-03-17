using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.Security;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class HomeController : BaseController
    {
        IHttpContextAccessor _httpContextAccessor;
        public HomeController(MsGisDbContext db, IHttpContextAccessor httpContextAccessor) : base(db)
        {
            _httpContextAccessor = httpContextAccessor;
            PageTitle = "Meliorasiya və su təsərrüfatı obyektlərinin elektron coğrafi informasiya sistemi";
        }

        public IActionResult Dashboard()
        {
            ViewData["CHANNEL_CHART_DATA"] = _db.Asqueryable<ViewObj01ChannelForDashboard>().ToList();
            ViewData["CHANNEL_CHART_DATA2"] = _db.Asqueryable<ViewObj01ChannelForDashboard2>().ToList();
            ViewData["CHANNEL_CHART_DATA3"] = _db.Asqueryable<ViewObj01ChannelForDashboard3>().ToList();
            ViewData["CHANNEL_CHART_DATA4"] = _db.Asqueryable<ViewObj01ChannelForDashboard4>().ToList();
            ViewData["LEFT_INFO"] = _db.Asqueryable<ViewDashboardLeftSide>().ToList();
            return View();
        }
    }
}
