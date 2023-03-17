using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class RoleController : BaseController
    {
        public RoleController(MsGisDbContext db) : base(db)
        {

        }

        public override IActionResult Index()
        {
            return View();
        }


        public object Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpRole>(), loadOptions);
        }

        public object LoadRole(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpRole>().Where(x => x.Status == 1), loadOptions);
        }

        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var role = _db.Find<ErpRole>(x => x.Id == key);

            if (role == null)
            {
                role = new ErpRole();
            }

            JsonConvert.PopulateObject(values, role);

            if (key == 0)
            {
                if (_db.Asqueryable<ErpRole>().Any(x => x.Name.ToUpper() == role.Name.Trim().ToUpper()))
                {
                    return BadRequest("Eyni adlı rol bazada mövcuddur");
                }
                else
                {
                    _db.Insert<ErpRole>(role);
                }
            }
            else
            {
                if (_db.Asqueryable<ErpRole>().Any(x => x.Name.ToUpper() == role.Name.Trim().ToUpper() && x.Id != role.Id))
                {
                    return BadRequest("Eyni adlı rol bazada mövcuddur");
                }
                else
                {
                    _db.Update<ErpRole>(role);
                }

            }

            _db.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            _db.DeleteById<ErpRole>(key);

            try
            {
                _db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }



        [HttpPut]
        public IActionResult AddMenuToRole(int key, string values)
        {
            return View();
        }

        [HttpPost]
        public IActionResult RemoveMenuFromRole(int roleId, int[] menuIds)
        {
            var mir = _db.Asqueryable<ErpMenuInRole>().Where(x => x.RoleId == roleId && menuIds.Contains(x.MenuId)).ToList();
            _db.DeleteRange(mir);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public JsonResult ReorderMenu(int menuId, int roleId, string dir)
        {
            var data = _db.Find<ErpMenuInRole>(x => x.MenuId == menuId && x.RoleId == roleId);
            if (data != null)
            {
                int order = data.OrderNumber;
                if (dir == "up")
                {
                    order = _db.Asqueryable<ErpMenuInRole>()
                        .Where(x => x.RoleId == data.RoleId && x.OrderNumber < data.OrderNumber)
                        .Select(x => x.OrderNumber)
                        .Max() - 10;
                }
                else
                {
                    order = _db.Asqueryable<ErpMenuInRole>()
                       .Where(x => x.RoleId == data.RoleId && x.OrderNumber > data.OrderNumber)
                       .Select(x => x.OrderNumber)
                       .Min() + 10;
                }

                data.OrderNumber = order;
                _db.Update<ErpMenuInRole>(data);
                _db.SaveChanges();
            }

            return Json(true);
        }

        [HttpGet]
        public object LoadRoleList(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpRole>(x => x.Status == 1).Select(x => new { Id = x.Id, Name = x.Name }), loadOptions);
        }
    }
}
