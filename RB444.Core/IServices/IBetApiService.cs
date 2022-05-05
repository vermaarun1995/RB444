using RB444.Data.Entities;
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

        /// <summary>
        /// Bet settle after match finish.
        /// </summary>        
        /// <returns></returns>
        Task<CommonReturnResponse> BetSettleAsync();

        /// <summary>
        /// Get back or lay amount.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="marketId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBackAndLayAmountAsync(int UserId, string marketId);
    }
}
