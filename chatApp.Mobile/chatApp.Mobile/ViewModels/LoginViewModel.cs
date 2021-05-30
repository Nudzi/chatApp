using chatApp.Mobile.Services;
using chatModel;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace chatApp.Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly APIService _service = new APIService("users");

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }
        string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        string _password = string.Empty;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public ICommand LoginCommand { get; set; }

        async Task Login()
        {
            IsBusy = true;
            try
            {
                Users user = await _service.Authentication<Users>(Username, Password);
                if (user != null)
                {
                    Global.LoggedUser = user;

                    await Application.Current.MainPage.DisplayAlert("Success", "Welcome " + user.FirstName + " " + user.LastName, "OK");
                    Application.Current.MainPage = new MainPage(user);
                }
                else
                {
                    IsBusy = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "Wrong username or password", "OK");
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }
    }
}