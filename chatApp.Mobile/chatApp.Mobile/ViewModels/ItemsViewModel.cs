using System.Collections.ObjectModel;
using System.Threading.Tasks;

using Xamarin.Forms;

using chatModel;
using chatApp.Mobile.Services;
using chatModel.Requests.Friends;
using chatModel.Requests.UserImages;
using System.Collections.Generic;
using System.Windows.Input;
using chatModel.Requests.Histories;
using System.Linq;

namespace chatApp.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly APIService _usersService = new APIService("users");
        private readonly APIService _friendsService = new APIService("friends");
        private readonly APIService _userImagesService = new APIService("userImages");
        private readonly APIService _historiesService = new APIService("histories");

        public ObservableCollection<FriendList> FriendList { get; set; } = new ObservableCollection<FriendList>();

        public ItemsViewModel()
        {
            Title = "Browse";
        }
        public async Task Init()
        {
            FriendsSearchRequest friendsSearchRequest = new FriendsSearchRequest
            {
                UserIdprimary = Global.LoggedUser.Id
            };
            var friends = await _friendsService.Get<List<Friends>>(friendsSearchRequest);

            FriendList.Clear();

            foreach (var item in friends)
            {
                Users user = await _usersService.GetById<Users>(item.UserIdsecondary);
                UserImagesSearchRequest request = new UserImagesSearchRequest
                {
                    UserId = user.Id
                };
                var userImages = await _userImagesService.Get<List<UserImages>>(request);
                FriendList tmp = new FriendList
                {
                    UserIdSecondary = item.UserIdsecondary,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Image = userImages[0].Image,
                    ImageThumb = userImages[0].ImageThumb,
                    Seen = true
                };
                HistoriesSearchRequest historiesSearchRequest = new HistoriesSearchRequest
                {
                    UserIdSecondary = Global.LoggedUser.Id,
                    UserIdPrimary = item.UserIdsecondary,
                    Status = false
                };
                var historylist = await _historiesService.Get<List<Histories>>(historiesSearchRequest);
                if (historylist.Count() != 0)
                    tmp.Seen = false;

                FriendList.Add(tmp);
            }
        }

    }
}