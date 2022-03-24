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
    public class BetApiService : IBetApiService
    {
        private readonly IBaseRepository _baseRepository;

        public BetApiService(IBaseRepository baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task<CommonReturnResponse> SaveBets(Bets model)
        {
            try
            {
                string betId = model.UserId.ToString() + model.PlaceTime;
                betId = cEncryption.MD5Encryption(betId);
                model.BetId = betId;
                model.PlaceTime = DateTime.Now;
                model.MatchedTime = DateTime.Now.AddSeconds(5);
                model.SettleTime = model.MatchedTime;
                model.ResultAmount = 0;
                var _result = await _baseRepository.InsertAsync(model);
                if (_result > 0) { _baseRepository.Commit(); } else { _baseRepository.Rollback(); }
                return new CommonReturnResponse { Data = _result > 0, Message = _result > 0 ? MessageStatus.Create : MessageStatus.Error, IsSuccess = _result > 0, Status = _result > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };

            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                //_logger.LogException("Exception : CurrencyService : AddUpdatedAsync()", ex);
                return new CommonReturnResponse { Data = false, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetBetHistoryAsync(UserBetsHistory model)
        {
            string sql = string.Empty;
            string _condition = string.Empty;
            UserBetPagination userBetPagination = new UserBetPagination();
            try
            {
                _condition = string.Format(@"UserId = {0} and Status = 1 and SportId = {1} and IsSettlement = {2}", model.UserId, model.SportId, model.IsSettlement);
                int Skip = ((model.PageNumber - 1) * model.PageSize + 1) - 1;
                string date = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                if (model.TodayHistory == true)
                {
                    sql = string.Format(@"select top {0} * from (select *,ROW_NUMBER() OVER (ORDER BY id) AS ROW_NUM from Bets) x where PlaceTime BETWEEN '{1} 00:00:00.000' AND '{1} 23:59:59.998' and {2} and ROW_NUM>{3}", model.PageSize, date, _condition, Skip);
                }
                else
                {
                    sql = string.Format(@"select top {0} * from (select *,ROW_NUMBER() OVER (ORDER BY id) AS ROW_NUM from Bets) x where PlaceTime BETWEEN '{1} {2}:00.000' AND '{3} {4}:59.998' and {5} and ROW_NUM>{6}", model.PageSize, model.StartDate, model.StartTime, model.EndDate, model.EndTime, _condition, Skip);
                }
                userBetPagination.betList = (await _baseRepository.QueryAsync<Bets>(sql)).ToList();
                userBetPagination.TotalRecord = await _baseRepository.RecordCountAsync<Bets>("where " + _condition);

                if (userBetPagination.betList != null && userBetPagination.betList.Count > 0)
                {
                    if (model.PageNumber > 1) { userBetPagination.PreviousPage = model.PageNumber - 1; }//previous page no.
                    int currentPageSize = model.PageNumber * model.PageSize;
                    if (currentPageSize != userBetPagination.TotalRecord && currentPageSize <= userBetPagination.TotalRecord) { userBetPagination.NextPage = model.PageNumber + 1; }//next page no.
                    if (currentPageSize < userBetPagination.TotalRecord) { userBetPagination.ShowPageInfo = $"{(currentPageSize - model.PageSize + 1)} - {currentPageSize}"; }//show page info list
                    else { userBetPagination.ShowPageInfo = $"{(currentPageSize - model.PageSize + 1)} - {userBetPagination.TotalRecord}"; }
                }

                return new CommonReturnResponse
                {
                    Data = userBetPagination,
                    Message = userBetPagination != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = userBetPagination != null,
                    Status = userBetPagination != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

       
        //string query = @"select * from Vendors where id = @id and isdeleted = 0 and isactive = 1;";
        //query += @"select * from VendorServiceMapping where vendor_id = @id and isdeleted = 0 and isactive = 1;";
        //            query += @"select * from VendorMembershipMapping where vendor_id = @id and isdeleted = 0 and isactive = 1;";

        //            var result = await _baseRepository.GetQueryMultipleAsync(query, new { id = id }, gr => gr.Read<VendorVM>(), gr => gr.Read<VendorServiceMapping>(), gr => gr.Read<VendorMembershipMapping>());
        //vendorVM = (result[0] as List<VendorVM>).FirstOrDefault();
        //            if (vendorVM != null)
        //            {
        //                vendorVM.vendorServiceMappingList = (result[1] as List<VendorServiceMapping>).ToList();
        //vendorVM.vendorMembershipMappingList = (result[2] as List<VendorMembershipMapping>).ToList();
    }
}