using System;

namespace chatModel.Requests.Histories
{
    public class HistoryList
    {
        public int Id { get; set; }
        public int UserIdPrimary { get; set; }
        public int UserIdSecondary { get; set; }
        public string Message { get; set; }
        public bool isPrimary { get; set; }
        public bool isVisibleMessage { get; set; }
        public bool isVisibleImage { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool Status { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumb { get; set; }
    }
}
