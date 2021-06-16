using chatApp.Mobile.Services;
using chatModel;
using chatModel.Requests.Feedbacks;
using chatModel.Requests.Histories;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace chatApp.Mobile.ViewModels
{
    public class FeedbackViewModel : BaseViewModel
    {
        private readonly APIService _feedbacksService = new APIService("feedbacks");
        public ObservableCollection<HistoryList> Histories { get; set; } = new ObservableCollection<HistoryList>();
        public byte[] byteImage { get; set; }
        public FeedbackViewModel()
        {
            InitCommand = new Command(async () => await Init());
        }
        public ICommand InitCommand { get; set; }
        public async Task Init()
        {
        }
        public async Task SendFeedback(string feedbacks, string reason, string userNumber)
        {
            int telephone;
            bool success = int.TryParse(userNumber, out telephone);
            try
            {
                FeedbacksUpsertRequest feedbacksUpsertRequest = new FeedbacksUpsertRequest
                {
                    Feedback = feedbacks,
                    Image = byteImage,
                    ImageThumb = byteImage,
                    Reason = reason,
                    UserId = Global.LoggedUser.Id
                };
                if (success)
                    feedbacksUpsertRequest.ReportedUserId = telephone;
                else feedbacksUpsertRequest.ReportedUserId = null;

                await _feedbacksService.Insert<Feedbacks>(feedbacksUpsertRequest);

                await Application.Current.MainPage.DisplayAlert("Success", "Success, thank you.", "OK");

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
