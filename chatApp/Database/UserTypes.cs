using System;
using System.Collections.Generic;

namespace chatApp.Database
{
    public partial class UserTypes
    {
        public UserTypes()
        {
            UsersUserTypes = new HashSet<UsersUserTypes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UsersUserTypes> UsersUserTypes { get; set; }
    }
}
