namespace chatModel.Requests.Friends
{
    public class FriendList
    {
        public int UserIdSecondary { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumb { get; set; }
        public bool Seen { get; set; }
    }
}
