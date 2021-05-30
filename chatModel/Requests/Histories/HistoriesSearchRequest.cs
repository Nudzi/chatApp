namespace chatModel.Requests.Histories
{
    public class HistoriesSearchRequest
    {
        public int? UserIdPrimary { get; set; }
        public int? UserIdSecondary { get; set; }
        public string Message { get; set; }
        public bool? Status { get; set; }

    }
}
