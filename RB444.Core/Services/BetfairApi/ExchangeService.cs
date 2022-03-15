﻿using Microsoft.Extensions.Configuration;
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

namespace RB444.Core.Services.BetfairApi
{
    public class ExchangeService : IExchangeService
    {
        private readonly IBaseRepository _baseRepository;
        private readonly IRequestServices _requestServices;
        private readonly IConfiguration _configuration;

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

        public async Task<CommonReturnResponse> GetMatchEventsAsync(long eventId)
        {
            double totalMatched;
            var eventReturnResponse = new EventReturnResponse();
            var eventModel = new EventModel();
            eventModel.data = new Model.Model.Data();
            eventModel.data.matchOddsData = new List<MatchOddsData>();
            List<MatchOddsData> matchOddsDataList = new List<MatchOddsData>();
            List<Runner> runnerList = new List<Runner>();
            Price price = new Price();
            List<Back> backList = new List<Back>();
            List<Lay> layList = new List<Lay>();
            List<Back> back2List = new List<Back>();
            List<Lay> lay2List = new List<Lay>();
            try
            {
                eventReturnResponse = await _requestServices.GetAsync<EventReturnResponse>(string.Format("{0}getdata/{1}", _configuration["DiamondApiKeyUrl"], eventId));

                totalMatched = Math.Round(Convert.ToDouble(eventReturnResponse.t1[0][0].bs1) + Convert.ToDouble(eventReturnResponse.t1[0][0].bs2) + Convert.ToDouble(eventReturnResponse.t1[0][0].bs3) + Convert.ToDouble(eventReturnResponse.t1[0][0].ls1) + Convert.ToDouble(eventReturnResponse.t1[0][0].ls2) + Convert.ToDouble(eventReturnResponse.t1[0][0].ls3) + Convert.ToDouble(eventReturnResponse.t1[0][1].bs1) + Convert.ToDouble(eventReturnResponse.t1[0][1].bs2) + Convert.ToDouble(eventReturnResponse.t1[0][1].bs3) + Convert.ToDouble(eventReturnResponse.t1[0][1].ls1) + Convert.ToDouble(eventReturnResponse.t1[0][1].ls2) + Convert.ToDouble(eventReturnResponse.t1[0][1].ls3), 2);

                backList.Add(new Back
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][0].b1),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][0].bs1)
                });

                backList.Add(new Back
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][0].b2),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][0].bs2)
                });

                backList.Add(new Back
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][0].b3),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][0].bs3)
                });

                back2List.Add(new Back
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][1].b1),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][1].bs1)
                });

                back2List.Add(new Back
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][1].b2),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][1].bs2)
                });

                back2List.Add(new Back
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][1].b3),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][1].bs3)
                });

                layList.Add(new Lay
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][0].l1),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][0].ls1)
                });

                layList.Add(new Lay
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][0].l2),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][0].ls2)
                });

                layList.Add(new Lay
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][0].l3),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][0].ls3)
                });

                lay2List.Add(new Lay
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][1].l1),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][1].ls1)
                });

                lay2List.Add(new Lay
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][1].l2),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][1].ls2)
                });

                lay2List.Add(new Lay
                {
                    price = Convert.ToDouble(eventReturnResponse.t1[0][1].l3),
                    size = Convert.ToDouble(eventReturnResponse.t1[0][1].ls3)
                });

                runnerList.Add(new Runner
                {
                    selectionId = Convert.ToInt32(eventReturnResponse.t1[0][0].sid),
                    handicap = 0,
                    status = eventReturnResponse.t1[0][0].status,
                    price = new Price
                    {
                        back = backList,
                        lay = layList
                    }
                });
                runnerList.Add(new Runner
                {
                    selectionId = Convert.ToInt32(eventReturnResponse.t1[0][1].sid),
                    handicap = 0,
                    status = eventReturnResponse.t1[0][1].status,
                    price = new Price
                    {
                        back = back2List,
                        lay = lay2List
                    }
                });

                matchOddsDataList.Add(new MatchOddsData
                {
                    exEventId = eventId.ToString(),
                    marketId = eventReturnResponse.t1[0][0].mid,
                    isSettlement = 0,
                    isScore = false,
                    isVoid = 0,
                    marketName = eventReturnResponse.t1[0][0].mname,
                    marketType = eventReturnResponse.t1[0][0].gtype,
                    max = 25000,
                    min = 100,
                    tableFlag = eventReturnResponse.t1[0][0].mname,
                    oddsType = eventReturnResponse.t1[0][0].mname,
                    oddsData = new OddsData
                    {
                        inPlay = Convert.ToBoolean(eventReturnResponse.t1[0][0].iplay),
                        betDelay = 5,
                        status = eventReturnResponse.t1[0][0].mstatus,
                        totalMatched = totalMatched.ToString(),
                        runners = runnerList
                    }
                });

                eventModel.data.matchOddsData = matchOddsDataList;

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
    }
}
