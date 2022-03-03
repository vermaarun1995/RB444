using RB444.Data.Entities;
using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface IOtherSetting
    {
        /// <summary>
        /// Add or Update Logo.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> AddUpdateLogoAsync(Logo model);

        /// <summary>
        /// Add or Update News.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> AddUpdateNewsAsync(News model);

        /// <summary>
        /// Add or Update Slider.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> AddUpdateSliderAsync(Slider model);
    }
}
