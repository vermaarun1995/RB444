using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB444.Core.IServices;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketWatchController : ControllerBase
    {
        private readonly IMarketWatchService _marketWatchService;
        public MarketWatchController(IMarketWatchService marketWatchService)
        {
            _marketWatchService = marketWatchService;
        }
        [HttpGet, Route("GetBetHistory")]
        public async Task<CommonReturnResponse> GetBetHistory(int SportId,int UserId)
        {
            return await _marketWatchService.GetBetHistory(SportId, UserId);
        }
    }
}
