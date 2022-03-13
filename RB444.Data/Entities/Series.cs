namespace RB444.Data.Entities
{
    public partial class Series
    {
        public long tournamentId { get; set; }
        public int SportId { get; set; }
        public string tournamentName { get; set; }
        public bool Status { get; set; }
    }
}
