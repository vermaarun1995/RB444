using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface ICommonService
    {
        /// <summary>
        /// Get All user Roles.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllRolesAsync();

        /// <summary>
        /// Get All Slider.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllSliderAsync();

        /// <summary>
        /// Get News.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetNewsAsync();

        /// <summary>
        /// Get Logo.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetLogoAsync();
    }
}
