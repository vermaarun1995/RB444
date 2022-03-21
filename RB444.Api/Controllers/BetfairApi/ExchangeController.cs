using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RB444.Core.IServices.BetfairApi;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RB444.Api.Controllers.BetfairApi
{
    [Route("api/exchange")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeService _exchangeService;

        public ExchangeController(IExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpGet, Route("GetSports")]
        public async Task<CommonReturnResponse> GetSports(int type)
        {
            return await _exchangeService.GetSportsAsync(type);
        }

        [HttpGet, Route("GetSeries")]
        public async Task<CommonReturnResponse> GetSeries(int SportId, int type)
        {
            return await _exchangeService.GetSeriesListAsync(SportId, type);
        }
    }
}
