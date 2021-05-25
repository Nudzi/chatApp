using System;
using System.Collections.Generic;

namespace chatApp.Database
{
    public partial class UserAdresses
    {
        public UserAdresses()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
