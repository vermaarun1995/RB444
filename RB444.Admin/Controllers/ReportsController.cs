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
using Microsoft.AspNetCore.Authorization;

namespace RB444.Admin.Controllers
{
    [Authorize]
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
            List<AccountStatementVM> accountStatementVM = null;
            try
            {
                var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
                ViewBag.LoginUser = user;

                var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatementForSuperAdmin?AdminId={1}", _configuration["ApiKeyUrl"], user.Id));
                accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception)
            {

                throw;
            }
            return View(accountStatementVM);
        }

        public async Task<ActionResult> ActivityLog()
        {
            List<ActivityLogVM> activityLogVM = null;
            try
            {
                var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
                ViewBag.LoginUser = user;
                var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetActivityLog", _configuration["ApiKeyUrl"]));
                activityLogVM = jsonParser.ParsJson<List<ActivityLogVM>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception)
            {
                throw;
            }

            return View(activityLogVM);
        }
    }
}
