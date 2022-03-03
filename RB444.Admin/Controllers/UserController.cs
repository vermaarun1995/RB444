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

        [HttpGet]
        public async Task<ActionResult> RegisterUsersList(int? id)
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;

            var isAbleToChange = id != null && id > 0 ? loginUser.RoleId == id : false;
            List<Users> usresList = new List<Users>();
            Users user = new Users { UserName = "arun", AssignCoin = 1000, ExposureLimit = 100, Id = 13, Status = 1 };
            usresList.Add(user);
            //// Get users
            //var users = UserManager.Users;//.Where(x => x.ParentId == loginUserId);
            //var u = users.Where(x => x.ParentId == loginUserId).ToList();
            //var totalUser = u;
            //if (u != null && u.Count() > 0)
            //{
            //    for (; ; )
            //    {
            //        var ids = u.Select(x => x.Id).ToList();
            //        var u1 = users.Where(x => ids.Contains(x.ParentId)).ToList();
            //        if (u1.Count == 0)
            //        {
            //            break;
            //        }

            //        totalUser.AddRange(u1);
            //        u = u1;
            //    }
            //}

            //totalUser = totalUser != null && totalUser.Count() > 0 ? totalUser.Where(x => x.Role == id).ToList() : totalUser;

            //var roleName = string.Empty;
            //switch (id)
            //{
            //    case 2:
            //        roleName = "Super Admin";
            //        break;
            //    case 3:
            //        roleName = "Admin";
            //        break;
            //    case 4:
            //        roleName = "Sub Admin";
            //        break;
            //    case 5:
            //        roleName = "Super Master";
            //        break;
            //    case 6:
            //        roleName = "Master";
            //        break;
            //    case 7:
            //        roleName = "User";
            //        break;
            //}

            var model = new RegisterListVM
            {
                LoginUserId = "3",
                LoginUserRole = 2,
                LoginUser = loginUser,
                RoleName = "Super Admin",
                Users = usresList,
                IsAbleToChange = isAbleToChange
            };

            return View(model);
        }

        public async Task<ActionResult> AccountStatement(int id)
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;

            var result = new List<AccountStatementVM>();
            var accountStatements = new List<AccountStatement>();

            var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetAllUsers", _configuration["ApiKeyUrl"]));
            var users = jsonParser.ParsJson<List<Users>>(Convert.ToString(commonModel.Data));

            //if (!string.IsNullOrWhiteSpace(id))
            //{
            //    accountStatements = accountStatements.Where(x => x.ToUserId == id).ToList();
            //}

            //if (accountStatements?.Count > 0)
            //{
            //    result = accountStatements.Select(x => new AccountStatementVM
            //    {
            //        Balance = x.Balance,
            //        CreatedDate = x.CreatedDate,
            //        Deposit = x.Deposit,
            //        Withdraw = x.Withdraw,
            //        Remark = x.Remark,
            //        FromUser = users.FirstOrDefault(y => y.Id == x.FromUserId).Name,
            //        ToUser = users.FirstOrDefault(y => y.Id == x.ToUserId).Name
            //    }).ToList();
            //}
            result.Add(new AccountStatementVM
            {
                Balance = 1000,
                CreatedDate = DateTime.Now,
                Deposit = 100,
                Withdraw = 10,
                Remark = "withdraw 10000",
                FromUser = "super admin",
                ToUser = "admin"
            });
            return View(result);
        }

        public async Task<ActionResult> UserProfile(string id)
        {
            var contextUser = HttpContext.User;
            var loginUser = await _userManager.FindByEmailAsync(contextUser.Identity.Name);
            ViewBag.LoginUser = loginUser;

            var isAdmin = string.IsNullOrWhiteSpace(id);

            if (!isAdmin)
            {
                //var commonModel = await _requestServices.GetAsync<CommonReturnResponse>(String.Format("{0}Account/GetAllUsers", _configuration["ApiKeyUrl"]));
                //var users = jsonParser.ParsJson<List<Users>>(Convert.ToString(commonModel.Data));
               // user = _dbContext.Users.FirstOrDefault(x => x.Id == id);
            }

            var result = new UserProfileVM
            {
                AgentRollingCommission = false,
                Commision = 1000,
                ExposureLimit = 1000,
                MobileNumber = "9887328875",
                Name = "Test",
                RollingCommission = false,
                UserId = "13",
                IsAdmin = isAdmin
            };

            return View(result);
        }

        public async Task<string> DeleteUser(string userId)
        {
            try
            {
                //// Update user delete status
                //await _dbContext.Database.ExecuteSqlCommandAsync("UPDATE AspNetUsers SET deleted=1 WHERE Id=@id",
                //            new SqlParameter("id", userId));

                //await _dbContext.SaveChangesAsync();

                return "ok";
            }
            catch
            {
               // var errorArr = ModelState.Values?.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                var errorHtml = "<ul>";
                //if (errorArr != null && errorArr.Count > 0)
                //{
                //    foreach (var error in errorArr)
                //    {
                      errorHtml += "<li>error</li>";
                //    }
                //}

                errorHtml += "</ul>";
                return errorHtml;
            }
        }

        [HttpPost]
        public async Task<string> UpdateUserProfile(string userId, string id, string value)
        {
            try
            {
                //switch (id)
                //{
                //    case "RollingCommission":
                //        var rollingCommission = value.ToLower() == "on";
                //        await _dbContext.Database.ExecuteSqlCommandAsync("UPDATE AspNetUsers SET RollingCommission=@rollingCommission WHERE Id=@id",
                //            new SqlParameter("rollingCommission", rollingCommission),
                //            new SqlParameter("id", userId));
                //        await _dbContext.SaveChangesAsync();
                //        break;
                //    case "ExposureLimit":
                //        await _dbContext.Database.ExecuteSqlCommandAsync("UPDATE AspNetUsers SET ExposureLimit=@exposureLimit WHERE Id=@id",
                //            new SqlParameter("exposureLimit", Convert.ToInt32(value)),
                //            new SqlParameter("id", userId));
                //        await _dbContext.SaveChangesAsync();
                //        break;
                //    case "MobileNumber":
                //        await _dbContext.Database.ExecuteSqlCommandAsync("UPDATE AspNetUsers SET MobileNumber=@mobileNumber, PhoneNumber=@mobileNumber WHERE Id=@id",
                //            new SqlParameter("mobileNumber", value),
                //            new SqlParameter("id", userId));
                //        await _dbContext.SaveChangesAsync();
                //        break;
                //    case "Password":
                //        // Get auth key from web.config
                //        string authKey = WebConfigurationManager.AppSettings["AuthKey"];

                //        // Encrypt password with specific key
                //        var encryptPassword = General.EncryptString(authKey, value);

                //        // Get user
                //        var user = await UserManager.FindByIdAsync(userId).ConfigureAwait(false);

                //        var hashedPassword = UserManager.PasswordHasher.HashPassword(encryptPassword);
                //        user.PasswordHash = hashedPassword;
                //        user.Password = encryptPassword;
                //        await UserManager.UpdateAsync(user);
                //        await _dbContext.SaveChangesAsync();
                //        break;
                //}

                return "ok";
            }
            catch
            {
                //var errorArr = ModelState.Values?.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
                var errorHtml = "<ul>";
                //if (errorArr != null && errorArr.Count > 0)
                //{
                //    foreach (var error in errorArr)
                //    {
                       errorHtml += "<li>error</li>";
                //    }
                //}

                errorHtml += "</ul>";
                return errorHtml;
            }
        }

    }
}
