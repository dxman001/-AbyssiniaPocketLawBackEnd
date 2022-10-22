using System;
using System.Collections.Generic;

namespace PocketLawDB.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? PassWord { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
    }
}
