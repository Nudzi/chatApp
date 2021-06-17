using chatApp.Mobile.ViewModels;
using chatModel.Requests.Friends;
using chatModel.Requests.Histories;
using Plugin.Media;
using System.IO;
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
            model.AlbumPath = file.AlbumPath;

            resultImage.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            file.GetStream().CopyTo(memoryStream);
            file.Dispose();
            model.byteImage = memoryStream.ToArray();
        }

        private async void Send_Message(object sender, System.EventArgs e)
        {

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
            var item = e.SelectedItem as HistoryList;
            await model.VoiceImage(item);
        }

        private async void Load_Messages(object sender, System.EventArgs e)
        {
            model.listSize += 10;
            await model.Init();
        }

        private async void Take_Shoot(object sender, System.EventArgs e)
        {

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {

                Directory = "Desktop",
                Name = "test.jpg"
            });

            if (file == null)
                return;
            await DisplayAlert("File Location", file.Path, "OK");
            var stream = file.GetStream();
            model.AlbumPath = file.AlbumPath;

            resultImage.Source = ImageSource.FromStream(() => stream);
            var memoryStream = new MemoryStream();
            file.GetStream().CopyTo(memoryStream);
            file.Dispose();
            model.byteImage = memoryStream.ToArray();
        }
    }
}