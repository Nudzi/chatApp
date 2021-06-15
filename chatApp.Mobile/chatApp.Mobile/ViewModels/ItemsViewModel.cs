using System.Collections.ObjectModel;
using System.Threading.Tasks;

using chatModel;
using chatApp.Mobile.Services;
using chatModel.Requests.Friends;
using chatModel.Requests.UserImages;
using System.Collections.Generic;
using chatModel.Requests.Histories;
using System.Linq;
using chatModel.Requests.Users;

namespace chatApp.Mobile.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private readonly APIService _usersService = new APIService("users");
        private readonly APIService _friendsService = new APIService("friends");
        private readonly APIService _userImagesService = new APIService("userImages");
        private readonly APIService _historiesService = new APIService("histories");

        public ObservableCollection<FriendList> FriendList { get; set; } = new ObservableCollection<FriendList>();
        public ObservableCollection<UsersSearchList> UsersSearchedList { get; set; } = new ObservableCollection<UsersSearchList>();

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

            FriendsSearchRequest friendsSearchRequest2 = new FriendsSearchRequest
            {
                UserIdsecondary = Global.LoggedUser.Id
            };
            var friends2 = await _friendsService.Get<List<Friends>>(friendsSearchRequest2);
            foreach (var item in friends2)
            {
                friends.Add(item);
            }

            FriendList.Clear();

            foreach (var item in friends)
            {
                Users user = await _usersService.GetById<Users>(item.UserIdsecondary);
                if(item.UserIdsecondary == Global.LoggedUser.Id) {
                    user = await _usersService.GetById<Users>(item.UserIdprimary);
                }
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
                if (item.UserIdsecondary == Global.LoggedUser.Id)
                {
                    tmp.UserIdSecondary = item.UserIdprimary;
                }

                HistoriesSearchRequest historiesSearchRequest = new HistoriesSearchRequest
                {
                    UserIdSecondary = Global.LoggedUser.Id,
                    UserIdPrimary = item.UserIdsecondary,
                    Status = false
                };
                //if (item.UserIdsecondary == Global.LoggedUser.Id)
                //{
                //    tmp.UserIdSecondary = item.UserIdprimary;
                //}
                var historylist = await _historiesService.Get<List<Histories>>(historiesSearchRequest);
                if (historylist.Count() != 0)
                    tmp.Seen = false;

                FriendList.Add(tmp);
            }

        }
    }
}