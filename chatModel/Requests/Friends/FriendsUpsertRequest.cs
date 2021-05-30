using System;

namespace chatModel.Requests.Friends
{
    public class FriendsUpsertRequest
    {
        public int Id { get; set; }
        public int UserIdprimary { get; set; }
        public int UserIdsecondary { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
