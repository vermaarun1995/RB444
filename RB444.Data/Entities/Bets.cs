namespace RB444.Data.Entities
{
    public partial class Bets
    {
        public int Id { get; set; }
        public string MatchId { get; set; }
        public string MatchKey { get; set; }
        public string SeriesName { get; set; }
        public string TeamName { get; set; }
        public bool Back { get; set; }
        public string BackName { get; set; }
        public bool Lay { get; set; }
        public string LayName { get; set; }
        public long BackBet { get; set; }
        public long LayBet { get; set; }
        public long MatchFinishAmount { get; set; }
        public int UserId { get; set; }
        public int BetType { get; set; }
    }
}
