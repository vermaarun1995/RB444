using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Models.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public ActionResult Login(string returnUrl = null)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(Model.ViewModel.LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return Redirect("/Home/Index");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View(model);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register(Model.ViewModel.RegisterViewModel model)
        {
            CommonReturnResponse commonModel = null;
            try
            {

                if (ModelState.IsValid)
                {
                    //// Get auth key from web.config
                    //string authKey = WebConfigurationManager.AppSettings["AuthKey"];

                    //// Encrypt password with specific key
                    //var encryptPassword = General.EncryptString(authKey, model.Password);

                    //var loginUserId = User.Identity.GetUserId();

                    //// Find users
                    //var loginUser = await _userManager.FindByIdAsync(loginUserId).ConfigureAwait(false);

                    //if (loginUser.AssignCoin < model.AssignCoin && loginUser.Role != 1)
                    //{
                    //    var message = loginUser.AssignCoin == 0 ? "no any" : $"only {loginUser.AssignCoin}";
                    //    return new CommonReturnResponse { Data = null, Message = $"You have {message} coins remaining.", IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };
                    //}

                    var user = new Users
                    {
                        UserName = model.Username,
                        FullName = model.Name,
                        AssignCoin = model.AssignCoin,
                        RoleId = 2,
                        PhoneNumber = model.MobileNumber,
                        Email = model.Email,
                        ParentId = 1,
                        RollingCommission = model.RollingCommission
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //bool isRegisterDetailsSaved = await SaveRegisterDetailAsync(model, user.Id);
                        //if (loginUser.Role != 1)
                        //{
                        //    loginUser.AssignCoin -= model.AssignCoin;

                        //}
                        commonModel = new CommonReturnResponse { Data = null, Message = MessageStatus.Success, IsSuccess = false, Status = ResponseStatusCode.OK };
                        return Json(JsonConvert.SerializeObject(commonModel));
                    }
                    commonModel = new CommonReturnResponse { Data = null, Message = MessageStatus.Error, IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };;
                    return Json(JsonConvert.SerializeObject(commonModel));
                }

                commonModel = new CommonReturnResponse { Data = null, Message = CustomMessageStatus.InvalidModelState, IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };
                return Json(JsonConvert.SerializeObject(commonModel));
            }
            catch
            {
                var errorArr = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                var errorHtml = "<ul>";
                if (errorArr != null && errorArr.Count > 0)
                {
                    foreach (var error in errorArr)
                    {
                        errorHtml += "<li>" + error + "</li>";
                    }
                }

                errorHtml += "</ul>";
                commonModel = new CommonReturnResponse { Data = false, Message = errorHtml, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
                return Json(JsonConvert.SerializeObject(commonModel));
            }
        }
    }
}
