using System;
using System.Collections.Generic;

namespace kindergarten.Models
{
    public partial class Parent
    {
        public Parent()
        {
            Children = new HashSet<Child>();
        }

        public int ParentId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Contact { get; set; }
        public string Email { get; set; } = null!;

        public virtual ICollection<Child> Children { get; set; }
    }
}
