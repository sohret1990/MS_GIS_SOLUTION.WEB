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
    public class MenuInRoleController : BaseController
    {
        public MenuInRoleController(MsGisDbContext db) : base(db)
        {
            PageTitle = "Rolların menyuları";
        }

        public object Load(DataSourceLoadOptions loadOptions, int RoleId)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpViewMenuInRole>().Where(x => x.RoleId == RoleId).OrderBy(x => x.OrderNumber), loadOptions);
        }

        public object LoadActiveMenu(DataSourceLoadOptions loadOptions, int RoleId)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpViewMenuInRole>().Where(x => x.RoleId == RoleId).OrderBy(x => x.OrderNumber), loadOptions);
        }

        public object LoadPassivMenu(DataSourceLoadOptions loadOptions, int RoleId)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpMenu>().Where(x => (RoleId != 0) && !x.MenuInRole.Select(k => k.RoleId).Contains(RoleId)), loadOptions);
        }


        [HttpPut]
        public IActionResult Edit(int key, string values)
        {
            var uir = new ErpMenuInRole();
            JsonConvert.PopulateObject(values, uir);


            var delete = _db.Asqueryable<ErpMenuInRole>(x => x.MenuId == uir.MenuId && x.RoleId == uir.RoleId && x.IsDeleted == 1).FirstOrDefault();
            if (delete != null)
            {
                _db.Delete(delete);
            }

            _db.Insert(uir);
            _db.SaveChanges();
            return Ok();
        }

        [HttpPost]
        public IActionResult ChangeOrder(Dictionary<int, int> data)
        {
            foreach (var item in data)
            {
                var uir = _db.Find<ErpMenuInRole>(x => x.Id == item.Key);
                if (uir != null)
                {
                    uir.OrderNumber = item.Value;
                    _db.Update(uir);
                }
            }

            _db.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        public IActionResult Delete(int key)
        {
            var d = _db.Find<ErpMenuInRole>(x => x.Id == key);
            _db.DeleteById<ErpMenuInRole>(key, true);
            _db.SaveChanges();
            return Ok();
        }

    }
}
