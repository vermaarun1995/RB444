using RB444.Core.IServices;
using RB444.Core.ServiceHelper;
using RB444.Data.Entities;
using RB444.Data.Repository;
using RB444.Model.Model;
using RB444.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB444.Core.Services
{
    public class MarketWatchService : IMarketWatchService
    {
        private readonly IBaseRepository _baseRepository;

        public MarketWatchService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CommonReturnResponse> GetBetHistory(int sportId, int userId)
        {

            try
            {
                var _condition = string.Format(@"UserId = {0} and SportId = {1}", userId, sportId);

                var sql = string.Format(@"select  u.FullName ,b.Event,b.OddsType,b.OddsRequest,b.AmountStake,b.ResultType,b.ResultAmount,b.Selection as Runner  from Bets b 
                            join Users u on u.Id=b.UserId 
                            where  {0}", _condition);


                var betList = (await _baseRepository.QueryAsync<MarketWatchVM>(sql)).ToList();
                return new CommonReturnResponse
                {
                    Data = betList,
                    Message = betList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = betList.Count > 0,
                    Status = betList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AccountService : DeleteUserVisaInfoAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
    }
}
