using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.Security;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        public MsGisDbContext _db;
        public SUser _currentUser;
        static List<string> operationList;
        internal static string ThemeName;
        public List<IdNameHelper> UserLayers;
        public List<IdNameHelper> UserRegions;
        public List<string> _OperationList
        {
            get
            {
                return operationList;
            }
        }

        public BaseController(MsGisDbContext db)
        {
            _db = db;
            operationList = operationList ?? db.Asqueryable<ErpOperation>().Select(x => x.Url).ToList();
        }

        public string PageTitle { get; set; }
        public string KeyField { get; set; }

        public virtual IActionResult Index()
        {
            return View();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _currentUser = _db.Find<SUser>(x => x.IdUser == (IdentityUser.IdentitiedUser == null ? 0 : IdentityUser.IdentitiedUser.IdUser));
            _db._currentUserId = _currentUser == null ? 0 : _currentUser.IdUser;
            _db._currentUserName = _currentUser == null ? "" : _currentUser.UserName;

            if (string.IsNullOrEmpty(ThemeName))
            {
                ThemeName = _db.Find<ErpSettings>(x => x.Code == "#APPLICATION_THEME#" && x.ErpSettingsGroup.Code == "#ERP#")?.Value ?? "dx.light.compact.css";
            }

            var layerIds = _db.Asqueryable<ErpUserLayer>().Where(x => x.UserId == _db._currentUserId).Select(x => x.LayerId).ToList();
            UserLayers= ArcGisServiceList.Layers.Where(x => layerIds.Contains(x.Id)).ToList();
            ViewData["Layers"] = UserLayers;
            ViewData["UserAdminUnits"] = UserRegions = _db.Asqueryable<ErpViewUserAdminUnit>().Select(x=>new IdNameHelper{ Id = x.Id, Name = x.Name }).ToList();

            ViewData["ApplicationTheme"] = ThemeName;
            ViewData["operationList"] = _OperationList;
            ViewData["OrganizationName"] = OrganizationName;
            ViewData["ApplicationName"] = ApplicationName;
            ViewData["ApplicationLogoSM"] = ApplicationLogoSM;
            ViewData["ApplicationLogoMD"] = ApplicationLogoMD;
            ViewData["ApplicationLogoDark"] = ApplicationLogoDark;
            ViewData["PageTitle"] = PageTitle;
            ViewData["KeyField"] = KeyField;

            base.OnActionExecuting(context);
        }


        public string ApplicationName
        {
            get
            {
                IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
                return configuration.GetSection("ApplicationName").Value;
            }
        }

        public string OrganizationName
        {
            get
            {
                IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
                return configuration.GetSection("OrganizationName").Value;
            }
        }

        public string ApplicationLogoSM
        {
            get
            {
                IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
                return configuration.GetSection("Logo").GetSection("sm").Value;
            }
        }

        public string ApplicationLogoMD
        {
            get
            {
                IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
                return configuration.GetSection("Logo").GetSection("md").Value;
            }
        }

        public string ApplicationLogoDark
        {
            get
            {
                IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();
                return configuration.GetSection("Logo").GetSection("dark").Value;
            }
        }
    }
}
