using System;
using System.Collections.Generic;

namespace PocketLawDB.Models
{
    public partial class Law
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Status { get; set; }
        public string? Entry { get; set; }
        public string? Category { get; set; }
        public string? Jurisdiction { get; set; }
        public string? Download { get; set; }
        public string? Keywords { get; set; }
    }
}
