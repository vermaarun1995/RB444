using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Models.Model;

namespace RB444.Admin.Controllers
{
    public class ReportController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;

        public ReportController(IRequestServices requestServices, IConfiguration configuration)
        {
            _requestServices = requestServices;
            _configuration = configuration;
        }
        public async Task<IActionResult> RollingCommision()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;
            CommonReturnResponse commonModel = null;
            List<Model.ViewModel.RollingCommisionVM> rollingCommisionVMs = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(string.Format("{0}Common/GetRollingCommission?PrentId={1}&UserId=13&Type=2", _configuration["ApiKeyUrl"], user.Id));
                if (commonModel.IsSuccess && commonModel.Data != null)
                {
                    rollingCommisionVMs = jsonParser.ParsJson<List<Model.ViewModel.RollingCommisionVM>>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return View(rollingCommisionVMs);
        }
    }
}
