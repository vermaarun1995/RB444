using Microsoft.AspNetCore.Mvc;
using RB444.Core.IServices;
using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Api.Controllers
{
    [Route("api/Common")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly ICommonService _commonService;

        public CommonController(ICommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet, Route("GetAllSliders")]
        public async Task<CommonReturnResponse> GetAllSliders()
        {
            return await _commonService.GetAllSliderAsync();
        }

        [HttpGet, Route("GetNews")]
        public async Task<CommonReturnResponse> GetNews()
        {
            return await _commonService.GetNewsAsync();
        }

        [HttpGet, Route("GetLogo")]
        public async Task<CommonReturnResponse> GetLogo()
        {
            return await _commonService.GetLogoAsync();
        }
    }
}
