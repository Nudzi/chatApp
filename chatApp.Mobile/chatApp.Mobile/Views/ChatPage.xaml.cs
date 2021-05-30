using chatApp.Mobile.ViewModels;
using chatModel.Requests.Friends;
using Plugin.Media;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace chatApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {
        ChatViewModel model = null;
        public ChatPage(FriendList item)
        {
            InitializeComponent();
            BindingContext = model = new ChatViewModel() { FriendList = item };
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void Add_Picture(object sender, System.EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Small
            });

            if (file == null)
                return;
            var stream = file.GetStream();

            resultImage.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            file.GetStream().CopyTo(memoryStream);
            file.Dispose();
            model.byteImage = memoryStream.ToArray();
        }

        private async void Send_Message(object sender, System.EventArgs e)
        {

            //await TextToSpeech.SpeakAsync("Jebi se Keno");
            if (message.Text == null && resultImage.Source == null)
            {
                await DisplayAlert("Error", "Send Image or Message!", "OK");
            }
            else
            {
                await model.SaveMessage(message.Text);
                Remove_Picture(sender, e);
                message.Text = null;
                await model.Init();
            }
        }

        private void Remove_Picture(object sender, System.EventArgs e)
        {
            resultImage.Source = null;
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            await DisplayAlert("Error", "Send Image or Message!", "OK");
        }
    }
}