﻿using Microsoft.AspNetCore.Identity;
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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace RB444.Admin.Controllers
{
    public class OtherSettingController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public OtherSettingController(IRequestServices requestServices, IConfiguration configuration, UserManager<Users> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _requestServices = requestServices;
            _configuration = configuration;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
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

        [HttpPost]
        public async Task<ActionResult> AddUpdatedSliderImages()
        {
            CommonReturnResponse commonModel = null;
            Slider model = new Slider();
            try
            {
                bool status = Convert.ToBoolean(Request.Form["Status"]);
                var file = Request.Form != null && Request.Form.Files.Count > 0 ? Request.Form.Files : null;

                model.FileName = UploadedFile(file, "SliderImages");
                model.FilePath = "SliderImages/" + model.FileName;
                model.Status = status;

                commonModel = await _requestServices.PostAsync<Slider, CommonReturnResponse>(string.Format("{0}OtherSetting/SaveSlider", _configuration["ApiKeyUrl"]), model);
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

        [HttpPost]
        public async Task<ActionResult> AddUpdatedLogoImages()
        {
            CommonReturnResponse commonModel = null;
            Logo model = new Logo();
            try
            {
                bool status = Convert.ToBoolean(Request.Form["Status"]);
                var file = Request.Form !=null && Request.Form.Files.Count > 0 ? Request.Form.Files : null;

                model.FileName = UploadedFile(file, "LogoImages");
                model.FilePath = "LogoImages/" + model.FileName;
                model.Status = status;
             
                commonModel = await _requestServices.PostAsync<Logo, CommonReturnResponse>(string.Format("{0}OtherSetting/SaveLogo", _configuration["ApiKeyUrl"]), model);
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


        private string UploadedFile(IFormFileCollection imageFile, string folderName)
        {
            string uniqueFileName = null;

            if (imageFile != null)
            {
                foreach (var item in imageFile)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + item.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        item.CopyTo(fileStream);
                    }
                }
            }
            return uniqueFileName;
        }
    }
}
