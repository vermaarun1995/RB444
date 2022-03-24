using RB444.Models.Model;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface IAccountService
    {
        /// <summary>
        /// Get Opening Balance.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetOpeningBalanceAsync(int UserId);

        /// <summary>
        /// Update Assign Coin
        /// </summary>
        /// <param name="AssignCoin"></param>
        /// <param name="LoginUserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateAssignCoinAsync(long AssignCoin, int LoginUserId);

        /// <summary>
        /// Deposit Assign Coin.
        /// </summary>
        /// <param name="assignCoin"></param>
        /// <param name="parentId"></param>
        /// <param name="userId"></param>
        /// <param name="UserRoleId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> DepositAssignCoinAsync(long assignCoin, int parentId, int userId, int UserRoleId);

        /// <summary>
        /// Get User Roles.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetUserRolesAsync();

        /// <summary>
        /// Get All Users.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetAllUsers(int RoleId, int LoginUserId);

        /// <summary>
        /// Get Users who create by your parent user.
        /// </summary>
        /// <param name="LoginUserId"></param>
        /// <param name="RoleId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetUsersByParentIdAsync(int LoginUserId, int RoleId, int UserId);

        /// <summary>
        /// Get User Detail.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> GetUserDetailAsync(int UserId);

        /// <summary>
        /// Update user detail.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<CommonReturnResponse> UpdateUserDetailAsync(string query);
    }
}
