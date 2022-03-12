using Microsoft.AspNetCore.Mvc;
using RB444.Core.IServices.BetfairApi;
using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Api.Controllers.BetfairApi
{
    [Route("api/BetfairApi/BetfairApi")]
    [ApiController]
    public class BetfairApiController : ControllerBase
    {
        private readonly IBetfairApiServices _betfairApiServices;

        public BetfairApiController(IBetfairApiServices betfairApiServices)
        {
            _betfairApiServices = betfairApiServices;
        }


        [HttpGet, Route("GetSportsList")]
        public async Task<CommonReturnResponse> GetSportsList()
        {
            return await _betfairApiServices.GetSportsListAsync();
        }        

        [HttpGet, Route("GetSeriesList")]
        public async Task<CommonReturnResponse> GetSeriesList()
        {
            return await _betfairApiServices.GetSeriesListAsync();
        }

        [HttpGet, Route("GetMatchList")]
        public async Task<CommonReturnResponse> GetMatchList(string Key)
        {
            return await _betfairApiServices.GetMatchListAsync(Key);
        }

        [HttpGet, Route("GetMatchOdds")]
        public async Task<CommonReturnResponse> GetMatchOdds(string id, string Key)
        {
            return await _betfairApiServices.GetMatchOddsAsync(id, Key);
        }
    }
}
