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
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet, Route("GetAllIcons")]
        public async Task<CommonReturnResponse> GetAllIcons(int? type)
        {
            return await _commonService.GetAllIconsAsync(type);
        }
    }
}
