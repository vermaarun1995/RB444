namespace RB444.Data.Entities
{
    public class SportsSetting
    {
        public int Id { get; set; }
        public string SportName { get; set; }
        public long MinOddLimit { get; set; }
        public long MaxOddLimit { get; set; }
        public bool Status { get; set; }
    }
}
