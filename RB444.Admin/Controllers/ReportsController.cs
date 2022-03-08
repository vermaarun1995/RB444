using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RB444.Model.ViewModel;
using Newtonsoft.Json;

namespace RB444.Admin.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;
        public ReportsController(IRequestServices requestServices, IConfiguration configuration, UserManager<Users> userManager)
        {
            _requestServices = requestServices;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<ActionResult> AccountStatement()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;

            var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatement?UserId={1}", _configuration["ApiKeyUrl"], user.Id));
            var accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));

            return View(accountStatementVM);
        }

        public async Task<ActionResult> ActivityLog()
        {
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = user;
            var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetActivityLog", _configuration["ApiKeyUrl"]));
            var activityLogVM = jsonParser.ParsJson<List<ActivityLogVM>>(Convert.ToString(commonModel.Data));

            return View(activityLogVM);
        }
    }
}
