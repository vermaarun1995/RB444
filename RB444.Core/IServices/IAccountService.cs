using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface IAccountService
    {
        /// <summary>
        /// Update Assign Coin
        /// </summary>
        /// <param name="AssignCoin"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateAssignCoinAsync(long AssignCoin, int LoginUserId);

        /// <summary>
        /// Get User Roles.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetUserRolesAsync();

        /// <summary>
        /// Get All Users.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllUsers();

        /// <summary>
        /// Get Users who create by your parent user.
        /// </summary>
        /// <param name="ParentUserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllUsersByParentIdAsync(int ParentUserId);
    }
}
