using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.Security;
using MS_GIS_SOLUTION.WEB.Models;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace MS_GIS_SOLUTION.WEB.Components
{
    public class LeftMenuViewComponent : ViewComponent
    {
        public LeftMenuVM LeftMenuItems { get; set; }
        IHttpContextAccessor _contextAccessor;
        public LeftMenuViewComponent(MsGisDbContext db, IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            BinaryFormatter bf = new BinaryFormatter();
            LeftMenuItems = new LeftMenuVM();

            byte[] data = _contextAccessor.HttpContext.Session.Get("LeftMenu");

            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    LeftMenuItems = (LeftMenuVM)bf.Deserialize(ms);
                    return;
                }
            }

            SUser user = IdentityUser.IdentitiedUser;

            var roles = db.Asqueryable<ErpUserInRole>()
                .Include(x => x.Role)
                .Where(x => x.IsDeleted == 0 && x.Role.IsDeleted == 0 && x.UserId == user.IdUser && x.Status == 1).Select(x => x.RoleId).ToList();

            LeftMenuItems.Menu = db.Asqueryable<ErpMenuInRole>().Include(x => x.Menu)
                .Where(x => x.Status == 1 && x.Menu.IsDeleted == 0 && x.IsDeleted == 0 && roles.Contains(x.RoleId))
                .OrderBy(x => x.OrderNumber)
                .Select(x => x.Menu).ToList();

            var menus = LeftMenuItems.Menu.Where(x => x.IsDeleted == 0).Select(x => x.Id).ToList();

            LeftMenuItems.MenuOperation = db.Asqueryable<ErpMenuOperation>()
                .Where(x => x.Status == 1 && menus.Contains(x.MenuId))
                .OrderBy(x => x.OrderNo)
                .Include(x => x.Operation)
                .Include(x => x.Menu)
                .ToList();


            var operations = LeftMenuItems.MenuOperation.Select(x => x.Id).ToList();

            LeftMenuItems.Operation = db.Asqueryable<ErpOperation>().Where(x => x.Status == 1 && operations.Contains(x.Id)).ToList();

            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, LeftMenuItems);
                _contextAccessor.HttpContext.Session.Set("LeftMenu", ms.GetBuffer());
            }
        }

        public IViewComponentResult Invoke()
        {
            return View("Default", LeftMenuItems);
        }
    }
}