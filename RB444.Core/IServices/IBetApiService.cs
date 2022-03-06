﻿using RB444.Data.Entities;
using RB444.Model.Model;
using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface IBetApiService
    {
        /// <summary>
        /// Save Bets.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> SaveBets(Bets model);

        /// <summary>
        /// Get User bet history.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBetHistoryAsync(UserBetsHistory model);
    }
}