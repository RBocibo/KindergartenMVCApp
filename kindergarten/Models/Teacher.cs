using System;
using System.Collections.Generic;

namespace kindergarten.Models
{
    public partial class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Contact { get; set; }
        public string Email { get; set; } = null!;
        public int ChildId { get; set; }

        public virtual Child? Child { get; set; } = null;
    }
}
