using System.Threading.Tasks;

using Xamarin.Forms;

using chatModel;
using chatModel.Requests.UserImages;
using System.Collections.Generic;
using chatModel.Requests.Users;
using chatApp.Mobile.Services;
using System.Collections.ObjectModel;
using chatModel.Requests.Friends;
using System;

namespace chatApp.Mobile.ViewModels
{
    public class SearchedUsersViewModel : BaseViewModel
    {
        private readonly APIService _usersService = new APIService("users");
        private readonly APIService _friendsService = new APIService("friends");
        private readonly APIService _userImagesService = new APIService("userImages");
        private readonly APIService _historiesService = new APIService("histories");

        public ObservableCollection<UsersSearchList> UsersSearchedList { get; set; } = new ObservableCollection<UsersSearchList>();

        public SearchedUsersViewModel()
        {
        }
        public bool HasData = true;
        public bool NoData = true;
        public string SearchPhone { get; set; }
        public async Task Init()
        {
            UsersSearchRequest usersSearchRequest = new UsersSearchRequest
            {
                PhoneNumber = SearchPhone
            };
            
            UsersSearchedList.Clear();
            var users = await _usersService.Get<List<Users>>(usersSearchRequest);
            FriendsSearchRequest friendsSearchRequest = new FriendsSearchRequest
            {
                UserIdprimary = Global.LoggedUser.Id
            };
            List<int> ids = new List<int>();
            // treba dodati sve idijeve
            var friends = await _friendsService.Get<List<Friends>>(friendsSearchRequest);
            foreach (var friend in friends)
            {
                ids.Add(friend.UserIdsecondary);
            }

            FriendsSearchRequest friendsSearchRequest2 = new FriendsSearchRequest
            {
                UserIdsecondary = Global.LoggedUser.Id
            };
            // treba dodati sve idijeve, ne mogu sebe da dodam!
            var friends2 = await _friendsService.Get<List<Friends>>(friendsSearchRequest2);
            foreach (var friend in friends2)
            {
                ids.Add(friend.UserIdprimary);
            }

            List<Users> finalUsers = new List<Users>();
            finalUsers = users;
            foreach (var item in users)
            {
                foreach (var tmp in ids)
                {
                    if (item.Id == tmp || item.Id == Global.LoggedUser.Id)
                            if(finalUsers.Contains(item))
                                finalUsers.Remove(item);
                }
            }


            foreach (var item in finalUsers)
            {
                UserImagesSearchRequest request = new UserImagesSearchRequest
                {
                    UserId = item.Id
                };
                var userImages = await _userImagesService.Get<List<UserImages>>(request);
                UsersSearchList tmp = new UsersSearchList
                {
                    UserId = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Image = userImages[0].Image
                };
                UsersSearchedList.Add(tmp);
            }
            

            if (UsersSearchedList.Count > 0)
            {
                HasData = true;
                NoData = false;
            }
            else { HasData = false; NoData = true; }
        }

        public async Task AddFriend(UsersSearchList item)
        {
            var result = await App.Current.MainPage.DisplayAlert("Information!", "Do you want to add this person as a friend?", "Yes", "No");
            if (result)
            {
                UsersSearchRequest usersSearchRequest = new UsersSearchRequest
                {
                    UserName = Global.LoggedUser.UserName
                };
                var user = await _usersService.Get<List<Users>>(usersSearchRequest);
                FriendsUpsertRequest friendsUpsertRequest = new FriendsUpsertRequest
                {
                    DateAdded = DateTime.Now,
                    UserIdprimary = user[0].Id,
                    UserIdsecondary = item.UserId
                };
                await _friendsService.Insert<Friends>(friendsUpsertRequest);
                await App.Current.MainPage.DisplayAlert("Information!", "Friend Succesufully added!", "Close");
            }
        }
    }
}