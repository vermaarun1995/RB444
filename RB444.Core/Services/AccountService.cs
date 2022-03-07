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

        public async Task<CommonReturnResponse> DepositAssignCoinAsync(long assignCoin, int parentId, int userId)
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
                    ToUserId = userId
                };
                _result = await _baseRepository.InsertAsync(depositCoint) > 0;
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

        //public async Task<CommonReturnResponse> GetParentUserDetailAsync(int LoginParentUserId)
        //{
        //    IDictionary<string, object> _keyValues = null;
        //    try
        //    {
        //        _keyValues = new Dictionary<string, object> { { "ParentId", ParentUserId } };
        //        var usersList = (await _baseRepository.SelectAsync<Users>(_keyValues)).OrderByDescending(a => a.Id).ToList();
        //        return new CommonReturnResponse
        //        {
        //            Data = usersList,
        //            Message = usersList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
        //            IsSuccess = usersList.Count > 0,
        //            Status = usersList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
        //        return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
        //    }
        //}

        public async Task<CommonReturnResponse> GetAllUsersByParentIdAsync(int ParentUserId)
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
