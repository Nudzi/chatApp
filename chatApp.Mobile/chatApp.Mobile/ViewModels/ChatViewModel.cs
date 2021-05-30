using chatApp.Mobile.Services;
using chatModel;
using chatModel.Requests.Friends;
using chatModel.Requests.Histories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace chatApp.Mobile.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        private readonly APIService _usersService = new APIService("users");
        private readonly APIService _historiesService = new APIService("histories");
        public ObservableCollection<HistoryList> Histories { get; set; } = new ObservableCollection<HistoryList>();

        public ChatViewModel()
        {
        }
        public FriendList FriendList { get; set; }
        public string FriendlyName { get; set; }
        public byte[] byteImage { get; set; }
        public async Task Init()
        {
            try
            {
                HistoriesSearchRequest historiesSearchRequest = new HistoriesSearchRequest
                {
                    UserIdSecondary = Global.LoggedUser.Id,
                    UserIdPrimary = FriendList.UserIdSecondary,
                    Status = false
                };
                var historyListUnseen = await _historiesService.Get<List<Histories>>(historiesSearchRequest);
                foreach (var item in historyListUnseen)
                {
                    item.Status = true;
                    await _historiesService.Update<Histories>(item.Id, item);
                }


                var historylist = await _historiesService.Get<List<Histories>>(null);
                var histories = new List<Histories>();
                foreach (var history in historylist)
                {
                    if (history.UserIdPrimary == Global.LoggedUser.Id || history.UserIdPrimary == FriendList.UserIdSecondary)
                    {
                        if (history.UserIdSecondary == Global.LoggedUser.Id || history.UserIdSecondary == FriendList.UserIdSecondary)
                        {
                            histories.Add(history);
                        }
                    }
                }
                //MLContext mlContext = new MLContext();


                Histories.Clear();
                foreach (var history in histories)
                {
                    HistoryList tmp = new HistoryList
                    {
                        Id = history.Id,
                        Image = history.Image,
                        ImageThumb = history.ImageThumb,
                        Message = history.Message,
                        ModifiedDate = history.ModifiedDate,
                        Status = history.Status,
                        UserIdPrimary = history.UserIdPrimary,
                        UserIdSecondary = history.UserIdSecondary,
                        isPrimary = false,
                        isVisibleImage = true,
                        isVisibleMessage = true
                    };
                    if (history.Image.Length == 0)
                        tmp.isVisibleImage = false;
                    if (history.Message == null)
                        tmp.isVisibleMessage = false;
                    if (tmp.UserIdPrimary == Global.LoggedUser.Id)
                        tmp.isPrimary = true;
                    Histories.Add(tmp);

                }

            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        public async Task SaveMessage(string message)
        {
            try
            {
                HistoriesUpsertRequest historiesUpsertRequest = new HistoriesUpsertRequest
                {
                    Image = byteImage,
                    ImageThumb = byteImage,
                    Message = message,
                    UserIdPrimary = Global.LoggedUser.Id,
                    UserIdSecondary = FriendList.UserIdSecondary,
                    ModifiedDate = DateTime.Now,
                    Status = false
                };
                await _historiesService.Insert<Histories>(historiesUpsertRequest);
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}