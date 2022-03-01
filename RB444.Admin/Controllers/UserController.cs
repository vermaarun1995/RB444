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
    public class UserController : Controller
    {
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly UserManager<Users> _userManager;

        public UserController(IRequestServices requestServices, IConfiguration configuration, UserManager<Users> userManager)
        {
            _requestServices = requestServices;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> Users()
        {
            var contextUser = HttpContext.User;
            CommonReturnResponse commonModel = null;
            List<UserRoles> userRoles = null;
            List<Users> users = null;
            try
            {
                var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetUserRoles", _configuration["ApiKeyUrl"]));
                userRoles = jsonParser.ParsJson<List<UserRoles>>(Convert.ToString(commonModel.Data));

                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetAllUsers", _configuration["ApiKeyUrl"]));
                users = jsonParser.ParsJson<List<Users>>(Convert.ToString(commonModel.Data));

                ViewBag.UserRoles = userRoles;
                ViewBag.LoginUser = loginUser;
                ViewBag.Users = users;
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View();
        }
       
    }
}
