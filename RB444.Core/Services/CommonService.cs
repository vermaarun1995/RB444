﻿using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Data.Repository;
using RB444.Model.Model;
using RB444.Model.ViewModel;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Core.Services
{
    public class CommonService : ICommonService
    {
        private readonly IBaseRepository _baseRepository;
        public CommonService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

                

        public async Task<CommonReturnResponse> GetAllRolesAsync()
        {
            try
            {
                var roles = await _baseRepository.GetListAsync<UserRoles>();
                return new CommonReturnResponse
                {
                    Data = roles,
                    Message = roles.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = roles.Count > 0,
                    Status = roles.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAllSliderAsync()
        {
            try
            {
                var sliders = await _baseRepository.GetListAsync<Slider>();
                return new CommonReturnResponse
                {
                    Data = sliders,
                    Message = sliders.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sliders.Count > 0,
                    Status = sliders.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAllLogoAsync()
        {
            try
            {
                var logos = await _baseRepository.GetListAsync<Logo>();
                return new CommonReturnResponse
                {
                    Data = logos,
                    Message = logos.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = logos.Count > 0,
                    Status = logos.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetLogoAsync()
        {
            try
            {
                var logos = (await _baseRepository.GetListAsync<Logo>()).Where(s=>s.Status==true).FirstOrDefault();
                return new CommonReturnResponse
                {
                    Data = logos,
                    Message = logos != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = logos != null,
                    Status = logos != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetNewsAsync()
        {
            try
            {
                var news = await _baseRepository.GetListAsync<News>();
                return new CommonReturnResponse
                {
                    Data = news,
                    Message = news.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = news.Count > 0,
                    Status = news.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetActivityLogAsync()
        {
            List<ActivityLogVM> activityLogVMs = null;
            try
            {
                string query = "select ActivityLog.*,Users.FullName as UserName from ActivityLog,Users where ActivityLog.UserId = Users.Id;";
                var result = await _baseRepository.GetQueryMultipleAsync(query, null, gr => gr.Read<ActivityLogVM>());
                activityLogVMs = (result[0] as List<ActivityLogVM>).ToList();
                return new CommonReturnResponse
                {
                    Data = activityLogVMs,
                    Message = activityLogVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = activityLogVMs.Count > 0,
                    Status = activityLogVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetUserActivityLogAsync(int UserId)
        {
            List<ActivityLogVM> activityLogVMs = null;
            try
            {
                string query = "select ActivityLog.*,Users.FullName as UserName from ActivityLog,Users where ActivityLog.UserId = Users.Id and Users.RoleId = 7 and UserId = " + UserId + ";";
                var result = await _baseRepository.GetQueryMultipleAsync(query, null, gr => gr.Read<ActivityLogVM>());
                activityLogVMs = (result[0] as List<ActivityLogVM>).ToList();
                return new CommonReturnResponse
                {
                    Data = activityLogVMs,
                    Message = activityLogVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = activityLogVMs.Count > 0,
                    Status = activityLogVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAccountStatementAsync(int UserId)
        {
            IDictionary<string, object> _keyValues = new Dictionary<string, object>();
            List<AccountStatementVM> accountStatementVMs = new List<AccountStatementVM>();
            try
            {
                var userList = await _baseRepository.GetListAsync<Users>();                

                _keyValues.Add("ToUserId", UserId);
                _keyValues.Add("IsProfitLoss", true);
                var accountStatementList = await _baseRepository.SelectAsync<AccountStatement>(_keyValues);
                foreach (var item in accountStatementList)
                {
                    var accountStatementVM = new AccountStatementVM
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Deposit = item.Deposit,
                        Balance = item.Balance,
                        Withdraw = item.Withdraw,
                        Remark = item.Remark,
                        FromUser = userList.Where(x => x.Id == item.FromUserId).Select(y => y.FullName).FirstOrDefault(),
                        ToUser = userList.Where(x => x.Id == item.ToUserId).Select(y => y.FullName).FirstOrDefault()
                    };
                    accountStatementVMs.Add(accountStatementVM);
                }
                return new CommonReturnResponse
                {
                    Data = accountStatementVMs,
                    Message = accountStatementVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = accountStatementVMs.Count > 0,
                    Status = accountStatementVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAccountStatementForSuperAdminAsync(int AdminId)
        {
            IDictionary<string, object> _keyValues = new Dictionary<string, object>();
            List<AccountStatementVM> accountStatementVMs = new List<AccountStatementVM>();
            try
            {
                var userList = await _baseRepository.GetListAsync<Users>();

                _keyValues.Add("FromUserId", AdminId);
                _keyValues.Add("IsProfitLoss", true);
                var accountStatementList = await _baseRepository.SelectAsync<AccountStatement>(_keyValues);
                foreach (var item in accountStatementList)
                {
                    var accountStatementVM = new AccountStatementVM
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Deposit = item.Deposit,
                        Balance = item.Balance,
                        Withdraw = item.Withdraw,
                        Remark = item.Remark,
                        FromUser = userList.Where(x => x.Id == item.FromUserId).Select(y => y.FullName).FirstOrDefault(),
                        ToUser = userList.Where(x => x.Id == item.ToUserId).Select(y => y.FullName).FirstOrDefault()
                    };
                    accountStatementVMs.Add(accountStatementVM);
                }
                return new CommonReturnResponse
                {
                    Data = accountStatementVMs,
                    Message = accountStatementVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = accountStatementVMs.Count > 0,
                    Status = accountStatementVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetProfitAndLossAsync(int UserId)
        {
            List<ProfitAndLossVM> profitAndLossVMs = new List<ProfitAndLossVM>();
            try
            {
                string query = string.Format(@"select  Bets.Id,BetId,Sports.SportName,Event,Market,Selection,(CASE WHEN OddsType = 1 THEN 'Match Odds' ELSE Case WHEN OddsType = 2 THEN 'Bookmaker' ELSE 'Fancy' end END) as OddsType,Type,OddsRequest,AmountStake,PlaceTime,SettleTime,IsSettlement,(CASE WHEN ResultType = 1 THEN 'Win' ELSE Case WHEN ResultType = 2 THEN 'Loss' ELSE 'Draw' end END) as ResultType,ResultAmount from Bets,Sports where Bets.SportId = Sports.Id and Bets.Status = 1 and Bets.IsSettlement = 1 and UserId = {0}", UserId);

                var result = await _baseRepository.GetQueryMultipleAsync(query, new { UserId = UserId }, gr => gr.Read<ProfitAndLossVM>());
                profitAndLossVMs = (result[0] as List<ProfitAndLossVM>).ToList();
                return new CommonReturnResponse
                {
                    Data = profitAndLossVMs,
                    Message = profitAndLossVMs.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = profitAndLossVMs.Count > 0,
                    Status = profitAndLossVMs.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
    }
}
