using System;
using System.Collections.Generic;

namespace PocketLawDB.Models
{
    public partial class Cassation
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Category { get; set; }
        public string? Volume { get; set; }
        public string? Decision { get; set; }
        public string? Given { get; set; }
        public string? Download { get; set; }
        public string? Keywords { get; set; }
    }
}
