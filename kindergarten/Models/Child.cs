using System;
using System.Collections.Generic;

namespace kindergarten.Models
{
    public partial class Child
    {
        public Child()
        {
            Teachers = new HashSet<Teacher>();
        }

        public int ChildId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public string Allegies { get; set; } = null!;
        public string? AllergyDescription { get; set; }
        public int ParentId { get; set; }
        public decimal? MonthlyFees { get; set; }

        public virtual Parent? Parent { get; set; } = null;
        public virtual ICollection<Teacher> Teachers { get; set; }
    }
}
