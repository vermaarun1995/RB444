using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Data.Repository;
using RB444.Model.ViewModel;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RB444.Core.Services
{
    public class SettingService : ISettingService
    {
        private readonly IBaseRepository _baseRepository;
        public SettingService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CommonReturnResponse> AddOrUpdateSportsSettingAsync(Sports sportsSetting)
        {
            try
            {
                //string query = string.Format(@"Update SportsSetting set Status = {0} where SportName = '{1}'", sportsSetting.Status, sportsSetting.SportName);
                //await _baseRepository.QueryAsync<SportsSetting>(query);
                var sports = await _baseRepository.GetDataByIdAsync<SportsSettings>(sportsSetting.Id);
                if (sports != null)
                {
                    int _resultId = await _baseRepository.UpdateAsync(sportsSetting);
                    if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = true, Message = MessageStatus.Update, IsSuccess = true, Status = ResponseStatusCode.OK };
                }
                else
                {
                    var _resultId = await _baseRepository.InsertAsync(sportsSetting);
                    if (_resultId > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                    return new CommonReturnResponse { Data = true, Message = MessageStatus.Save, IsSuccess = true, Status = ResponseStatusCode.OK };
                }
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        //public async Task<CommonReturnResponse> UpdateSportsStatusAsync(SportsSetting sportsSetting)
        //{
        //    try
        //    {
        //        string query = string.Format(@"Update SportsSetting set Status = {0} where SportName = '{1}'", sportsSetting.Status, sportsSetting.SportName);
        //        await _baseRepository.QueryAsync<SportsSetting>(query);
        //        _baseRepository.Commit();
        //        return new CommonReturnResponse { Data = true, Message = MessageStatus.Delete, IsSuccess = true, Status = ResponseStatusCode.OK };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
        //    }
        //}

        //public async Task<CommonReturnResponse> UpdateSportsLimitAsync(SportsSetting sportsSetting)
        //{
        //    try
        //    {
        //        string query = string.Format(@"Update SportsSetting set MinOddLimit = {0},MaxOddLimit = {1} where SportName = '{2}'", sportsSetting.MinOddLimit, sportsSetting.MaxOddLimit, sportsSetting.SportName);
        //        await _baseRepository.QueryAsync<SportsSetting>(query);
        //        _baseRepository.Commit();
        //        return new CommonReturnResponse { Data = true, Message = MessageStatus.Delete, IsSuccess = true, Status = ResponseStatusCode.OK };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
        //    }
        //}

        public async Task<CommonReturnResponse> UpdateStakeLimitAsync(List<StakeLimit> stakeLimitList)
        {
            try
            {
                await _baseRepository.BulkUpdate(stakeLimitList);
                _baseRepository.Commit();
                return new CommonReturnResponse { Data = true, Message = MessageStatus.Update, IsSuccess = true, Status = ResponseStatusCode.OK };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetStakeLimitAsync()
        {
            try
            {
                var stakeLimits = await _baseRepository.GetListAsync<StakeLimit>();
                return new CommonReturnResponse
                {
                    Data = stakeLimits,
                    Message = stakeLimits.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = stakeLimits.Count > 0,
                    Status = stakeLimits.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
    }
}
