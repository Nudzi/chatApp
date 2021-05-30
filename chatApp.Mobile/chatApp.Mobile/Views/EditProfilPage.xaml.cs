using chatApp.Mobile.ViewModels;
using chatModel;
using System;
using System.Net.Mail;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace chatApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditProfilPage : ContentPage
    {
        ProfilViewModel model = null;
        public EditProfilPage(Users user)
        {
            InitializeComponent();
            BindingContext = model = new ProfilViewModel { User = user };
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (validateRegistration() == true)
            {
                await model.SaveUserProfil();
                Application.Current.MainPage = new MainPage(Global.LoggedUser);
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
            if (validateTelephone() == false)
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

        private bool validateTelephone()
        {
            foreach (var letter in inputTelephone.Text)
            {
                if (!Char.IsNumber(letter))
                {
                    telephoneError.IsVisible = true;
                    return false;
                }
            }


            if (inputTelephone.Text == "")
            {
                telephoneError.Text = "Must insert Telephone!";
                telephoneError.IsVisible = true;
                return false;
            }
            else
            {

                telephoneError.IsVisible = false;
                telephoneError.Text = "";
                return true;
            }
        }
    }
}