using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface IMarketWatchService
    {
        /// <summary>
        /// Get User Roles.
        /// </summary>
        /// <returns></returns>
        Task<CommonReturnResponse> GetBetHistory(int sportId,int userId);

    }
}
