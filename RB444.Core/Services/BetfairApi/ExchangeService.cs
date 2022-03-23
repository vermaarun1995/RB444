using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RB444.Core.IServices;
using RB444.Core.IServices.BetfairApi;
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
using static RB444.Core.ServiceHelper.CommonFun;

namespace RB444.Core.Services.BetfairApi
{
    public class ExchangeService : IExchangeService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;
        CommonFun commonFun = new CommonFun();

        public ExchangeService(IBaseRepository baseRepository, IRequestServices requestServices, IConfiguration configuration)
        {
            _baseRepository = baseRepository;
            _requestServices = requestServices;
            _configuration = configuration;
        }

        public async Task<CommonReturnResponse> GetSportsAsync(int type)
        {
            IDictionary<string, object> _keyValues = null;
            var sports = new List<Sports>();
            try
            {
                if (type == 1)
                {
                    sports = await _baseRepository.GetListAsync<Sports>();
                }
                else
                {
                    _keyValues = new Dictionary<string, object> { { "Status", 1 } };
                    sports = (await _baseRepository.SelectAsync<Sports>(_keyValues)).ToList();
                }
                return new CommonReturnResponse
                {
                    Data = sports,
                    Message = sports.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sports.Count > 0,
                    Status = sports.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
        }

        public async Task<CommonReturnResponse> GetSeriesListAsync(int SportId, int type)
        {
            IDictionary<string, object> _keyValues = null;
            var seriesReturnResponse = new SeriesReturnResponse();
            List<SeriesDataByApi> seriesDataByApis = null;
            List<SeriesDataByApi> seriesDistinctDataByApis = new List<SeriesDataByApi>();
            List<Series> serieslistByDatabase = null;
            List<Series> serieslist = new List<Series>();
            try
            {
                seriesReturnResponse = await _requestServices.GetAsync<SeriesReturnResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                seriesDataByApis = seriesReturnResponse.data;
                foreach (var itemSeries in seriesDataByApis)
                {
                    if (!seriesDistinctDataByApis.Any(x => x.SeriesId == itemSeries.SeriesId))
                        seriesDistinctDataByApis.Add(itemSeries);
                }
                _keyValues = new Dictionary<string, object> { { "SportId", SportId } };
                serieslistByDatabase = (await _baseRepository.SelectAsync<Series>(_keyValues)).ToList();

                foreach (var item in seriesDistinctDataByApis)
                {
                    var series = new Series
                    {
                        tournamentId = Convert.ToInt64(item.SeriesId),
                        tournamentName = item.SeriesName,
                        SportId = SportId,
                        Status = serieslistByDatabase.Where(x => x.tournamentId == item.SeriesId).Select(s => s.Status).FirstOrDefault()

                    };
                    serieslist.Add(series);

                    if (serieslistByDatabase.Count > 0 && type == 1)
                    {
                        foreach (var item2 in serieslistByDatabase)
                        {
                            if (item.SeriesName.Equals(item2.tournamentName) && item2.Status != true)
                            {
                                serieslist.Remove(item2);
                            }
                        }
                    }
                }

                return new CommonReturnResponse
                {
                    Data = serieslist,
                    Message = serieslist.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = serieslist.Count > 0,
                    Status = serieslist.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (seriesDataByApis != null) { seriesDataByApis = null; } }
        }

        public async Task<CommonReturnResponse> GetMatchesListAsync(int SportId, long SeriesId, int type)
        {
            IDictionary<string, object> _keyValues = null;
            var matchReturnResponse = new MatchesReturnResponse();
            List<MatchesDataByApi> matchDataByApis = null;
            List<Matches> matcheslistByDatabase = null;
            List<Matches> matcheslist = new List<Matches>();
            try
            {
                matchReturnResponse = await _requestServices.GetAsync<MatchesReturnResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                matchDataByApis = matchReturnResponse.data;
                matchDataByApis = matchDataByApis.Where(x => x.SeriesId == SeriesId).ToList();

                _keyValues = new Dictionary<string, object> { { "SportId", SportId }, { "SeriesId", SeriesId } };
                matcheslistByDatabase = (await _baseRepository.SelectAsync<Matches>(_keyValues)).ToList();

                foreach (var item in matchDataByApis)
                {
                    var matches = new Matches
                    {
                        SeriesId = Convert.ToInt64(item.SeriesId),
                        eventId = item.eventId,
                        eventName = item.eventName,
                        MatchDate = item.eventDate,
                        SportId = SportId,
                        Status = matcheslistByDatabase.Count > 0 ? matcheslistByDatabase.Where(x => x.SeriesId == item.SeriesId && x.eventId == item.eventId).Select(s => s.Status).Count() > 0 ? matcheslistByDatabase.Where(x => x.SeriesId == item.SeriesId && x.eventId == item.eventId).Select(s => s.Status).FirstOrDefault() : true : true

                    };
                    matcheslist.Add(matches);

                    if (matcheslistByDatabase.Count > 0 && type == 1)
                    {
                        foreach (var item2 in matcheslistByDatabase)
                        {
                            if (item.eventName.Equals(item2.eventName) && item2.Status != true)
                            {
                                matcheslist.Remove(item2);
                            }
                        }
                    }
                }

                return new CommonReturnResponse
                {
                    Data = matcheslist,
                    Message = matcheslist.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = matcheslist.Count > 0,
                    Status = matcheslist.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (matchDataByApis != null) { matchDataByApis = null; } }
        }

        public async Task<CommonReturnResponse> GetMatchEventsAsync(long eventId, int SportId)
        {
            double totalMatched;
            string eventName = "";
            bool inPlay = false;
            var matchReturnResponse = new MatchesReturnResponse();
            var eventReturnResponse = new EventReturnResponse();
            var eventModel = new EventModel();
            eventModel.data = new Model.Model.Data();
            eventModel.data.matchOddsData = new List<MatchOddsData>();
            List<MatchOddsData> matchOddsDataList = new List<MatchOddsData>();
            Price price = new Price();
            try
            {
                matchReturnResponse = await _requestServices.GetAsync<MatchesReturnResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                eventName = matchReturnResponse.data.Where(x => x.eventId == eventId).Select(y => y.eventName).FirstOrDefault();

                eventReturnResponse = await _requestServices.GetAsync<EventReturnResponse>(string.Format("{0}getdata/{1}", _configuration["DiamondApiKeyUrl"], eventId));
                if (eventReturnResponse.t1 != null)
                {
                    var eventCnt = eventReturnResponse.t1.Count;
                    for (int i = 0; i < eventCnt; i++)
                    {
                        List<Runner> runnerList = new List<Runner>();
                        totalMatched = Math.Round(Convert.ToDouble(eventReturnResponse.t1[i][0].bs1) + Convert.ToDouble(eventReturnResponse.t1[i][0].bs2) + Convert.ToDouble(eventReturnResponse.t1[i][0].bs3) + Convert.ToDouble(eventReturnResponse.t1[i][0].ls1) + Convert.ToDouble(eventReturnResponse.t1[i][0].ls2) + Convert.ToDouble(eventReturnResponse.t1[i][0].ls3) + Convert.ToDouble(eventReturnResponse.t1[i][1].bs1) + Convert.ToDouble(eventReturnResponse.t1[i][1].bs2) + Convert.ToDouble(eventReturnResponse.t1[i][1].bs3) + Convert.ToDouble(eventReturnResponse.t1[i][1].ls1) + Convert.ToDouble(eventReturnResponse.t1[i][1].ls2) + Convert.ToDouble(eventReturnResponse.t1[i][1].ls3), 2);

                        inPlay = Convert.ToBoolean(eventReturnResponse.t1[i][0].iplay);

                        for (int j = 0; j < eventReturnResponse.t1[i].Count; j++)
                        {                            
                            List<Back> backList = new List<Back>();
                            List<Lay> layList = new List<Lay>();
                            backList.Add(new Back
                            {
                                price = Convert.ToDouble(eventReturnResponse.t1[i][j].b1),
                                size = Convert.ToDouble(eventReturnResponse.t1[i][j].bs1)
                            });

                            backList.Add(new Back
                            {
                                price = Convert.ToDouble(eventReturnResponse.t1[i][j].b2),
                                size = Convert.ToDouble(eventReturnResponse.t1[i][j].bs2)
                            });

                            backList.Add(new Back
                            {
                                price = Convert.ToDouble(eventReturnResponse.t1[i][j].b3),
                                size = Convert.ToDouble(eventReturnResponse.t1[i][j].bs3)
                            });
                            layList.Add(new Lay
                            {
                                price = Convert.ToDouble(eventReturnResponse.t1[i][j].l1),
                                size = Convert.ToDouble(eventReturnResponse.t1[i][j].ls1)
                            });

                            layList.Add(new Lay
                            {
                                price = Convert.ToDouble(eventReturnResponse.t1[i][j].l2),
                                size = Convert.ToDouble(eventReturnResponse.t1[i][j].ls2)
                            });

                            layList.Add(new Lay
                            {
                                price = Convert.ToDouble(eventReturnResponse.t1[i][j].l3),
                                size = Convert.ToDouble(eventReturnResponse.t1[i][j].ls3)
                            });
                            runnerList.Add(new Runner
                            {
                                selectionId = Convert.ToInt32(eventReturnResponse.t1[i][j].sid),
                                runnerName = eventReturnResponse.t1[i][j].nat,
                                handicap = 0,
                                status = eventReturnResponse.t1[i][j].status,
                                price = new Price
                                {
                                    back = backList,
                                    lay = layList
                                }
                            });
                        }
                        matchOddsDataList.Add(new MatchOddsData
                        {
                            exEventId = eventId.ToString(),
                            eventName = eventName,
                            marketId = eventReturnResponse.t1[i][0].mid,
                            isSettlement = 0,
                            isScore = false,
                            isVoid = 0,
                            marketName = eventReturnResponse.t1[i][0].mname,
                            marketType = eventReturnResponse.t1[i][0].gtype,
                            max = 25000,
                            min = 100,
                            tableFlag = eventReturnResponse.t1[i][0].mname,
                            oddsType = eventReturnResponse.t1[i][0].mname,
                            oddsData = new OddsData
                            {                                
                                betDelay = 5,
                                status = eventReturnResponse.t1[i][0].mstatus,
                                totalMatched = totalMatched.ToString(),
                                runners = runnerList
                            },                            
                        });
                    }

                    eventModel.data.matchOddsData = matchOddsDataList;
                    eventModel.data.delayed = Convert.ToBoolean(eventReturnResponse.delayed);
                    eventModel.data.inPlay = inPlay;
                }
                else
                {
                    eventModel.data.matchOddsData = null;
                }


                return new CommonReturnResponse
                {
                    Data = eventModel,
                    Message = MessageStatus.Success,
                    IsSuccess = true,
                    Status = ResponseStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            //finally { if (matchDataByApis != null) { matchDataByApis = null; } }
        }

        public async Task<CommonReturnResponse> GetSportsEventsAsync(int SportId)
        {
            string sportNameForApi = "";
            if (SportId == 1)
            {
                sportNameForApi = "getsoccermatches";
            }
            else if (SportId == 2)
            {
                sportNameForApi = "gettennismatches";
            }
            else if (SportId == 4)
            {
                sportNameForApi = "getcricketmatches";
            }

            var sportsEventModelList = new List<SportsEventModel>();
            try
            {
                sportsEventModelList = await _requestServices.GetAsync<List<SportsEventModel>>(string.Format("http://marketsarket.in:3000/{0}", sportNameForApi));

                return new CommonReturnResponse
                {
                    Data = sportsEventModelList,
                    Message = sportsEventModelList.Count > 0 ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sportsEventModelList.Count > 0,
                    Status = sportsEventModelList.Count > 0 ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (sportsEventModelList != null) { sportsEventModelList = null; } }
        }

        public async Task<CommonReturnResponse> GetSportsInPlayEventsAsync()
        {
            var sportsEventModelList = new List<SportsEventModel>();
            var sportsInPlayEventList = new SportInPlayEventModel();
            try
            {
                string todayDate = commonFun.GetMonthName(DateTime.Now.Month) + " " + DateTime.Now.Day.ToString() + " " + DateTime.Now.Year.ToString();
                string tommorowDate = commonFun.GetMonthName(DateTime.Now.Month) + " " + (DateTime.Now.Day + 1).ToString() + " " + DateTime.Now.Year.ToString();
                sportsEventModelList = await _requestServices.GetAsync<List<SportsEventModel>>(string.Format("http://marketsarket.in:3000/{0}", "getsoccermatches"));
                sportsEventModelList.AddRange(await _requestServices.GetAsync<List<SportsEventModel>>(string.Format("http://marketsarket.in:3000/{0}", "gettennismatches")));
                sportsEventModelList.AddRange(await _requestServices.GetAsync<List<SportsEventModel>>(string.Format("http://marketsarket.in:3000/{0}", "getcricketmatches")));

                sportsInPlayEventList.sportsEventModelInPlay = sportsEventModelList.Where(x => Convert.ToBoolean(x.inPlay) == true).ToList();

                sportsInPlayEventList.sportsEventModelToday = sportsEventModelList.Where(x => x.eventName.Split('/')[1].Trim().Substring(0, 11) == todayDate).ToList();

                sportsInPlayEventList.sportsEventModelTommorow = sportsEventModelList.Where(x => x.eventName.Split('/')[1].Trim().Substring(0, 11) == tommorowDate).ToList();

                return new CommonReturnResponse
                {
                    Data = sportsInPlayEventList,
                    Message = sportsInPlayEventList != null ? MessageStatus.Success : MessageStatus.NoRecord,
                    IsSuccess = sportsInPlayEventList != null,
                    Status = sportsInPlayEventList != null ? ResponseStatusCode.OK : ResponseStatusCode.NOTFOUND
                };
            }
            catch (Exception ex)
            {
                //_logger.LogException("Exception : AircraftService : GetAircarftDetailsAsync()", ex);
                return new CommonReturnResponse { Data = null, Message = ex.InnerException != null ? ex.InnerException.Message : ex.Message, IsSuccess = false, Status = ResponseStatusCode.EXCEPTION };
            }
            finally { if (sportsEventModelList != null) { sportsEventModelList = null; } }
        }
    }
}
