using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
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
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;

        public BetApiService(IBaseRepository baseRepository, IRequestServices requestServices, IConfiguration configuration)
        {
            _baseRepository = baseRepository;
            _requestServices = requestServices;
            _configuration = configuration;
        }

        public async Task<CommonReturnResponse> SaveBets(Bets model)
        {
            try
            {
                var sportList = await _baseRepository.GetListAsync<Sports>();
                int betDelayTime = sportList.Where(x => x.Id == model.SportId).Select(y => y.BetDelayTime).FirstOrDefault() * 1000;
                string betId = model.UserId.ToString() + DateTime.Now.ToString();
                betId = cEncryption.MD5Encryption(betId);
                model.BetId = betId;
                model.PlaceTime = DateTime.Now;
                model.MatchedTime = DateTime.Now.AddSeconds(5);
                model.SettleTime = model.MatchedTime;
                model.ResultAmount = 0;
                await Task.Delay(betDelayTime);
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

        public async Task<CommonReturnResponse> BetSettleAsync(long eventId, string marketId)
        {
            string sql = string.Empty;
            string _condition = string.Empty;
            var commonReturnResponse = new CommonReturnResponse();
            var matchReturnResponseList = new List<MatchReturnResponseNew>();
            var matchReturnResponse = new MatchReturnResponseNew();
            var betList = new List<Bets>();
            var teamSelectionIds = new List<TeamSelectionId>();
            try
            {
                matchReturnResponseList = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}getresultdata/{1}", _configuration["ApiKeyUrl"], marketId));
                matchReturnResponse = matchReturnResponseList.FirstOrDefault();
                if (matchReturnResponse.status.ToLower() == "closed")
                {
                    commonReturnResponse = await _requestServices.GetAsync<CommonReturnResponse>(string.Format("{0}common/GetBetDataListByMarketId?MarketId={1}", _configuration["MyApiKeyUrl"], marketId));
                    if (commonReturnResponse.IsSuccess && commonReturnResponse.Data != null)
                    {
                        betList=jsonParser.ParsJson<List<Bets>>(Convert.ToString(commonReturnResponse.Data));
                        var teamNameResponse = await _requestServices.GetAsync<TeamNameResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], betList.FirstOrDefault().SportId));
                        var runnerNames = teamNameResponse.data.Where(x => x.marketId == marketId).FirstOrDefault();

                        if (runnerNames != null)
                        {
                            teamSelectionIds.Add(new TeamSelectionId
                            {
                                teamName = runnerNames.runnerName1,
                                selectionId = runnerNames.selectionId1
                            });
                            teamSelectionIds.Add(new TeamSelectionId
                            {
                                teamName = runnerNames.runnerName2,
                                selectionId = runnerNames.selectionId2
                            });
                            if (runnerNames.selectionId3 != 0 && runnerNames.runnerName3 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName3,
                                    selectionId = runnerNames.selectionId3
                                });
                            }
                            if (runnerNames.selectionId4 != 0 && runnerNames.runnerName4 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName4,
                                    selectionId = runnerNames.selectionId4
                                });
                            }
                            if (runnerNames.selectionId5 != 0 && runnerNames.runnerName5 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName5,
                                    selectionId = runnerNames.selectionId5
                                });
                            }
                            if (runnerNames.selectionId6 != 0 && runnerNames.runnerName6 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName6,
                                    selectionId = runnerNames.selectionId6
                                });
                            }
                            if (runnerNames.selectionId7 != 0 && runnerNames.runnerName7 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName7,
                                    selectionId = runnerNames.selectionId7
                                });
                            }
                            if (runnerNames.selectionId8 != 0 && runnerNames.runnerName8 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName8,
                                    selectionId = runnerNames.selectionId8
                                });
                            }
                            if (runnerNames.selectionId9 != 0 && runnerNames.runnerName9 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName9,
                                    selectionId = runnerNames.selectionId9
                                });
                            }
                            if (runnerNames.selectionId10 != 0 && runnerNames.runnerName10 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName10,
                                    selectionId = runnerNames.selectionId10
                                });
                            }
                            if (runnerNames.selectionId11 != 0 && runnerNames.runnerName11 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName11,
                                    selectionId = runnerNames.selectionId11
                                });
                            }
                            if (runnerNames.selectionId12 != 0 && runnerNames.runnerName12 != "")
                            {
                                teamSelectionIds.Add(new TeamSelectionId
                                {
                                    teamName = runnerNames.runnerName12,
                                    selectionId = runnerNames.selectionId12
                                });
                            }
                        }

                        foreach (var runner in matchReturnResponse.runners)
                        {
                            bool isDraw = false;
                            foreach (var item in betList.Where(x => x.SelectionId == runner.selectionId).ToList())
                            {
                                var selectionName = teamSelectionIds.Where(x => x.selectionId == runner.selectionId).FirstOrDefault().teamName;
                                if (item.Selection.ToLower().Contains("draw") && selectionName.ToLower().Contains("draw"))
                                {
                                    isDraw = true;
                                }
                                
                                if (isDraw && runner.status.ToLower() == "winner")
                                {

                                    item.ResultType = 3;
                                    item.UpdatedDate = DateTime.Now;
                                    //var resultAmout = (item.AmountStake * item.OddsRequest) - item.AmountStake;
                                    item.ResultAmount = 0;
                                    await _baseRepository.UpdateAsync(item);
                                   
                                }
                                else if (runner.status.ToLower() == "winner")
                                {
                                    item.IsSettlement = 1;
                                    item.ResultType = 1;
                                    item.UpdatedDate = DateTime.Now;
                                    //var resultAmout = (item.AmountStake * item.OddsRequest) - item.AmountStake;
                                    item.ResultAmount = (item.AmountStake * item.OddsRequest) - item.AmountStake;
                                    await _baseRepository.UpdateAsync(item);
                                }
                                else if (runner.status.ToLower() == "loser")
                                {
                                    item.IsSettlement = 1;
                                    item.ResultType = 2;
                                    item.UpdatedDate = DateTime.Now;
                                    item.ResultAmount = -((item.AmountStake * item.OddsRequest) - item.AmountStake);
                                    await _baseRepository.UpdateAsync(item);
                                }
                            }
                        }
                        _baseRepository.Commit();
                    }
                }

                return new CommonReturnResponse
                {
                    Data = null,
                    Message = MessageStatus.Success,
                    IsSuccess = true,
                    Status = ResponseStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _baseRepository.Rollback();
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }
    }
}