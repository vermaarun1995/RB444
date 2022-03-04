using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Model.ViewModel;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class OtherSettingController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        public OtherSettingController(IRequestServices requestServices, IConfiguration configuration, UserManager<Users> userManager)
        {
            _requestServices = requestServices;
            _configuration = configuration;
            _userManager = userManager;
        }

        #region News settings
        public async Task<ActionResult> News()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllNews", _configuration["ApiKeyUrl"]));
            var newsList = jsonParser.ParsJson<List<News>>(Convert.ToString(commonModel.Data));

            return View(newsList);
        }

        [HttpPost]
        public async Task<ActionResult> AddUpdatedCreateNews(News model)
        {
            CommonReturnResponse commonModel = null;
            try
            {
                commonModel = await _requestServices.PostAsync<News, CommonReturnResponse>(string.Format("{0}OtherSetting/SaveNews", _configuration["ApiKeyUrl"]), model);
                var data = JsonConvert.SerializeObject(commonModel);
                return Json(data);
            }
            catch (Exception ex)
            {
                var data = new CommonReturnResponse()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
                return Json(JsonConvert.SerializeObject(data));
            }
        }

        #endregion

        #region Slider Images settings
        public async Task<ActionResult> SliderImages()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;

            var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllSliders", _configuration["ApiKeyUrl"]));
            var sliderList = jsonParser.ParsJson<List<Slider>>(Convert.ToString(commonModel.Data));
            return View(sliderList);
        }

        public async Task<ActionResult> SaveSliderImages()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> SaveSliderImages(HttpPostedFileBase file)
        //{
        //    // Get login user id
        //    var loginUserId = User.Identity.GetUserId();
        //    if (GetLoginUserRole(loginUserId) != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string ext = Path.GetExtension(file.FileName);
        //            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".svg")
        //            {
        //                string _FileName = Path.GetFileName(file.FileName);
        //                string fileName = DateTime.Now.Minute + DateTime.Now.Second + _FileName;
        //                string filePathDB = "UploadedFiles/" + fileName;
        //                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
        //                file.SaveAs(_path);

        //                SliderImage obj = new SliderImage();
        //                SuperAdminBAL _bal = new SuperAdminBAL();
        //                obj.IsActive = true;
        //                obj.FileName = fileName;
        //                obj.FilePath = filePathDB;
        //                bool isUpdated = _bal.SaveSliderImages(obj);
        //                return RedirectToAction("SliderImages", "Home");
        //            }
        //            else
        //            {
        //                TempData["ErrorMsg"] = "Supported Formate is jpg,jpeg,png and svg.";
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            TempData["ErrorMsg"] = "File not Found.";
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMsg"] = "File upload failed!!";
        //        return View();
        //    }
        //}
        #endregion

        #region Logo Images settings
        public async Task<ActionResult> LogoImages()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllLogo", _configuration["ApiKeyUrl"]));
            var LogoList = jsonParser.ParsJson<List<Logo>>(Convert.ToString(commonModel.Data));
            return View(LogoList);
        }

        public async Task<ActionResult> SaveLogoImages()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            return View();
        }

        //[HttpPost]
        //public async Task<ActionResult> SaveLogoImages(HttpPostedFileBase file)
        //{
        //    // Get login user id
        //    var loginUserId = User.Identity.GetUserId();
        //    if (GetLoginUserRole(loginUserId) != 2)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }
        //    try
        //    {
        //        if (file.ContentLength > 0)
        //        {
        //            string ext = Path.GetExtension(file.FileName);
        //            if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".svg")
        //            {
        //                string _FileName = Path.GetFileName(file.FileName);
        //                string fileName = DateTime.Now.Minute + DateTime.Now.Second + _FileName;
        //                string filePathDB = "UploadedFiles/" + fileName;
        //                string _path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
        //                file.SaveAs(_path);

        //                LogoImage obj = new LogoImage();
        //                SuperAdminBAL _bal = new SuperAdminBAL();
        //                obj.IsActive = true;
        //                obj.FileName = fileName;
        //                obj.FilePath = filePathDB;
        //                bool isUpdated = _bal.SaveLogoImages(obj);
        //                return RedirectToAction("LogoImages", "Home");
        //            }
        //            else
        //            {
        //                TempData["ErrorMsg"] = "Supported Formate is jpg,jpeg,png and svg.";
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            TempData["ErrorMsg"] = "File not Found.";
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["ErrorMsg"] = "File upload failed!!";
        //        return View();
        //    }
        //}
        #endregion


    }
}
