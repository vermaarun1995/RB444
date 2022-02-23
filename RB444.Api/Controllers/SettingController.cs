using Microsoft.AspNetCore.Mvc;
using RB444.Core.IServices;
using RB444.Data.Entities;
using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Api.Controllers
{
    [Route("api/Setting")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpPost, Route("UpdateSportsStatus")]
        public async Task<CommonReturnResponse> UpdateSportsStatus(SportsSetting sportsSetting)
        {
            return await _settingService.UpdateSportsStatusAsync(sportsSetting);
        }

        [HttpPost, Route("UpdateSportsLimit")]
        public async Task<CommonReturnResponse> UpdateSportsLimit(SportsSetting sportsSetting)
        {
            return await _settingService.UpdateSportsLimitAsync(sportsSetting);
        }
    }
}
