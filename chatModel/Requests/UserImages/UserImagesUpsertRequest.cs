namespace chatModel.Requests.UserImages
{
    public class UserImagesUpsertRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumb { get; set; }
    }
}
