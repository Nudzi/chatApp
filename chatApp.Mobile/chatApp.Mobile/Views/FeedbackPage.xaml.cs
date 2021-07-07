using chatApp.Mobile.ViewModels;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace chatApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FeedbackPage : ContentPage
    {
        FeedbackViewModel model = null;
        public FeedbackPage()
        {
            InitializeComponent();
            BindingContext = model = new FeedbackViewModel();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private void Remove_Picture(object sender, System.EventArgs e)
        {
            resultImage.Source = null;
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

        private async void Send_Feedback(object sender, EventArgs e)
        {
            await model.SendFeedback(feedbacks.Text, reason.Text, userNumber.Text);
            Application.Current.MainPage = new MainPage(Global.LoggedUser);
            feedbacks.Text = "";
            reason.Text = "";
            userNumber.Text = "";
        }
    }
}