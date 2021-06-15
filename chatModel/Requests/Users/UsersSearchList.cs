using System;
using System.Collections.Generic;
using System.Text;

namespace chatModel.Requests.Users
{
    public class UsersSearchList
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Image { get; set; }
    }
}
