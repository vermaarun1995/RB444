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
using Microsoft.AspNetCore.Http;

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

        [HttpPost]
        public async Task<ActionResult> AddUpdatedSliderImages()
        {
            CommonReturnResponse commonModel = null;
            Slider slider = new Slider();
            try
            {
                bool status = Convert.ToBoolean(Request.Form["Status"]);
                var file = Request.Form != null && Request.Form.Files.Count > 0 ? Request.Form.Files : null;

                slider.Status = status;

                commonModel = await _requestServices.PostAsync<Slider, CommonReturnResponse>(string.Format("{0}OtherSetting/SaveSlider", _configuration["ApiKeyUrl"]), new Slider());
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
            Logo logo = new Logo();
            try
            {
                bool status = Convert.ToBoolean(Request.Form["Status"]);
                var file = Request.Form !=null && Request.Form.Files.Count > 0 ? Request.Form.Files : null;

                logo.Status = status;
             
                commonModel = await _requestServices.PostAsync<Logo, CommonReturnResponse>(string.Format("{0}OtherSetting/SaveLogo", _configuration["ApiKeyUrl"]), new Logo());
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


    }
}
