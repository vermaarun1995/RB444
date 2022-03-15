using System;
using System.Collections.Generic;
using System.Text;

namespace RB444.Model.Model
{
    public class EventModel
    {
        public Data data { get; set; }
        public Meta meta { get; set; }
    }

    // EventModel myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Back
    {
        public double price { get; set; }
        public double size { get; set; }
    }

    public class Lay
    {
        public double price { get; set; }
        public double size { get; set; }
    }

    public class Price
    {
        public List<Back> back { get; set; }
        public List<Lay> lay { get; set; }
    }

    public class Runner
    {
        public int selectionId { get; set; }
        public int handicap { get; set; }
        public string status { get; set; }
        public Price price { get; set; }
    }

    public class OddsData
    {
        public bool inPlay { get; set; }
        public bool preBet { get; set; }
        public int betDelay { get; set; }
        public string totalMatched { get; set; }
        public string status { get; set; }
        public bool crossMatching { get; set; }
        public List<Runner> runners { get; set; }
    }

    public class RunnersData
    {
        public string _7287668 { get; set; }
        public string _9633043 { get; set; }
    }

    public class MatchOddsData
    {
        public string exEventId { get; set; }
        public string eventName { get; set; }
        public string marketId { get; set; }
        public string exMarketId { get; set; }
        public string marketName { get; set; }
        public int min { get; set; }
        public int max { get; set; }
        public string marketType { get; set; }
        public string tableFlag { get; set; }
        public bool isScore { get; set; }
        public int isVoid { get; set; }
        public int isSettlement { get; set; }
        public OddsData oddsData { get; set; }
        public RunnersData runnersData { get; set; }
        public string oddsType { get; set; }
    }

    public class Data
    {
        public List<object> fancyData { get; set; }
        public List<MatchOddsData> matchOddsData { get; set; }
        public List<object> bookmakersData { get; set; }
        public List<object> sportsbookData { get; set; }
        public List<object> binaryData { get; set; }
        public bool isScore { get; set; }
    }

    public class Meta
    {
        public string message { get; set; }
        public int status_code { get; set; }
        public bool status { get; set; }
    }
}
