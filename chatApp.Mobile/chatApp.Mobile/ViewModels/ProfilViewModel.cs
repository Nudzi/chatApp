using chatApp.Mobile.Services;
using chatModel;
using chatModel.Requests.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace chatApp.Mobile.ViewModels
{
    public class ProfilViewModel : BaseViewModel
    {
        private readonly APIService _usersService = new APIService("users");

        public ProfilViewModel()
        {
            InitCommand = new Command(async () => await Init());
            SaveCommand = new Command(async () => await SaveUserProfil());
        }
        public Users User { get; set; }
        string _username = string.Empty;

        string _firstname = string.Empty;
        public string FirstName
        {
            get { return _firstname; }
            set { SetProperty(ref _firstname, value); }
        }
        string _lastname = string.Empty;
        public string LastName
        {
            get { return _lastname; }
            set { SetProperty(ref _lastname, value); }
        }
        string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        string _passwordConf = string.Empty;
        public string PasswordConf
        {
            get { return _passwordConf; }
            set { SetProperty(ref _passwordConf, value); }
        }
        private ICommand SaveCommand { get; set; }
        private ICommand InitCommand { get; set; }

        public async Task Init()
        {
            try
            {
                //var gettedUser = await _usersService.GetById<Users>(User.Id);
                //User = gettedUser;
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error", "OK");
            }
        }
        public async Task SaveUserProfil()
        {
            try
            {
                var gettedUser = Global.LoggedUser;
                //var forUserAddress = await _usersService.GetById<Users>(gettedUser.Id);

                var userInts = new List<int>();
                if (gettedUser.UserTypes == null)
                {
                    userInts.Add((int)chatModel.Enums.UserTypes.User);
                }
                else
                {
                    foreach (var item in gettedUser.UserTypes)
                        userInts.Add(item.UserTypeId);
                }
                //if(forUserAddress.UserAddressId == null)
                //{
                //    forUserAddress.UserAddressId = 1;
                //}

                UsersInsertRequest request = new UsersInsertRequest();
                request.Id = gettedUser.Id;
                request.Email = gettedUser.Email;
                request.Status = true;
                request.FirstName = gettedUser.FirstName;
                request.LastName = gettedUser.LastName;
                request.Password = Password;
                request.PasswordConfirmation = PasswordConf;
                request.Telephone = gettedUser.Telephone;
                request.UserName = gettedUser.UserName;
                request.UserTypes = userInts;
                //request.UserAddressId = forUserAddress.UserAddressId;


                if (request != null)
                {
                    if(!User.FirstName.Equals(""))
                        request.FirstName = User.FirstName;

                    if (!User.LastName.Equals(""))
                        request.LastName = User.LastName;

                    if (!User.Telephone.Equals(""))
                        request.Telephone = User.Telephone;

                    //await _usersService.Update<Users>(request.Id, request);
                    //var glob = await _usersService.GetById<Users>(request.Id);
                    //Global.LoggedUser = glob;
                    await Application.Current.MainPage.DisplayAlert("Success", "Successfuly edited! ", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Cannot save right now, try later!", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}