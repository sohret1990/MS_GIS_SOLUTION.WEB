using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MS_GIS_SOLUTION.Core;
using MS_GIS_SOLUTION.Entities;
using MS_GIS_SOLUTION.Security;
using MS_GIS_SOLUTION.WEB.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace MS_GIS_SOLUTION.WEB.Controllers
{
    public enum PageRedirectReason
    {
        ChangePassword = 1,
        OldPasswordIncorrect = 2,
        PasswordAndConfirmError = 3
    }

    public enum ForgoPasswordRequestType
    {
        Security_Code_Request = 1,
        Reset_Password_Request = 2
    }


    public class UsersController : BaseController
    {
        [Obsolete]
        private IHostingEnvironment _env;
        Microsoft.Extensions.Configuration.IConfiguration _configuration;

        [Obsolete]
        public UsersController(MsGisDbContext db, IHostingEnvironment env, Microsoft.Extensions.Configuration.IConfiguration configuration) : base(db)
        {
            _env = env;
            _configuration = configuration;
        }


        public IActionResult CreateEdit(int id = 0)
        {
            var data = _db.Find<SUser>(x => x.IdUser == id);
            UserViewModel uvm = new UserViewModel() { ImagePath = "agro-erp-user-no-image.png" };
            if (data != null)
            {
                uvm.Id = data.IdUser;
                uvm.DefaultOperationId = data.DefaultOperationId;
                uvm.Id = data.IdUser;
                uvm.Mail = data.Mail;
                uvm.Username = data.UserName;
                uvm.UserLastName = data.UserLastName;
                uvm.UserFirstName = data.UserFirstName;
                uvm.UserMiddleName = data.UserMiddleName;
                uvm.ImagePath = data.ImagePath;
                uvm.UserRegionIds = _db.Asqueryable<ErpUserRegion>().Where(x=>x.UserId == id).Select(x => x.RegionId).ToList();
                uvm.UserLayerIds = _db.Asqueryable<ErpUserLayer>().Where(x=>x.UserId == id).Select(x => x.LayerId).ToList();
            }

            return View(uvm);
        }

        [HttpGet]
        public object Load(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SUser>(), loadOptions);
        }

        [HttpDelete]
        public IActionResult Delete(int key)
        {
            if (key == 0)
            {
                return BadRequest();
            }
            try
            {
                _db.DeleteById<SUser>(key);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest("Əlaqəli bağlantı var və öncə onu silməyiniz gərəklidir.");
            }

            return Ok();
        }

        [HttpGet]
        public object LoadUserList(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<SUser>().Select(x => new { Id = x.IdUser, Name = x.UserName }), loadOptions);
        }

        [HttpGet]
        public object LoadOperationList(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_db.Asqueryable<ErpOperation>(x => x.Status == 1 && !x.Url.Contains("#")).Select(x => new { Id = x.Id, Name = x.Name }), loadOptions);
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Save(UserViewModel uvm, IFormFile image)
        {
            if (!ModelState.IsValid && _db.Find<SUser>(x => x.UserName == uvm.Username) != null)
            {
                return View("CreateEdit", uvm);
            }


            var usr = _db.Find<SUser>(x => x.IdUser == uvm.Id);



            if (uvm.Id == 0)
            {
                usr = new SUser()
                {
                    DefaultOperationId = uvm.DefaultOperationId,
                    Mail = uvm.Mail,
                    Password = ErpPasswordGenerator.MD5Generator(uvm.Pass),
                    UserName = uvm.Username,
                    UserLastName = uvm.UserLastName,
                    UserFirstName = uvm.UserFirstName,
                    UserMiddleName = uvm.UserMiddleName,
                    ImagePath = uvm.ImagePath
                };

                _db.Insert(usr);

            }
            else
            {
                usr.DefaultOperationId = uvm.DefaultOperationId;
                usr.Mail = uvm.Mail;
                usr.ImagePath = uvm.ImagePath;
                usr.UserLastName = uvm.UserLastName;
                usr.UserFirstName = uvm.UserFirstName;
                usr.UserMiddleName = uvm.UserMiddleName;
                _db.Update(usr);
            }

            if (image != null)
            {
                string path = _env.WebRootPath + "/Content/uploads/Users/";
                usr.ImagePath = "agro-erp-user-" + usr.IdUser + ".png";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                FileStream fs = new FileStream(Path.Combine(path, usr.ImagePath), FileMode.OpenOrCreate);
                image.CopyTo(fs);
                fs.Flush();
                fs.Close();
                usr = _db.Find<SUser>(x => x.IdUser == usr.IdUser);


                _db.Update(usr);
                _db.SaveChanges();
            }

            #region User Regions area
            uvm.UserRegionIds = _db.Asqueryable<ErpUserRegion>(x => x.UserId == usr.IdUser).Select(x => x.RegionId).ToList();
            if (uvm.UserRegionIdsJson != null)
            {
                JsonConvert.PopulateObject(uvm.UserRegionIdsJson, uvm.UserRegionIds);
                var deletedRegions = _db.Asqueryable<ErpUserRegion>().Where(x => x.UserId == usr.IdUser).ToList();
                _db.DeleteRange(entities: deletedRegions, deleteFromDataBase: true);

                foreach (var item in uvm.UserRegionIds)
                {
                    _db.Insert<ErpUserRegion>(new ErpUserRegion
                    {
                        UserId = usr.IdUser,
                        RegionId = item,
                        Status = 1
                    });
                }
            }
            #endregion

            #region User Layers area
            uvm.UserLayerIds = _db.Asqueryable<ErpUserLayer>(x => x.UserId == usr.IdUser).Select(x => x.LayerId).ToList();
            if (uvm.UserLayerIdsJson != null)
            {
                JsonConvert.PopulateObject(uvm.UserLayerIdsJson, uvm.UserLayerIds);
                var deletedLayers = _db.Asqueryable<ErpUserLayer>().Where(x => x.UserId == usr.IdUser).ToList();
                _db.DeleteRange(entities: deletedLayers, deleteFromDataBase: true);

                foreach (var item in uvm.UserLayerIds)
                {
                    _db.Insert<ErpUserLayer>(new ErpUserLayer
                    {
                        UserId = usr.IdUser,
                        LayerId = item,
                        Status = 1
                    });
                }
            }
            #endregion

            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult ChangePassword(int id, PageRedirectReason reason = PageRedirectReason.ChangePassword)
        {
            var data = _db.Find<SUser>(x => x.IdUser == id);
            UserViewModel uvm = new UserViewModel();
            if (data != null)
            {
                uvm.DefaultOperationId = data.DefaultOperationId;
                uvm.Id = data.IdUser;
                uvm.Mail = data.Mail;
                uvm.Username = data.UserName;
            }
            if (reason == PageRedirectReason.OldPasswordIncorrect)
            {
                ViewData["oldPasswordIncorrect"] = "Köhnə şifrə yanlışdır!";
            }
            else if (reason == PageRedirectReason.PasswordAndConfirmError)
            {
                ViewData["oldPasswordIncorrect"] = "Yeni şifrə ilə təkrarı eyni deyil!";
            }
            return View(uvm);
        }



        [HttpPost]
        [ValidateAntiForgeryToken()]
        public IActionResult ChangePassword(UserViewModel uvm)
        {
            var usr = _db.Find<SUser>(x => x.IdUser == uvm.Id);
            string newPassword = ErpPasswordGenerator.MD5Generator(uvm.Pass);
            string oldPassword = ErpPasswordGenerator.MD5Generator(uvm.OldPassword);
            if (uvm.Pass != uvm.ConfirmPassword)
            {
                return RedirectToAction(nameof(ChangePassword), new { id = uvm.Id, reason = PageRedirectReason.PasswordAndConfirmError });
            }

            if (usr.Password == oldPassword)
            {
                usr.Password = newPassword;
                _db.Update(usr);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewData["oldPasswordIncorrect"] = "Köhnə şifrə yanlışdır!";
                return RedirectToAction(nameof(ChangePassword), new { id = uvm.Id, reason = PageRedirectReason.OldPasswordIncorrect });
            }
        }

        public JsonResult CheckUserName(string UserName)
        {
            var user = _db.Find<SUser>(x => x.UserName.Trim().ToLower() == UserName.Trim().ToLower() && x.IsDeleted == 0);
            return Json(data: new { message = user == null ? "" : "Eyni adlı istifadəçi sistemdə mövcuddur!", status = user == null });
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ViewData["FormTitle"] = "E-poçt ünvanınızı daxil edin!";
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public IActionResult ForgotPassword(string mailOrSecurityCode, ForgoPasswordRequestType requestType = ForgoPasswordRequestType.Security_Code_Request)
        {
            SUser usr;
            EmailSettings emailSettings = _configuration.GetSection("EmailSettings").Get<EmailSettings>();
            string templatePath = Path.Combine(_env.WebRootPath, "MailTemplates", "ForgotPasswordSecurityCodeTemplate.html");
            string loginLink = Request.Host + Url.Action("Login", "Account");

            if (string.IsNullOrEmpty(mailOrSecurityCode))
            {
                ViewData["StatusClass"] = "danger";
                ViewData["ErrorMessage"] = "Cəhdiniz uğursuz alındı";
                return View();
            }

            if (requestType == ForgoPasswordRequestType.Security_Code_Request)
            {
                ViewData["FormTitle"] = "Təhlükəsizlik kodunuzu daxil edin!";
                usr = _db.Find<SUser>(x => x.Mail.ToLower().Trim() == mailOrSecurityCode.ToLower().Trim());

                if (usr != null)
                {
                    _db.Asqueryable<ErpPasswordRecoveryRequest>().Where(x => x.Mail.ToLower().Trim() == mailOrSecurityCode.ToLower().Trim()).ToList().ForEach(x => x.Status = 0);
                    var req = new ErpPasswordRecoveryRequest()
                    {
                        ExpiryDateTime = DateTime.Now.AddMinutes(5),
                        Mail = mailOrSecurityCode.ToLower().Trim(),
                        PasswordHash = ErpPasswordGenerator.Generate(7),
                        Status = 1,
                        Token = ErpPasswordGenerator.Generate(23),
                        UserId = usr.IdUser,
                        IsConfirmed = 0,
                    };

                    _db.Insert(req);
                    _db.SaveChanges();


                    SendMail.SendSecurityCodeMail(emailSettings, templatePath, usr.UserFullName, mailOrSecurityCode, "İtirilmiş şifrənin bərpası", req.PasswordHash, loginLink, "Şifrənizi yeniləmək üçün təhlükəsizlik kodu");
                    ViewData["ErrorMessage"] = "Təhlükəsizlik kodu elektron poçtunuza göndərildi!";
                    ViewData["StatusClass"] = "success";
                    ViewData["ConfirmForgotPassword"] = req.PasswordHash;
                }
                else
                {
                    ViewData["ErrorMessage"] = "Daxil etdiyiniz e-poçt ünvanı sistemdə qeydiyyatda deyil!";
                    ViewData["StatusClass"] = "danger";
                    return View();
                }
            }
            else
            {
                var req = _db.Asqueryable<ErpPasswordRecoveryRequest>().FirstOrDefault(x => x.PasswordHash == mailOrSecurityCode);
                var result = ValidateRecoveryRequest(req);
                if (result.Item1)
                {
                    usr = _db.Find<SUser>(x => x.IdUser == req.UserId);
                    string newPass = ErpPasswordGenerator.Generate(10);

                    usr.Password = ErpPasswordGenerator.MD5Generator(newPass);
                    req.Status = 0;
                    _db.Update(usr);
                    _db.Update(req);

                    _db.SaveChanges();
                    SendMail.SendSecurityCodeMail(emailSettings, templatePath, usr.UserFullName, req.Mail, "İtirilmiş şifrənin bərpası", newPass, loginLink, "Sizin yeni şifrəniz");
                    ViewData["FormTitle"] = "Sistemə daxil olmaq üçün şifrə e-poçt ünvanınıza göndərildi!";
                    ViewData["HideForm"] = true;
                }
                else
                {
                    ViewData["StatusClass"] = "danger";
                    ViewData["ErrorMessage"] = result.Item2;
                    ViewData["ConfirmForgotPassword"] = result;
                }
            }

            return View();
        }


        private Tuple<bool, string> ValidateRecoveryRequest(ErpPasswordRecoveryRequest request)
        {
            var result = new Tuple<bool, string>(true, "");

            if (request == null)
            {
                result = new Tuple<bool, string>(false, "Təhlükəsizlik kodu səhv daxil edilmişdir!");
            }
            else
            {
                if (request.Status == 0)
                {
                    result = new Tuple<bool, string>(false, "Bu təhlükəsizlik kodu artıq istifadə edilmişdir!");
                }

                if (request.ExpiryDateTime < DateTime.Now)
                {
                    result = new Tuple<bool, string>(false, "Bu təhlükəsizlik kodunun etibarlılıq müddəti bitmişdir!");
                }
            }

            return result;
        }

        public IActionResult SmsVerification()
        {
            return View();
        }
    }
}