using System.ComponentModel.DataAnnotations;

namespace RB444.Data.Entities
{
    public partial class Series
    {
        [Key]
        public long tournamentId { get; set; }
        public int SportId { get; set; }
        public string tournamentName { get; set; }
        public bool Status { get; set; }
    }
}
