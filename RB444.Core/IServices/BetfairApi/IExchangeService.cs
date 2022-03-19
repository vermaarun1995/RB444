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

        /// <summary>
        /// Get matches list.
        /// </summary>
        /// <param name="SportId"></param>
        /// <param name="SeriesId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetMatchesListAsync(int SportId, long SeriesId, int type);

        /// <summary>
        /// Get matches list.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="SportId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetMatchEventsAsync(long eventId, int SportId);

        /// <summary>
        /// Get Matches List By Sports.
        /// </summary>
        /// <param name="SportId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetSportsEventsAsync(int SportId);

        /// <summary>
        /// Get Sport Event In Play.
        /// </summary>        
        /// <returns></returns>
        Task<CommonReturnResponse> GetSportsInPlayEventsAsync();
    }
}
