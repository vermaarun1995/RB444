using System;
using System.Collections.Generic;
using System.Text;

namespace RB444.Data.Entities
{
    public partial class SeriesSetting
    {
        public int Id { get; set; }
        public string SportName { get; set; }
        public string SeriesName { get; set; }
        public Nullable<bool> Action { get; set; }
    }
}
