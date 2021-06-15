using System;

namespace chatModel
{
    public class Histories
    {
        public int Id { get; set; }
        public int UserIdPrimary { get; set; }
        public int UserIdSecondary { get; set; }
        public string Message { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumb { get; set; }
        public string ImagePath { get; set; }
    }
}
