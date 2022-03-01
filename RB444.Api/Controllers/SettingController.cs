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

        [HttpPost, Route("AddOrUpdateSportsSetting")]
        public async Task<CommonReturnResponse> AddOrUpdateSportsSetting(SportsSetting sportsSetting)
        {
            return await _settingService.AddOrUpdateSportsSettingAsync(sportsSetting);
        }
    }
}
