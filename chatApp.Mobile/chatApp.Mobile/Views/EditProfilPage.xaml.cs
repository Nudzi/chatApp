using chatApp.Mobile.ViewModels;
using chatModel;
using Plugin.Media;
using System;
using System.IO;
using System.Net.Mail;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace chatApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilPage : ContentPage
    {
        ProfilViewModel model = null;
        public EditProfilPage()
        {
            InitializeComponent();
            BindingContext = model = new ProfilViewModel {  };
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
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (validateRegistration() == true)
            {
                await model.SaveUserProfil(resultImage);
                Application.Current.MainPage = new MainPage(Global.LoggedUser);
                resultImage.Source = null;
            }
            else
            {
                await DisplayAlert("Error", "Wrong input!", "OK");
            }
        }
        private bool validateRegistration()
        {
            bool valid = true;
            if (validatePassword() == false)
                valid = false;
            if (validatePasswordConf() == false)
                valid = false;
            if (validateEmail() == false)
                valid = false;

            if (valid == false)
            {
                return false;
            }
            else
            {
                return true;
            };
        }

        private bool validatePassword()
        {
            if (inputPassword.Text == "")
            {

                passwordError.Text = "Must insert password!";
                passwordError.IsVisible = true;
                return false;
            }
            
            else
            {
                passwordError.IsVisible = false;
                passwordError.Text = "";
                return true;
            }
        }
        private bool validatePasswordConf()
        {
            if (inputPassword.Text != inputConf.Text)
            {

                passwordConfError.Text = "Passwords do not match!";
                passwordConfError.IsVisible = true;
                return false;
            }
            else
            {
                passwordConfError.Text = "";
                passwordConfError.IsVisible = false;
                return true;
            }
        }
        private bool validateEmail()
        {
            try
            {
                MailAddress mail = new MailAddress(inputEmail.Text);

            }
            catch (Exception)
            {
                emailError.Text = "Email not in the correct format!";
                emailError.IsVisible = true;
                return false;
            }

            if (inputEmail.Text == "")
            {

                emailError.Text = "Must insert Email!";
                emailError.IsVisible = true;
                return false;
            }
            else
            {

                emailError.IsVisible = false;
                emailError.Text = "";
                return true;
            }
        }

        private void Remove_Picture(object sender, System.EventArgs e)
        {
            resultImage.Source = null;
        }
    }
}