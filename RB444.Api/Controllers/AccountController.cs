using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Models.Model;
using System;
using System.Threading.Tasks;

namespace RB444.Api.Controllers
{
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IAccountService _accountService;
        public AccountController(UserManager<Users> userManager, SignInManager<Users> signInManager, IAccountService accountService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost, Route("Login")]
        public async Task<CommonReturnResponse> Login(LoginViewModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return new CommonReturnResponse { Data = null, Message = CustomMessageStatus.InvalidModelState, IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };
            }
            try
            {
                var user = await _userManager.FindByEmailAsync(model.email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.email, model.password, model.rememberme, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return new CommonReturnResponse { Data = user, Message = CustomMessageStatus.Loginsuccess, IsSuccess = true, Status = ResponseStatusCode.OK };
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                        return new CommonReturnResponse { Data = null, Message = CustomMessageStatus.InvliadLogin, IsSuccess = false, Status = ResponseStatusCode.BADREQUEST };
                    }
                }
                else
                {
                    return new CommonReturnResponse { Data = null, Message = MessageStatus.NoRecord, IsSuccess = true, Status = ResponseStatusCode.NOTFOUND };
                }
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountController : Login()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
    }
}
