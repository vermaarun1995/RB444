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
                if (type == 2)
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
                        Status = serieslistByDatabase.Count > 0 ? serieslistByDatabase.Where(x => x.tournamentId == item.SeriesId).FirstOrDefault() != null ? serieslistByDatabase.Where(x => x.tournamentId == item.SeriesId).Select(s => s.Status).FirstOrDefault() : true : true
                    };
                    serieslist.Add(series);

                    if (serieslistByDatabase.Count > 0 && type == 1)
                    {
                        foreach (var item2 in serieslistByDatabase)
                        {
                            if (item.SeriesName.Equals(item2.tournamentName) && item2.Status != true)
                            {
                                var itemToRemove = serieslist.FirstOrDefault(r => r.tournamentId == item2.tournamentId);
                                serieslist.Remove(itemToRemove);
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
                        marketId = item.marketId,
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

        public async Task<CommonReturnResponse> GetMatchEventsAsync(string marketId, long eventId, int SportId)
        {
            double totalMatched;
            string eventName = "";
            bool inPlay = false;
            var matchReturnResponse = new List<MatchReturnResponseNew>();
            var teamNameResponse = new TeamNameResponse();
            var eventModel = new EventModel();
            var teamSelectionIds = new List<TeamSelectionId>();
            eventModel.data = new Model.Model.Data();
            eventModel.data.matchOddsData = new List<MatchOddsData>();
            List<MatchOddsData> matchOddsDataList = new List<MatchOddsData>();
            Price price = new Price();
            try
            {
                matchReturnResponse = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}{1}", _configuration["ApiMatchOddsUrl"], marketId));

                teamNameResponse = await _requestServices.GetAsync<TeamNameResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                //string abc = _requestServices.GetMarketAsync(teamNameResponse.data[0].market_runner_json);
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

                List<RunnerOld> runnerList = new List<RunnerOld>();
                foreach (var item in matchReturnResponse[0].runners)
                {
                    List<Back> backList = new List<Back>();
                    List<Lay> layList = new List<Lay>();
                    backList.Add(new Back
                    {
                        price = item.ex.availableToBack[2].price,
                        size = item.ex.availableToBack[2].size
                    });
                    backList.Add(new Back
                    {
                        price = item.ex.availableToBack[1].price,
                        size = item.ex.availableToBack[1].size
                    });
                    backList.Add(new Back
                    {
                        price = item.ex.availableToBack[0].price,
                        size = item.ex.availableToBack[0].size
                    });

                    layList.Add(new Lay
                    {
                        price = item.ex.availableToLay[0].price,
                        size = item.ex.availableToLay[0].size
                    });
                    layList.Add(new Lay
                    {
                        price = item.ex.availableToLay[1].price,
                        size = item.ex.availableToLay[1].size
                    });
                    layList.Add(new Lay
                    {
                        price = item.ex.availableToLay[2].price,
                        size = item.ex.availableToLay[2].size
                    });

                    runnerList.Add(new RunnerOld
                    {
                        selectionId = item.selectionId,
                        runnerName = teamSelectionIds.Where(x => x.selectionId == item.selectionId).Select(y => y.teamName).FirstOrDefault(),
                        handicap = 0,
                        status = item.status,
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
                    eventName = runnerNames.eventName,
                    marketId = marketId,
                    exMarketId = marketId,
                    isSettlement = 0,
                    isScore = false,
                    isVoid = 0,
                    marketName = runnerNames.marketName,
                    marketType = runnerNames.marketType,
                    max = 25000,
                    min = 100,
                    tableFlag = runnerNames.marketType,
                    oddsType = runnerNames.marketType,
                    oddsData = new OddsData
                    {
                        betDelay = 5,
                        status = matchReturnResponse[0].status,
                        totalMatched = matchReturnResponse[0].totalMatched,
                        runners = runnerList
                    },
                });

                eventModel.data.matchOddsData = matchOddsDataList;
                eventModel.data.inPlay = matchReturnResponse[0].inplay;

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
            var matchReturnResponse = new MatchesReturnResponse();
            var matchReturnResponseNew = new List<MatchReturnResponseNew>();

            var sportsEventModelList = new List<SportsEventModel>();
            try
            {
                matchReturnResponse = await _requestServices.GetAsync<MatchesReturnResponse>(string.Format("{0}getmatches/{1}", _configuration["ApiKeyUrl"], SportId));
                foreach (var item in matchReturnResponse.data)
                {
                    matchReturnResponseNew = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}{1}", _configuration["ApiMatchOddsUrl"], item.marketId));
                    if (matchReturnResponseNew != null)
                    {
                        var sportsEventModel = new SportsEventModel
                        {
                            gameId = item.eventId.ToString(),
                            eventName = item.eventName,
                            eventDate = GetISTDateTime(item.eventDate),
                            marketId = item.marketId,
                            inPlay = matchReturnResponseNew[0].inplay.ToString(),
                            back11 = matchReturnResponseNew[0].runners[0].ex.availableToBack[0].price,
                            back1 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToBack[0].price : 0,
                            back12 = matchReturnResponseNew[0].runners[1].ex.availableToBack[0].price,
                            lay11 = matchReturnResponseNew[0].runners[0].ex.availableToLay[0].price,
                            lay1 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToLay[0].price : 0,
                            lay12 = matchReturnResponseNew[0].runners[1].ex.availableToLay[0].price,
                            eid = SportId.ToString(),
                            m1 = "",
                            f = "",
                            tv = item.MainTvurl,
                            vir = 0
                        };
                        sportsEventModelList.Add(sportsEventModel);
                    }
                }
                return new CommonReturnResponse
                {
                    Data = sportsEventModelList.OrderByDescending(y => y.inPlay).ToList(),
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
            var matchReturnResponse = new MatchesReturnResponse();
            var matchReturnResponseNew = new List<MatchReturnResponseNew>();
            try
            {
                matchReturnResponse = await _requestServices.GetAsync<MatchesReturnResponse>(string.Format("{0}getmatches/1", _configuration["ApiKeyUrl"]));
                foreach (var item in matchReturnResponse.data)
                {
                    matchReturnResponseNew = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}{1}", _configuration["ApiMatchOddsUrl"], item.marketId));
                    if (matchReturnResponseNew != null)
                    {
                        var sportsEventModel = new SportsEventModel
                        {
                            gameId = item.eventId.ToString(),
                            eventName = item.eventName,
                            eventDate = GetISTDateTime(item.eventDate),
                            marketId = item.marketId,
                            inPlay = matchReturnResponseNew[0].inplay.ToString(),
                            back11 = matchReturnResponseNew[0].runners[0].ex.availableToBack[0].price,
                            back1 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToBack[0].price : 0,
                            back12 = matchReturnResponseNew[0].runners[1].ex.availableToBack[0].price,
                            lay11 = matchReturnResponseNew[0].runners[0].ex.availableToLay[0].price,
                            lay1 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToLay[0].price : 0,
                            lay12 = matchReturnResponseNew[0].runners[1].ex.availableToLay[0].price,
                            eid = "1",
                            m1 = "",
                            f = "",
                            tv = item.MainTvurl,
                            vir = 0
                        };
                        sportsEventModelList.Add(sportsEventModel);
                    }
                }

                matchReturnResponse = await _requestServices.GetAsync<MatchesReturnResponse>(string.Format("{0}getmatches/2", _configuration["ApiKeyUrl"]));
                foreach (var item in matchReturnResponse.data)
                {
                    matchReturnResponseNew = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}{1}", _configuration["ApiMatchOddsUrl"], item.marketId));
                    if (matchReturnResponseNew != null)
                    {
                        var sportsEventModel = new SportsEventModel
                        {
                            gameId = item.eventId.ToString(),
                            eventName = item.eventName,
                            eventDate = GetISTDateTime(item.eventDate),
                            marketId = item.marketId,
                            inPlay = matchReturnResponseNew[0].inplay.ToString(),
                            back1 = matchReturnResponseNew[0].runners[0].ex.availableToBack[0].price,
                            back11 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToBack[0].price : 0,
                            back12 = matchReturnResponseNew[0].runners[1].ex.availableToBack[0].price,
                            lay1 = matchReturnResponseNew[0].runners[0].ex.availableToLay[0].price,
                            lay11 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToLay[0].price : 0,
                            lay12 = matchReturnResponseNew[0].runners[1].ex.availableToLay[0].price,
                            eid = "1",
                            m1 = "",
                            f = "",
                            tv = item.MainTvurl,
                            vir = 0
                        };
                        sportsEventModelList.Add(sportsEventModel);
                    }
                }

                matchReturnResponse = await _requestServices.GetAsync<MatchesReturnResponse>(string.Format("{0}getmatches/4", _configuration["ApiKeyUrl"]));
                foreach (var item in matchReturnResponse.data)
                {
                    matchReturnResponseNew = await _requestServices.GetAsync<List<MatchReturnResponseNew>>(string.Format("{0}{1}", _configuration["ApiMatchOddsUrl"], item.marketId));
                    if (matchReturnResponseNew != null)
                    {
                        var sportsEventModel = new SportsEventModel
                        {
                            gameId = item.eventId.ToString(),
                            eventName = item.eventName,
                            eventDate = GetISTDateTime(item.eventDate),
                            marketId = item.marketId,
                            inPlay = matchReturnResponseNew[0].inplay.ToString(),
                            back1 = matchReturnResponseNew[0].runners[0].ex.availableToBack[0].price,
                            back11 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToBack[0].price : 0,
                            back12 = matchReturnResponseNew[0].runners[1].ex.availableToBack[0].price,
                            lay1 = matchReturnResponseNew[0].runners[0].ex.availableToLay[0].price,
                            lay11 = item.market_runner_count == 3 ? matchReturnResponseNew[0].runners[2].ex.availableToLay[0].price : 0,
                            lay12 = matchReturnResponseNew[0].runners[1].ex.availableToLay[0].price,
                            eid = "1",
                            m1 = "",
                            f = "",
                            tv = item.MainTvurl,
                            vir = 0
                        };
                        sportsEventModelList.Add(sportsEventModel);
                    }
                }

                sportsInPlayEventList.sportsEventModelInPlay = sportsEventModelList.Where(x => Convert.ToBoolean(x.inPlay) == true).ToList();

                sportsInPlayEventList.sportsEventModelToday = sportsEventModelList.Where(x => x.eventDate.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")).OrderByDescending(y => y.inPlay).ToList();

                sportsInPlayEventList.sportsEventModelTommorow = sportsEventModelList.Where(x => x.eventDate.ToString("dd/MM/yyyy") == DateTime.Now.AddDays(1).ToString("dd/MM/yyyy")).OrderByDescending(y => y.inPlay).ToList();

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
