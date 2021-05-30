using System;

namespace chatModel.Requests.Friends
{
    public class FriendsSearchRequest
    {
        public int? UserIdprimary { get; set; }
        public int? UserIdsecondary { get; set; }
    }
}
