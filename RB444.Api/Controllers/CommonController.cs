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

        [HttpGet, Route("GetSports")]
        public async Task<CommonReturnResponse> GetSports(int type)
        {
            return await _commonService.GetSportsAsync(type);
        }

        [HttpGet, Route("GetAllSliders")]
        public async Task<CommonReturnResponse> GetAllSliders()
        {
            return await _commonService.GetAllSliderAsync();
        }

        [HttpGet, Route("GetAllNews")]
        public async Task<CommonReturnResponse> GetAllNews()
        {
            return await _commonService.GetNewsAsync();
        }

        [HttpGet, Route("GetAllLogo")]
        public async Task<CommonReturnResponse> GetAllLogo()
        {
            return await _commonService.GetAllLogoAsync();
        }

        [HttpGet, Route("GetLogo")]
        public async Task<CommonReturnResponse> GetLogo()
        {
            return await _commonService.GetLogoAsync();
        }

        [HttpGet, Route("GetActivityLog")]
        public async Task<CommonReturnResponse> GetActivityLog()
        {
            return await _commonService.GetActivityLogAsync();
        }

        [HttpGet, Route("GetUserActivityLog")]
        public async Task<CommonReturnResponse> GetUserActivityLog()
        {
            return await _commonService.GetUserActivityLogAsync();
        }

        [HttpGet, Route("GetAccountStatement")]
        public async Task<CommonReturnResponse> GetAccountStatement(int UserId)
        {
            return await _commonService.GetAccountStatementAsync(UserId);
        }

        [HttpGet, Route("GetAccountStatementForSuperAdmin")]
        public async Task<CommonReturnResponse> GetAccountStatementForSuperAdmin(int AdminId)
        {
            return await _commonService.GetAccountStatementForSuperAdminAsync(AdminId);
        }
    }
}
