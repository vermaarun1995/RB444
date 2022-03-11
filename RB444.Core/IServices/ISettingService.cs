using RB444.Data.Entities;
using RB444.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface ISettingService
    {
        /// <summary>
        /// Add or update sports setting
        /// </summary>
        /// <param name="sportsSetting"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> AddOrUpdateSportsSettingAsync(Sports sportsSetting);
        /// <summary>
        /// Update Status of Sports.
        /// </summary>
        /// <param name="sportsSetting"></param>
        /// <returns></returns>
        //Task<CommonReturnResponse> UpdateSportsStatusAsync(SportsSetting sportsSetting);

        /// <summary>
        /// Update Limit of Sports.
        /// </summary>
        /// <param name="sportsSetting"></param>
        /// <returns></returns>
        //Task<CommonReturnResponse> UpdateSportsLimitAsync(SportsSetting sportsSetting);

        /// <summary>
        /// Update Stake Limit.
        /// </summary>
        /// <param name="stakeLimitList"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateStakeLimitAsync(List<StakeLimit> stakeLimitList);

        /// <summary>
        /// Get All Stake Limit.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetStakeLimitAsync();
    }
}
