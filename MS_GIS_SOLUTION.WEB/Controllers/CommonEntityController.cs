using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.Entities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class CommonEntityController : BaseController
    {
        IConfiguration _configuration;
        IHttpContextAccessor _httpContextAccessor;
        public CommonEntityController(MsGisDbContext db, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(db)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public object GetGlobalList(int typeId)
        {
            return _db.Asqueryable<ErpGlobalList>().Where(x => x.TypeId == typeId && x.Status == 1).Select(x => new { Id = x.Id, Name = x.Name }).ToList();
        }

        public object GetUnits(int typeId)
        {
            return _db.Asqueryable<ErpUnit>().Where(x => x.UnitTypeId == typeId && x.Status == 1).Select(x => new { Id = x.Id, Name = x.Name }).ToList();
        }

        public IActionResult Search(string data = " ")
        {
            data = data.ToLower();
            var list = _db.Asqueryable<ErpOperation>()
                .Where(x => x.MenuOperation.SelectMany(z => z.Menu.MenuInRole)
                .SelectMany(m => m.Role.UserInRole)
                .Select(k => k.UserId).Any())
                .Select(x => new { label = x.Name, value = x.Url, desc = x.Description ?? "", icon = x.Icon ?? "" })
                .ToList();

            return Json(list);
        }


        public ActionResult CldrData()
        {
            return new CldrDataScriptBuilder()
                .SetCldrPath("~/wwwroot/js/devextreme/cldr")
                .SetInitialLocale("az")
                .UseLocales("az")
                .Build();
        }

        public JsonResult LoadTourSteps(string url)
        {
            var data = _db.Asqueryable<ErpPageTourStep>().Where(x => x.Status == 1 && url.Contains(x.PageUrl)).OrderBy(x => x.OrderNumber)
                .Select(x => new
                {
                    title = x.Title,
                    content = x.Content,
                    target = x.Target,
                    placement = x.Placement,
                    onNext = x.OnNextCallbackFunc,
                    onPrev = x.OnPrevCallbackFunc,
                    orderNumber = x.OrderNumber
                }).OrderBy(x => x.orderNumber).ToList();
            return Json(data);
        }

        public object LoadArcgisFilterProps(string entityName)
        {
            var entity = Type.GetType("MS_GIS_SOLUTION.Entities." + entityName);
            var props = entity.GetProperties().Cast<PropertyInfo>().Where(x => x.GetCustomAttribute<ArcgisFilterAttribute>() != null).ToList();

            return Json(props);
        }

        public object LoadDbColumnName(string entityName)
        {
            List<string> FUNCTIONAL_COLUMNS = "GlobalID,fk_id_user,insert_date,insert_user,is_deleted,update_date,update_user".Split(',').ToList();
            var entity = Type.GetType($"MS_GIS_SOLUTION.Entities.{entityName}, MS_GIS_SOLUTION.Entities");
            var props = entity.GetProperties().Cast<PropertyInfo>().ToList();

            var etype = ((RelationalModel)((Model)_db.GetModel().Model).RelationalModel).Tables.First().Value.EntityTypeMappings.First().EntityType;
            var type = ((RelationalModel)((Model)_db.GetModel().Model).RelationalModel).Tables.First(x => x.Value.EntityTypeMappings.First().EntityType.Name == entity.FullName);
            var columns = type.Value.Columns.Where(x => !FUNCTIONAL_COLUMNS.Contains(x.Key)).Select(x => new { x.Key, x.Value.PropertyMappings.First().Property.Name }).ToList();

            return Json(columns);
        }

        public List<IdNameHelper> GetUserLayers()
        {
            return UserLayers;
        }

    }
}
