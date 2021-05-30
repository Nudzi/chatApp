using chatModel;

namespace chatApp.Mobile.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Users User { get; set; }
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
    }
}
