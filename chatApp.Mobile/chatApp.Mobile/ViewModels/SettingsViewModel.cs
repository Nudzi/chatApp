using chatModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace chatApp.Mobile.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel()
        {
            Title = "Settings";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }
        public Users User { get; set; }
        public ICommand OpenWebCommand { get; }

    }
}