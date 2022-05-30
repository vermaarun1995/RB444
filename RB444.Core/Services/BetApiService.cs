using Hangfire;
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
using System.Threading.Tasks;

namespace RB444.Core.Services
{
    public class BetApiService : IBetApiService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        CommonFun commonFun = new CommonFun();
        string teamAmountStr = "";

        public BetApiService(IBaseRepository baseRepository, IRequestServices requestServices, IConfiguration configuration, IAccountService accountService)
        {
            _baseRepository = baseRepository;
            _requestServices = requestServices;
            _configuration = configuration;
            _accountService = accountService;
        }

        public async Task<CommonReturnResponse> SaveBets(Bets model)
        {
            try
            {
                var IsProperBet = await CheckProperBet(model);
                if (IsProperBet.Item1 == false)
                {
                    return new CommonReturnResponse { Data = null, Message = "Odds not match", IsSuccess = false, Status = ResponseStatusCode.OK };
                }
                else
                {
                    model.OddsRequest = IsProperBet.Item2;
                }
                var sportList = await _baseRepository.GetListAsync<Sports>();
                var minAmt = sportList.Where(x => x.Id == model.SportId).Select(y => y.MinOddLimit).FirstOrDefault();

                if (Convert.ToInt64(model.AmountStake) < minAmt)
                {
                    return new CommonReturnResponse { Data = null, Message = $"Minimum amount for bet is {minAmt}. Please select Amount greater than {minAmt}", IsSuccess = false, Status = ResponseStatusCode.OK };
                }
                var maxAmt = sportList.Where(x => x.Id == model.SportId).Select(y => y.MaxOddLimit).FirstOrDefault();
                if (Convert.ToInt64(model.AmountStake) > maxAmt)
                {
                    return new CommonReturnResponse { Data = null, Message = $"Maximum amount for bet is {minAmt}. Please select Amount less than {maxAmt}", IsSuccess = false, Status = ResponseStatusCode.OK };
                }
                var getBalance = await _accountService.GetOpeningBalanceAsync(model.UserId);
                if (getBalance.Data < model.AmountStake)
                {
                    return new CommonReturnResponse { Data = null, Message = $"Available balance is : {getBalance.Data}", IsSuccess = false, Status = ResponseStatusCode.OK };
                }
                int betDelayTime = sportList.Where(x => x.Id == model.SportId).Select(y => y.BetDelayTime).FirstOrDefault() * 1000;
                string betId = model.UserId.ToString() + DateTime.Now.ToString();
                betId = cEncryption.MD5Encryption(betId);
                model.BetId = betId;
                model.PlaceTime = DateTime.Now;
                model.MatchedTime = DateTime.Now.AddSeconds(5);
                model.SettleTime = model.MatchedTime;
                model.ResultAmount = 0;
                model.Status = true;
                await Task.Delay(betDelayTime);
                var _result = await _baseRepository.InsertAsync(model);
                if (_result > 0)
                {
                    _baseRepository.Commit();
                }
                else
                {
                    _baseRepository.Rollback();
                }
                return new CommonReturnResponse { Data = _result > 0, Message = _result > 0 ? "Bet Placed successfully." : MessageStatus.Error, IsSuccess = _result > 0, Status = _result > 0 ? ResponseStatusCode.OK : ResponseStatusCode.ERROR };
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
                userBetPagination.betList = (await _baseRepository.QueryAsync<Bets>(sql)).OrderByDescending(x => x.Id).ToList();
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

        public async Task<CommonReturnResponse> GetBackAndLayAmountAsync(int UserId, string marketId, int SportId)
        {
            string[] arr = new string[0];
            string sql = string.Empty;
            string _condition = string.Empty;
            UserBetPagination userBetPagination = new UserBetPagination();
            var teamSelectionIds = new List<TeamSelectionId>();
            var teamAmount = new List<TeamAmount>();
            var teamAmountFinal = new List<TeamAmount>();
            string oddsStr = "", responseStr = "";
            try
            {
                var teamNameResponse = await _requestServices.GetAsync<TeamNameResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                var runnerNames = teamNameResponse.data.Where(x => x.marketId == marketId).FirstOrDefault();

                teamSelectionIds = commonFun.GetTeamName(runnerNames);

                sql = $"select * from Bets where IsSettlement = 2 and MarketId='{marketId}' and userid = {UserId}";
                var betList = (await _baseRepository.QueryAsync<Bets>(sql)).ToList();
                if (betList.Count <= 0)
                {
                    if (teamSelectionIds.Count > 0)
                    {
                        for (int i = 0; i < teamSelectionIds.Count; i++)
                        {
                            teamAmount.Add(new TeamAmount
                            {
                                selectionId = Convert.ToInt64(teamSelectionIds[i].selectionId),
                                amount = 0
                            });
                        }
                    }
                    teamAmountStr = JsonConvert.SerializeObject(teamAmount);
                    teamAmountStr = teamAmountStr.Replace("\"selectionId\":", "\"");
                    teamAmountStr = teamAmountStr.Replace(",\"", "\"");
                    teamAmountStr = teamAmountStr.Replace("amount\"", "");
                    teamAmountStr = teamAmountStr.Replace("amount\"", "");
                    return new CommonReturnResponse
                    {
                        Data = teamAmountStr,
                        Message = userBetPagination != null ? MessageStatus.Success : MessageStatus.NoRecord,
                        IsSuccess = userBetPagination != null,
                        Status = userBetPagination != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                    };
                }

                if (teamSelectionIds.Count > 0)
                {
                    for (int i = 0; i < betList.Count; i++)
                    {
                        if (betList[i].Type == "back")
                        {
                            for (int j = 0; j < teamSelectionIds.Count; j++)
                            {
                                if (betList[i].SelectionId == teamSelectionIds[j].selectionId)
                                {
                                    oddsStr = oddsStr + teamSelectionIds[j].selectionId + ":" + ((betList[i].AmountStake * betList[i].OddsRequest) - betList[i].AmountStake).ToString() + "| ";
                                }
                                else
                                {
                                    oddsStr = oddsStr + teamSelectionIds[j].selectionId + ":" + (-betList[i].AmountStake).ToString() + "| ";
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < teamSelectionIds.Count; j++)
                            {
                                if (betList[i].SelectionId == teamSelectionIds[j].selectionId)
                                {
                                    oddsStr = oddsStr + teamSelectionIds[j].selectionId + ":" + (-((betList[i].AmountStake * betList[i].OddsRequest) - betList[i].AmountStake)).ToString() + "| ";
                                }
                                else
                                {
                                    oddsStr = oddsStr + teamSelectionIds[j].selectionId + ":" + (betList[i].AmountStake).ToString() + "| ";
                                }
                            }
                        }

                        oddsStr = oddsStr.Substring(0, oddsStr.Length - 2);
                        responseStr = responseStr + oddsStr + "@";
                        oddsStr = "";
                    }

                    responseStr = responseStr.Substring(0, responseStr.Length - 1);
                    var responseArr = responseStr.Split('@');
                    arr = new string[responseArr[0].Split('|').Count()];

                    for (int k = 0; k < responseArr.Length; k++)
                    {
                        var z = responseArr[k].Split('|');
                        for (int q = 0; q < z.Length; q++)
                        {
                            teamAmount.Add(new TeamAmount
                            {
                                selectionId = Convert.ToInt64(z[q].Split(':')[0]),
                                amount = Convert.ToDouble(z[q].Split(':')[1])
                            });
                        }
                    }
                }

                for (int kk = 0; kk < teamSelectionIds.Count; kk++)
                {
                    teamAmountFinal.Add(new TeamAmount
                    {
                        selectionId = teamSelectionIds[kk].selectionId,
                        amount = teamAmount.Where(x => x.selectionId == teamSelectionIds[kk].selectionId).Sum(y => y.amount)
                    });
                }

                responseStr = responseStr.Substring(0, responseStr.Length - 1);
                teamAmountStr = JsonConvert.SerializeObject(teamAmountFinal);
                teamAmountStr = teamAmountStr.Replace("\"selectionId\":", "\"");
                teamAmountStr = teamAmountStr.Replace(",\"", "\"");
                teamAmountStr = teamAmountStr.Replace("amount\"", "");
                return new CommonReturnResponse
                {
                    Data = teamAmountStr,
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

        public async Task<CommonReturnResponse> BetSettleAsync()
        {
            string sql = string.Empty, _condition = string.Empty, marketId = string.Empty, logStr = string.Empty;
            bool flg = false;
            var commonReturnResponse = new CommonReturnResponse();
            var matchReturnResponseList = new List<MatchReturnResponseNew>();
            var matchReturnResponse = new MatchReturnResponseNew();
            var betList = new List<Bets>();
            var teamSelectionIds = new List<TeamSelectionId>();
            var unsettleBetMarketIdList = new List<string>();
            try
            {
                sql = "select distinct MarketId from Bets where IsSettlement = 2";
                unsettleBetMarketIdList = (await _baseRepository.QueryAsync<Bets>(sql)).Select(x => x.MarketId).ToList();
                for (int i = 0; i < unsettleBetMarketIdList.Count; i++)
                {
                    marketId = unsettleBetMarketIdList[i];
                    matchReturnResponseList = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}getresultdata/{1}", _configuration["ApiKeyUrl"], marketId));
                    matchReturnResponse = matchReturnResponseList.FirstOrDefault();
                    if (matchReturnResponse != null)
                    {
                        if (matchReturnResponse.status.ToLower() == "closed")
                        {
                            logStr = logStr + "Settle match of MarketId = " + marketId + "  and Time of Settle = " + DateTime.Now.ToString() + " ";
                            commonReturnResponse = await _requestServices.GetAsync<CommonReturnResponse>(string.Format("{0}common/GetBetDataListByMarketId?MarketId={1}", _configuration["MyApiKeyUrl"], marketId));
                            if (commonReturnResponse.IsSuccess && commonReturnResponse.Data != null)
                            {
                                betList = jsonParser.ParsJson<List<Bets>>(Convert.ToString(commonReturnResponse.Data));
                                var teamNameResponse = await _requestServices.GetAsync<TeamNameResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], betList.FirstOrDefault().SportId));
                                var runnerNames = teamNameResponse.data.Where(x => x.marketId == marketId).FirstOrDefault();

                                teamSelectionIds = commonFun.GetTeamName(runnerNames);

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
                                            item.ResultAmount = 0;
                                            await _baseRepository.UpdateAsync(item);

                                        }
                                        else if (runner.status.ToLower() == "winner")
                                        {
                                            item.IsSettlement = 1;
                                            item.ResultType = 1;
                                            item.UpdatedDate = DateTime.Now;
                                            item.ResultAmount = (float?)((item.AmountStake * item.OddsRequest) - item.AmountStake);
                                            await _baseRepository.UpdateAsync(item);
                                        }
                                        else if (runner.status.ToLower() == "loser")
                                        {
                                            item.IsSettlement = 1;
                                            item.ResultType = 2;
                                            item.UpdatedDate = DateTime.Now;
                                            item.ResultAmount = (float?)-((item.AmountStake * item.OddsRequest) - item.AmountStake);
                                            await _baseRepository.UpdateAsync(item);
                                        }
                                    }
                                }
                                _baseRepository.Commit();
                            }
                        }
                    }
                }

                return new CommonReturnResponse
                {
                    Data = logStr.Length > 0 ? logStr : $"No Bet Settle {DateTime.Now.ToString()}",
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

        private async Task<Tuple<bool, double>> CheckProperBet(Bets model)
        {
            var matchReturnResponse = new List<MatchReturnResponseNew>();
            try
            {
                matchReturnResponse = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}{1}", _configuration["ApiMatchOddsUrl"], model.MarketId));
                foreach (var item in matchReturnResponse[0].runners)
                {
                    if (model.SelectionId == item.selectionId)
                    {
                        if (model.Type == "back")
                        {
                            if (model.OddsRequest <= item.ex.availableToBack[0].price)
                            {
                                return new Tuple<bool, double>(true, item.ex.availableToBack[0].price);
                            }
                            else
                            {
                                return new Tuple<bool, double>(false, 0);
                            }
                            //else if (model.OddsRequest == item.ex.availableToBack[1].price)
                            //{
                            //    flg = true;
                            //    break;
                            //}
                            //else if (model.OddsRequest == item.ex.availableToBack[2].price)
                            //{
                            //    flg = true;
                            //    break;
                            //}
                        }
                        if (model.Type == "lay")
                        {
                            if (model.OddsRequest >= item.ex.availableToLay[0].price)
                            {
                                return new Tuple<bool, double>(true, item.ex.availableToLay[0].price);
                            }
                            else
                            {
                                return new Tuple<bool, double>(false, 0);
                            }
                            //else if (model.OddsRequest == item.ex.availableToLay[1].price)
                            //{
                            //    flg = true;
                            //    break;
                            //}
                            //else if (model.OddsRequest == item.ex.availableToLay[2].price)
                            //{
                            //    flg = true;
                            //    break;
                            //}
                        }
                    }
                }
            }
            catch (Exception)
            {
                return new Tuple<bool, double>(false, 0);
            }
            return new Tuple<bool, double>(false, 0);
        }
    }
}