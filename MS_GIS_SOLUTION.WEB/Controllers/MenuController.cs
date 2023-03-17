using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class MenuController : BaseController
    {
        public MenuController(MsGisDbContext db) : base(db)
        {

        }

        public object Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load<ErpMenu>(_db.Asqueryable<ErpMenu>(), loadOptions);
        }

        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var role = _db.Find<ErpMenu>(x => x.Id == key);

            if (role == null)
            {
                role = new ErpMenu();
            }

            JsonConvert.PopulateObject(values, role);

            if (key == 0)
            {
                if (_db.Asqueryable<ErpMenu>().Any(x => x.Name.ToUpper() == role.Name.Trim().ToUpper()))
                {
                    return BadRequest("Eyni adlı menyu bazada mövcuddur");
                }
                else
                {
                    _db.Insert<ErpMenu>(role);
                }
            }
            else
            {
                if (_db.Asqueryable<ErpMenu>().Any(x => x.Name.ToUpper() == role.Name.Trim().ToUpper() && x.Id != role.Id))
                {
                    return BadRequest("Eyni adlı menyu bazada mövcuddur");
                }
                else
                {
                    _db.Update<ErpMenu>(role);
                }

            }

            _db.SaveChanges();
            return Ok();

        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            _db.DeleteById<ErpMenu>(key);

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

        public object LoadActiveOperation(DataSourceLoadOptions loadOptions, int id)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpMenuOperation>().Where(x => x.MenuId == id).Select(x => new { Id = x.Id, ParentId = x.ParentId, OrderNo = x.OrderNo, Name = x.Operation.Name }), loadOptions);
        }

        public object LoadPassivOperation(DataSourceLoadOptions loadOptions, int id)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpOperation>().Where(x => !x.MenuOperation.Select(z => z.MenuId).Contains(id)), loadOptions);
        }

        [HttpPost]
        public IActionResult AddOperationToMenu(string values)
        {
            var menuOperation = new ErpMenuOperation();
            JsonConvert.PopulateObject(values, menuOperation);
            _db.Insert(menuOperation);
            _db.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public IActionResult ChangeOrder(string data)
        {

            var items = new List<ReOrderMenuItems>();

            JsonConvert.PopulateObject(data, items);

            foreach (var item in items)
            {
                var uir = _db.Find<ErpMenuOperation>(x => x.Id == item.Id);
                if (uir != null)
                {
                    uir.OrderNo = item.OrderNo;
                    uir.ParentId = item.ParentId;
                    _db.Update(uir);
                }
            }

            _db.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IActionResult DeleteMenuOperation(int key)
        {
            _db.DeleteById<ErpMenuOperation>(key, true);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public JsonResult EditOperation(ErpOperation data)
        {
            if (data.Id == 0)
            {
                _db.Insert(data);
            }
            else
            {
                var operation = _db.Find<ErpOperation>(x => x.Id == data.Id);
                operation.CssClass = data.CssClass;
                operation.Description = data.Description;
                operation.Icon = data.Icon;
                operation.Name = data.Name;
                operation.Target = data.Target;
                operation.Url = data.Url;
                operation.Status = data.Status;

                _db.Update(data);
            }

            bool result = true;

            try
            {
                result = _db.SaveChanges() == 1;
            }
            catch
            {
                result = false;
            }

            return Json(data: result);

        }

        public IActionResult EditOperationPartial(int id)
        {
            var op = _db.Find<ErpOperation>(x => x.Id == id);
            if (op == null)
            {
                op = new ErpOperation();
            }

            return View("_EditOperation", op);
        }
    }

    public class ReOrderMenuItems
    {
        public int Id;
        public int OrderNo;
        public int? ParentId;
    }
}
