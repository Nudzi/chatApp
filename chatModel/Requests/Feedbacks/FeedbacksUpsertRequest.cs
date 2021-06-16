using System;

namespace chatModel.Requests.Feedbacks
{
    public class FeedbacksUpsertRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Feedback { get; set; }
        public int? ReportedUserId { get; set; }
        public string Reason { get; set; }
        public byte[] Image { get; set; }
        public byte[] ImageThumb { get; set; }
    }
}
