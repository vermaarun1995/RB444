using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
            //SuperAdminBAL getNewsDetails = new SuperAdminBAL();
            // getNewsDetails.GetNewsDetails()
            return View();
        }

        public async Task<ActionResult> ChangeSts(int id)
        {
            // Get login user id
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;
            try
            {
                //SuperAdminBAL _bal = new SuperAdminBAL();
                //bool isUpdated = _bal.ChangeNewsSts(id);
                return RedirectToAction("News", "OtherSetting");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ActionResult> CreateNews()
        {
            // Get login user id
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;

            return View();
        }

        [HttpPost]
        public ActionResult CreateNews(News obj)
        {
            try
            {
                bool isName = obj.NewsText != null && obj.NewsText.Trim() != null ? true : false;
                if (isName)
                {
                    //SuperAdminBAL _bal = new SuperAdminBAL();
                    //obj.IsDeleted = false;
                    //bool isUpdated = _bal.PostNews(obj);
                    return RedirectToAction("News", "OtherSetting");
                }
                else
                {
                    TempData["ErrorMsg"] = "Please Fill News Content.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Slider Images settings
        public async Task<ActionResult> SliderImages()
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;

            //SuperAdminBAL getImagesDetails = new SuperAdminBAL();
            //getImagesDetails.GetSliderImagesDetails()
            return View();
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
            //SuperAdminBAL getLogoDetails = new SuperAdminBAL();
            //getLogoDetails.GetLogoImagesDetails()
            return View();
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
