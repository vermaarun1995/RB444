using Microsoft.AspNetCore.Mvc;
using RB444.Core.IServices;
using RB444.Data.Entities;
using RB444.Model.Model;
using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Api.Controllers
{
    [Route("api/BetApi")]
    [ApiController]
    public class BetApiController : ControllerBase
    {
        private readonly IBetApiService _betApiService;
        public BetApiController(IBetApiService betApiService)
        {
            _betApiService = betApiService;
        }

        [HttpPost, Route("SaveBets")]
        public async Task<CommonReturnResponse> SaveBets(Bets model)
        {
            return await _betApiService.SaveBets(model);
        }

        [HttpPost, Route("GetBetHistory")]
        public async Task<CommonReturnResponse> GetBetHistory(UserBetsHistory model)
        {
            return await _betApiService.GetBetHistoryAsync(model);
        }

        [HttpGet, Route("BetSettle")]
        public async Task<CommonReturnResponse> BetSettle(long eventId, string marketId)
        {
            return await _betApiService.BetSettleAsync(eventId, marketId);
        }
    }
}
