using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Data.Repository;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository _baseRepository;

        public AccountService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }
        public async Task<CommonReturnResponse> UpdateAssignCoinAsync(long AssignCoin, int LoginUserId)
        {
            Users users = null;
            bool _result = false;
            try
            {
                if (LoginUserId > 0)
                {
                    users = await _baseRepository.GetDataByIdAsync<Users>(LoginUserId);
                    if (users != null)
                    {
                        users.AssignCoin = users.AssignCoin - AssignCoin;
                        _result = await _baseRepository.UpdateAsync(users) == 1;
                        if (_result) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    }
                    else
                    {
                        return new CommonReturnResponse() { IsSuccess = false, Status = ResponseStatusCode.NOTFOUND, Message = MessageStatus.NoRecord, Data = null };
                    }
                    return new CommonReturnResponse
                    {
                        Data = _result,
                        Message = _result ? MessageStatus.Update : MessageStatus.Error,
                        IsSuccess = _result,
                        Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                    };
                }
                return new CommonReturnResponse() { IsSuccess = false, Status = ResponseStatusCode.BADREQUEST, Message = MessageStatus.InvalidData, Data = null };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (users != null) { users = null; } }
        }

        public async Task<CommonReturnResponse> DepositAssignCoinAsync(long assignCoin, int parentId, int userId, int UserRoleId)
        {
            bool _result = false;
            try
            {
                var depositCoint = new AccountStatement
                {
                    CreatedDate = DateTime.Now,
                    Deposit = assignCoin,
                    Withdraw = 0,
                    Balance = assignCoin,
                    Remark = "Deposit",
                    FromUserId = parentId,
                    ToUserId = userId,
                    ToUserRoleId = UserRoleId
                };
                _result = await _baseRepository.InsertAsync(depositCoint) > 0;
                if (_result == true) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                return new CommonReturnResponse
                {
                    Data = _result,
                    Message = _result ? MessageStatus.Update : MessageStatus.Error,
                    IsSuccess = _result,
                    Status = _result ? ResponseStatusCode.OK : ResponseStatusCode.ERROR
                };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetUserRolesAsync()
        {
            try
            {
                var userRoles = await _baseRepository.GetListAsync<UserRoles>();
                return new CommonReturnResponse
                {
                    Data = userRoles,
                    Message = userRoles.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = userRoles.Count > 0,
                    Status = userRoles.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAllUsers()
        {
            try
            {
                var users = await _baseRepository.GetListAsync<Users>();
                return new CommonReturnResponse
                {
                    Data = users,
                    Message = users.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = users.Count > 0,
                    Status = users.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetUserDetailAsync(int UserId)
        {
            try
            {
                var user = await _baseRepository.GetDataByIdAsync<Users>(UserId);
                return new CommonReturnResponse
                {
                    Data = user,
                    Message = user != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = user != null,
                    Status = user != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetAllUsersByParentIdAsync(int LoginUserId, int RoleId)
        {
            IDictionary<string, object> _keyValues = null;
            try
            {
                var userRoles = await _baseRepository.GetListAsync<UserRoles>();
                _keyValues = new Dictionary<string, object> { { "Id", LoginUserId } };
                var loginUser = (await _baseRepository.SelectAsync<Users>(_keyValues)).FirstOrDefault();

                _keyValues = new Dictionary<string, object> { { "ParentId", LoginUserId } };
                var users = (await _baseRepository.SelectAsync<Users>(_keyValues)).ToList();

                var isAbleToChange = RoleId > 0 ? loginUser.RoleId == RoleId - 1 : false;

                var u = users.Where(x => x.ParentId == LoginUserId).ToList();
                var totalUser = u;
                if (u != null && u.Count() > 0)
                {
                    for (; ; )
                    {
                        var ids = u.Select(x => x.Id).ToList();
                        var u1 = users.Where(x => ids.Contains(x.ParentId)).ToList();
                        if (u1.Count == 0)
                        {
                            break;
                        }

                        totalUser.AddRange(u1);
                        u = u1;
                    }
                }

                totalUser = totalUser != null && totalUser.Count() > 0 ? totalUser.Where(x => x.RoleId == RoleId).ToList() : totalUser;
                var roleName = userRoles.Where(y => y.Id == RoleId).Select(x => x.Name).FirstOrDefault();

                var model = new RegisterListVM
                {
                    LoginUserId = loginUser.Id,
                    LoginUserRole = loginUser.RoleId,
                    LoginUser = loginUser,
                    RoleName = roleName,
                    Users = totalUser,
                    UserRoles = userRoles,
                    IsAbleToChange = isAbleToChange
                };

                return new CommonReturnResponse
                {
                    Data = model,
                    Message = model != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = model != null,
                    Status = model != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> CheckCoinAsync(int ParentUserId, long AssignCoin)
        {
            IDictionary<string, object> _keyValues = null;
            try
            {
                _keyValues = new Dictionary<string, object> { { "ParentId", ParentUserId } };
                var usersList = (await _baseRepository.SelectAsync<Users>(_keyValues)).OrderByDescending(a => a.Id).ToList();
                return new CommonReturnResponse
                {
                    Data = usersList,
                    Message = usersList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = usersList.Count > 0,
                    Status = usersList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
    }
}
