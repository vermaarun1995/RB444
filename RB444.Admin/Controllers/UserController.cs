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
            var user = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);

            CommonReturnResponse commonModel = null;
            List<UserRoles> userRoles = null;
            List<Users> users = null;
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetUserRoles", _configuration["ApiKeyUrl"]));
                userRoles = jsonParser.ParsJson<List<UserRoles>>(Convert.ToString(commonModel.Data));

                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetAllUsers?RoleId={1}&LoginUserId={2}", _configuration["ApiKeyUrl"], user.RoleId, user.Id));
                users = jsonParser.ParsJson<List<Users>>(Convert.ToString(commonModel.Data));

                ViewBag.UserRoles = userRoles;
                ViewBag.LoginUser = user;
                ViewBag.Users = users;
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> RegisterUsersList(int? id, int? userId)
        {
            var loginUser = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
            ViewBag.LoginUser = loginUser;
            CommonReturnResponse commonModel = null;
            var model = new RegisterListVM();
            try
            {
                commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetUsersByParentId?ParentId={1}&RoleId={2}&UserId={3}", _configuration["ApiKeyUrl"], loginUser.Id, id, userId));
                model = jsonParser.ParsJson<RegisterListVM>(Convert.ToString(commonModel.Data));

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<ActionResult> AccountStatement(int id)
        {
            List<AccountStatementVM> accountStatementVM = null;
            try
            {
                var loginUser = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
                ViewBag.LoginUser = loginUser;

                var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Common/GetAccountStatement?UserId={1}", _configuration["ApiKeyUrl"], id));
                accountStatementVM = jsonParser.ParsJson<List<AccountStatementVM>>(Convert.ToString(commonModel.Data));
            }
            catch (Exception)
            {

                throw;
            }

            return View(accountStatementVM);
        }

        public async Task<ActionResult> UserProfile(int id)
        {
            Users users = null;
            try
            {
                users = new Users();
                var loginUser = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
                ViewBag.LoginUser = loginUser;

                var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetUserDetail?UserId={1}", _configuration["ApiKeyUrl"], id));
                if (commonModel.Data != null)
                {
                    users = jsonParser.ParsJson<Users>(Convert.ToString(commonModel.Data));
                }
            }
            catch (Exception)
            {
                throw;
            }

            var result = new UserProfileVM
            {
                AgentRollingCommission = false,
                Commision = users.Commision,
                ExposureLimit = users.ExposureLimit,
                MobileNumber = users.PhoneNumber,
                Name = users.FullName,
                RollingCommission = users.RollingCommission,
                UserId = users.Id,
                IsAdmin = true
            };

            return View(result);
        }

        public async Task<string> DeleteUser(int userId,string status)
        {
            try
            {
                int statusId = status == "active" ? 1 : status == "suspend" ? 2 : 3;
               var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/UpdateUserStatus?Status={1}&UserId={2}", _configuration["ApiKeyUrl"], statusId, userId));
                if(commonModel.IsSuccess) { return "ok"; }
            }
            catch(Exception ex)
            {
                throw;
            }
            return "Something went wrong.";
        }

        [HttpPost]
        public async Task<JsonResult> UpdateUserProfile(string userId, string id, string value)
        {
            CommonReturnResponse commonModel = null;
            string sql = string.Empty;
            try
            {
                commonModel = new CommonReturnResponse();
                switch (id)
                {
                    case "RollingCommission":
                        var rollingCommission = value.ToLower() == "on";
                        sql = "UPDATE Users SET RollingCommission=" + rollingCommission + " WHERE Id=" + userId;
                        commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/UpdateUserDetail?query={1}", _configuration["ApiKeyUrl"], sql));
                        break;
                    case "ExposureLimit":
                        sql = "UPDATE Users SET ExposureLimit=" + value + " WHERE Id=" + userId;
                        commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/UpdateUserDetail?query={1}", _configuration["ApiKeyUrl"], sql));
                        break;
                    case "MobileNumber":
                        sql = "UPDATE Users SET PhoneNumber=" + value + " WHERE Id=" + userId;
                       commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/UpdateUserDetail?query={1}", _configuration["ApiKeyUrl"], sql));
                        break;
                    case "Password":
                        var profileUser = await _userManager.FindByIdAsync(userId);
                        var Code = await _userManager.GeneratePasswordResetTokenAsync(profileUser);
                        var result = await _userManager.ResetPasswordAsync(profileUser, Code, value);
                        if (result.Succeeded)
                        {
                            commonModel = new CommonReturnResponse()
                            {
                                Data = result,
                                IsSuccess = true,
                                Message = CustomMessageStatus.resetPwd,
                                Status = ResponseStatusCode.OK
                            };
                        }
                        else
                        {
                            commonModel = new CommonReturnResponse()
                            {
                                Data = result.Errors,
                                IsSuccess = false,
                                Message = result.ToString(),
                                Status = ResponseStatusCode.NOTACCEPTABLE
                            };
                        }
                        break;
                }

                return Json(commonModel);
            }
            catch (Exception ex)
            {
                commonModel = new CommonReturnResponse()
                {
                    Data = null,
                    IsSuccess = false,
                    Message = ex.Message,
                    Status = ResponseStatusCode.NOTACCEPTABLE
                };
                return Json(JsonConvert.SerializeObject(commonModel));
            }
        }

        [HttpPost]
        public async Task<string> DepositWithdrawCoin(int UserId,bool IsDeposit,long Balance,string Remark,string Password)
        {
            try
            {
                var loginUser = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
                var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/DepositWithdrawCoin?Amount={1}&ParentId={2}&UserId={3}&UserRoleId={4}&&Remark={5}&Type={6}&Password={7}", _configuration["ApiKeyUrl"], Balance, loginUser.Id,UserId, loginUser.RoleId+1, Remark, IsDeposit,Password));
                if (commonModel.IsSuccess) { return "ok"; }
                return "ok";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public async Task<string> PorfitLoss(int UserId, bool IsProfit, long Balance, string Remark, string Password)
        {
            try
            {
                //var loginUser = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
                //var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/DepositWithdrawCoin?Amount={1}&ParentId={2}&UserId={3}&UserRoleId={4}&&Remark={5}&Type={6}&Password={7}", _configuration["ApiKeyUrl"], Balance, loginUser.Id, UserId, loginUser.RoleId + 1, Remark, IsDeposit,Password));
                //if (commonModel.IsSuccess) { return "ok"; }
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreditReference(int uId)
        {
            CommonReturnResponse commonModel = null;
            List<Series> serieslist = null;
            try
            {
                //commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}exchange/GetSeries?SportId={1}&type=2", _configuration["ApiKeyUrl"], SportId));
                //if (commonModel.IsSuccess && commonModel.Data != null)
                //{
                //    serieslist = jsonParser.ParsJson<List<Series>>(Convert.ToString(commonModel.Data));
                //    if (status > 0)
                //    {
                //        serieslist = serieslist.Where(s => s.Status == Convert.ToBoolean(status - 1)).ToList();
                //    }
                //}
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AddServiceController : deleteService()", ex);
                throw;
            }
            return PartialView("_CreditReference");
        }

        [HttpPost]
        public async Task<string> ExposureLimit(int UserId, long Balance, string Remark, string Password)
        {
            try
            {
                //var loginUser = JsonConvert.DeserializeObject<Users>(Request.Cookies["loginUserDetail"]);
                //var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/DepositWithdrawCoin?Amount={1}&ParentId={2}&UserId={3}&UserRoleId={4}&&Remark={5}&Type={6}&Password={7}", _configuration["ApiKeyUrl"], Balance, loginUser.Id, UserId, loginUser.RoleId + 1, Remark, IsDeposit,Password));
                //if (commonModel.IsSuccess) { return "ok"; }
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        

    }
}
