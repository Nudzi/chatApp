using System;
using System.Collections.Generic;

namespace chatModel
{
    public class UsersUserTypes
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserTypeId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public Users User { get; set; }
        public UserTypes UserType { get; set; }
    }
}
