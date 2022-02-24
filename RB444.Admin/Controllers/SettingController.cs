
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class SettingController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;

        public SettingController(IRequestServices requestServices, IConfiguration configuration)
        {
            _requestServices = requestServices;
            _configuration = configuration;
        }
        public IActionResult SportsSetting()
        {
            return View();
        }

        public IActionResult SeriesSetting()
        {
            return View();
        }

        public IActionResult MatchSetting()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> SliderSetting()
        {
            CommonReturnResponse commonModel = null;
            List<Slider> sliderList = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllSliders", _configuration["ApiKeyUrl"]));
                sliderList = jsonParser.ParsJson<List<Slider>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View(sliderList);
        }

        [HttpGet]
        public async Task<ActionResult> LogoSetting()
        {
            CommonReturnResponse commonModel = null;
            List<Logo> logoList = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllLogo", _configuration["ApiKeyUrl"]));
                logoList = jsonParser.ParsJson<List<Logo>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View(logoList);
        }

        [HttpGet]
        public async Task<ActionResult> NewsSetting()
        {
            CommonReturnResponse commonModel = null;
            List<News> newsList = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAllNews", _configuration["ApiKeyUrl"]));
                newsList = jsonParser.ParsJson<List<News>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View(newsList);
        }
    }
}
