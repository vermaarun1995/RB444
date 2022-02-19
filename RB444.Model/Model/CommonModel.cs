using System;
using System.Collections.Generic;

namespace RB444.Models.Model
{
    public class CommonModel
    {
    }
    public class CommonReturnResponse
    {
        public Boolean IsSuccess { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }

    public class PriorityStatus
    {
        public int PriorityId { get; set; }
        public string Priority { get; set; }
        public string PriorityColourCode { get; set; }
    }

    public class SportsData
    {
        public string key { get; set; }
        public string group { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public bool has_outrights { get; set; }
    }

    public class Sports
    {
        public string group { get; set; }
    }

    public class Series
    {
        public string key { get; set; }
        public string group { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public bool has_outrights { get; set; }
    }

    public class Outcome
    {
        public string name { get; set; }
        public double price { get; set; }
    }

    public class Market
    {
        public string key { get; set; }
        public List<Outcome> outcomes { get; set; }
    }

    public class Bookmaker
    {
        public string key { get; set; }
        public string title { get; set; }
        public DateTime last_update { get; set; }
        public List<Market> markets { get; set; }
    }

    public class MatchList
    {
        public string id { get; set; }
        public string sport_key { get; set; }
        public string sport_title { get; set; }
        public DateTime commence_time { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }
    }

    public class MatchOdds
    {
        public string id { get; set; }
        public string sport_key { get; set; }
        public string sport_title { get; set; }
        public DateTime commence_time { get; set; }
        public string home_team { get; set; }
        public string away_team { get; set; }
        public List<Bookmaker> bookmakers { get; set; }
    }
}
