﻿using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface ICommonService
    {
        /// <summary>
        /// Get All user Roles.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllRolesAsync();

        /// <summary>
        /// Get All Slider.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllSliderAsync();

        /// <summary>
        /// Get News.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetNewsAsync();

        /// <summary>
        /// Get All Logo.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllLogoAsync();

        /// <summary>
        /// Get Active Logo.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetLogoAsync();

        /// <summary>
        /// Get All Role Activity Log.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetActivityLogAsync();

        /// <summary>
        /// Get User Activity Log.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetUserActivityLogAsync(int UserId);

        /// <summary>
        /// Get Account Statement.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAccountStatementAsync(int UserId);

        /// <summary>
        /// Get Account Statement for all user who are below to super admin.
        /// </summary>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAccountStatementForSuperAdminAsync(int AdminId);

        /// <summary>
        /// Get Profit and loss data of user.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetProfitAndLossAsync(int UserId);
    }
}
