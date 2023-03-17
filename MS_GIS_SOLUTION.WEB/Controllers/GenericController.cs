using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.DevExtremeControlLibrary;
using MS_GIS_SOLUTION.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class GenericController<T> : BaseController where T : GisBaseEntity
    {
        public List<GridComboColumn> _comboColumns;

        public GenericController(MsGisDbContext db) : base(db)
        {
            _comboColumns = new List<GridComboColumn>();
            PageTitle = "Mənbə tipləri";
            KeyField = "IdSourceType";
        }


        public override IActionResult Index()
        {

            //DataGridBuilder<T> dgb = factory.DataGrid<T>();

            PropertyInfo[] properties = typeof(T).GetProperties().Where(x => x.GetCustomAttribute<GridColumnAttribute>() != null).ToArray();
            ViewData["Properties"] = properties;
            StringBuilder sb = new StringBuilder("[");

            foreach (PropertyInfo property in properties)
            {
                var displayName = property.Name;
                if (property.GetCustomAttribute<DisplayNameAttribute>() != null)
                {
                    displayName = property.GetCustomAttribute<DisplayNameAttribute>().DisplayName;
                }

                GridColumnDataType dataType = GridColumnDataType.String;
                switch (property.PropertyType.FullName)
                {
                    case "System.Int32":
                    case "System.Byte":
                        dataType = GridColumnDataType.Number;
                        break;
                    case "System.DateTime":
                        dataType = GridColumnDataType.Date;
                        break;
                    default:
                        break;
                }


                //dgb.Columns(columns =>
                //{
                //    columns.Add().DataField(property.Name).Caption(displayName).DataType(dataType);
                //});




                //sb.Append("{ dataField: \"" + property.Name + "\", caption: \"" + displayName + "\"},");
            }

            //dgb.Configure<T>(RouteData.Values["controller"].ToString()).ConfigureGridExport(fileName: RouteData.Values["controller"].ToString());

            sb.Append("]");

            ViewData["PageTitle"] = PageTitle;
            ViewData["Columns"] = sb.ToString();
            ViewData["KeyField"] = KeyField;
            ViewData["ComboColumns"] = _comboColumns;
            return View();
        }

        [HttpPut]
        public virtual IActionResult Edit(int key, string values)
        {
            var t = typeof(T);
            T data = _db.SelectById<T>(key);

            if (data == null && key > 0) return BadRequest("Əməliyyat zamanı xəta baş verdi!");

            if (data == null)
            {
                data = Activator.CreateInstance<T>();
            }

            JsonConvert.PopulateObject(values, data);

            //if (!EntityValidator.ValidateData(data, _db.GetModel().Database.GetDbConnection()))
            //    return BadRequest("Əməliyyat zamanı xəta baş verdi!");

            if (key == 0)
            {
                _db.Insert(data);
            }
            else
            {
                _db.Update(data);
            }

            bool result = _db.SaveChanges() > 0;

            if (result)
                return Ok();
            else
                return BadRequest("Əməliyyat zamanı xəta baş verdi!");
        }

        public virtual LoadResult Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<T>(), loadOptions);
        }

        [HttpDelete]
        public virtual IActionResult Delete(int key)
        {
            bool result = true;
            _db.DeleteById<T>(key);
            result = _db.SaveChanges() > 0;

            if (result)
                return Ok();
            else
                return BadRequest("Əməliyyat zamanı xəta baş verdi!");
        }
    }
}
