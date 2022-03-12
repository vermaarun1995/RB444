using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Core.IServices.BetfairApi
{
    public interface IExchangeService
    {
        /// <summary>
        /// Get all sports.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetSportsAsync(int type);

        /// <summary>
        /// Get Series list.
        /// </summary>
        /// <param name="SportId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetSeriesListAsync(int SportId, int type);
    }
}
