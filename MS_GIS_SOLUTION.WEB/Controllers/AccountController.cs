using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.Security;
using MS_GIS_SOLUTION.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public class AccountController : BaseController
    {
        IConfiguration _configuration;
        IHostingEnvironment _hostingEnvironment;
        public AccountController(MsGisDbContext db, IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(db)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewData["ReturnUrl"] = ReturnUrl;
            var user = new SUser();
            return View(user);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(string Username, string Password, string ReturnUrl)
        {
            var user = _db.Asqueryable<SUser>().Include(x => x.UserInRole).FirstOrDefault(x => x.UserName == Username);
            var result = DoLogin(Username, Password);

            if (!result)
            {
                ModelState.AddModelError("Password", "İstifadəçi adı və ya şifrə səhvdir!");
                ViewBag.Message = "İstifadəçi adı və ya şifrə səhvdir!";
                return View();
            }

            //ErpEmployee employee = _db.Asqueryable<ErpEmployee>().First(x => x.Id == user.EmployeeId);
            //ErpPerson person = _db.Asqueryable<ErpPerson>().First(x => x.Id == employee.PersonId);

            var roles = _db.Asqueryable<ErpRole>().Where(x => x.UserInRole.Where(z => z.UserId == user.IdUser).Select(x => x.RoleId).Contains(x.Id)).ToList();
            List<int> menuIds = _db.Asqueryable<ErpRole>(x => x.UserInRole.Select(z => z.UserId).Contains(user.IdUser)).SelectMany(x => x.MenuInRole).Select(x => x.Menu.Id).ToList();

            var defaultOperation = _db.Find<ErpOperation>(x => x.Id == user.DefaultOperationId);

            HttpContext.Session.SetInt32("UserId", user.IdUser);

            List<Claim> claims = new List<Claim>
            {
                new Claim("FullName",user.UserFullName??""),
                //new Claim("OrganizationId", $"{_db.Asqueryable<ErpEmployee>(x=>x.Id == employee.Id).Include(x=>x.DepartmentNavigation).First().DepartmentNavigation?.OrganizationId}"),
                new Claim("ImagePath", $"{user.ImagePath}"),
                new Claim("UserId", user.IdUser.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("DefaultOperationId", $"{user.DefaultOperationId}"),
                new Claim("MenuIds", string.Join(',', menuIds)),
                new Claim("Username", user.UserName),
                new Claim("DefaultOperationUrl", defaultOperation!=null?defaultOperation.Url:"/"),
               new Claim("Roles", string.Join(',', roles.Select(x=>x.Code).ToList()))
            };

            //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity)).Wait();

            //Telegram bot helper
            //TelegramBotHelper botHelper = new TelegramBotHelper(_configuration);
            //await botHelper.SendLoginMessage(person);

            //System login log
            var features = HttpContext.Features.Get<IHttpConnectionFeature>();
            ErpUserActionLog actionLog = new ErpUserActionLog();
            actionLog.LogonUserId = user.IdUser;
            actionLog.IpAddress = Response.HttpContext.Connection.RemoteIpAddress.ToString();
            actionLog.Status = 1;
            actionLog.LoginDateTime = DateTime.Now;

            var headers = this.Request.Headers;
            actionLog.Browser = Request.Headers["User-Agent"].ToString();

            _db.Insert(actionLog);
            _db.SaveChanges();



            Response.Cookies.Append("arcgistoken", GenerateToken());


            if (!string.IsNullOrEmpty(ReturnUrl) && ReturnUrl != "/")
            {
                return Redirect(ReturnUrl);
            }

            if (defaultOperation != null)
            {
                return Redirect(defaultOperation.Url);
            }

            return Redirect("/");
        }


        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return View("Login");
        }

        private bool DoLogin(string Username, string Password)
        {
            if (Username != null && Password != null)
            {
                var user = _db.Find<SUser>(x => x.UserName == Username);

                if (user != null && ErpPasswordGenerator.MD5Generator(Password) == user.Password)
                {
                    return true;
                }
            }

            return false;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private string GenerateToken()
        {
            string token = string.Empty;
            try
            {

                var arcSettings = _configuration.GetSection("ArcgisSettings").Get<ArcGisSettingsHelper>();

                WebClient client = new WebClient();
                var collection = new NameValueCollection();
                collection["f"] = arcSettings.Format;
                collection["expiration"] = arcSettings.Expiration.ToString();
                collection["username"] = arcSettings.Username;
                collection["password"] = arcSettings.Password;
                collection["referer"] = arcSettings.Referrer;
                collection["client"] = arcSettings.Client;

                var obj = new JsonTokenResult();
                token = Encoding.UTF8.GetString(client.UploadValues(arcSettings.GenerateTokenUrl, "POST", collection));
                //WriteToFile(token);

                //JsonConvert.PopulateObject(token, obj);
                //token = obj.token;

            }
            catch(Exception ex)
            {
                //if (ex.InnerException!=null)
                //{
                //    WriteToFile(ex.InnerException.Message);
                //}
                //else
                //{
                //    WriteToFile(ex.Message);
                //}
            }

            return token;
        }

        private void WriteToFile(string message)
        {
            string dir = Path.Combine( _hostingEnvironment.WebRootPath, "log");
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            message = $"{DateTime.Now}: {message}";
            using (FileStream fs = new FileStream( Path.Combine(dir,"msgislog.txt"), FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                fs.Write(buffer, 0, buffer.Length);
            }
        }

    }

    class JsonTokenResult
    {
        public bool ssl { get; set; }
        public string token { get; set; }

    }
}
