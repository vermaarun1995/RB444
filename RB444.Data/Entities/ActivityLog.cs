using System;

namespace RB444.Data.Entities
{
    public partial class ActivityLog
    {
        public int Id { get; set; }
        public DateTime LoginDate { get; set; }
        public int LoginStatus { get; set; } // 1. for online 2. for offline
        public string IpAddress { get; set; }
        public string ISP { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
    }
}
