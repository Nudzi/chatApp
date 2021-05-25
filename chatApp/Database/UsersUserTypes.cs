using System;
using System.Collections.Generic;

namespace chatApp.Database
{
    public partial class UsersUserTypes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual Users User { get; set; }
        public virtual UserTypes UserType { get; set; }
    }
}
