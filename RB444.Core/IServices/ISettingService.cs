using RB444.Data.Entities;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface ISettingService
    {
        /// <summary>
        /// Update Status of Sports.
        /// </summary>
        /// <param name="sportsSetting"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateSportsStatusAsync(SportsSetting sportsSetting);

        /// <summary>
        /// Update Limit of Sports.
        /// </summary>
        /// <param name="sportsSetting"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateSportsLimitAsync(SportsSetting sportsSetting);
    }
}
