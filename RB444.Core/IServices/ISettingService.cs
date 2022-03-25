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
        /// Update Series Setting.
        /// </summary>
        /// <param name="SeriesId"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateSeriesSettingAsync(long SeriesId, bool Status);

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
