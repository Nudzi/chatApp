using chatApp.Mobile.Services;
using chatApp.Mobile.ViewModels;
using chatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace chatApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        RegisterViewModel model = null;
        APIService _usersService = new APIService("users");
        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = model = new RegisterViewModel();
        }

        private async void ButtonRegister_Clicked(object sender, EventArgs e)
        {
            bool doubleUserName = false;
            bool doubleEmail = false;

            List<Users> lista = await _usersService.Get<List<Users>>(null);

            foreach (var item in lista)
            {
                if (item.UserName.Equals(inputUserName.Text) == true)
                {
                    doubleUserName = true;
                }
                if (item.Email.Equals(inputEmail.Text) == true)
                {
                    doubleEmail = true;
                }
            }
            if (validateRegistration() == true)
            {

                if (doubleUserName == true)
                {
                    await DisplayAlert("Error", "User name already exists!", "OK");
                    userNameError.Text = "User name already exists!";
                    userNameError.IsVisible = true;
                }
                else if (doubleEmail == true)
                {
                    await DisplayAlert("Error", "Email already exists!", "OK");
                    emailError.Text = "Email already exists!";
                    emailError.IsVisible = true;

                }
                else
                {
                    await model.Register();
                }


            }
            else
            {
                await DisplayAlert("Error", "Wrong input!", "OK");
            }
        }
        private bool validateRegistration()
        {
            bool valid = true;
            if (validateFirstName() == false)
                valid = false;
            if (validateLastName() == false)
                valid = false;
            if (validateEmail() == false)
                valid = false;
            if (validateUserName() == false)
                valid = false;
            if (validatePassword() == false)
                valid = false;
            if (validateTelephone() == false)
                valid = false;
            if (validatePasswordConf() == false)
                valid = false;
            if (validateAddress() == false)
                valid = false;
            if (validateCity() == false)
                valid = false;
            if (validateCountry() == false)
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
        private bool validateFirstName()
        {
            if (inputFirstName.Text == "")
            {
                firstNameError.Text = "Must insert name!";
                firstNameError.IsVisible = true;
                return false;
            }
            else
            {
                firstNameError.IsVisible = false;
                firstNameError.Text = "";
                return true;
            }
        }

        private bool validateLastName()
        {
            if (inputLastName.Text == "")
            {
                lastNameError.Text = "Must insert last name!";
                lastNameError.IsVisible = true;
                return false;
            }
            else
            {
                lastNameError.IsVisible = false;
                lastNameError.Text = "";
                return true;
            }
        }
        private bool validateTelephone()
        {
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

        private bool validateAddress()
        {
            if (inputAddress.Text == "")
            {
                addressError.Text = "Must insert Address!";
                addressError.IsVisible = true;
                return false;
            }
            else
            {
                addressError.IsVisible = false;
                addressError.Text = "";
                return true;
            }
        }

        private bool validateCity()
        {
            if (inputCity.Text == "")
            {
                cityError.Text = "Must insert City!";
                cityError.IsVisible = true;
                return false;
            }
            else
            {
                cityError.IsVisible = false;
                cityError.Text = "";
                return true;
            }
        }
        private bool validateCountry()
        {
            if (inputAddress.Text == "")
            {
                countryError.Text = "Must insert Country!";
                countryError.IsVisible = true;
                return false;
            }
            else
            {
                countryError.IsVisible = false;
                countryError.Text = "";
                return true;
            }
        }
        private bool validateUserName()
        {
            if (inputUserName.Text == "")
            {
                userNameError.Text = "Must insert User name!";
                userNameError.IsVisible = true;
                return false;
            }
            if (inputUserName.Text.Count() < 2)
            {
                userNameError.Text = "User name must be above 2 characters";
                userNameError.IsVisible = true;
                return false;
            }
            else
            {
                userNameError.IsVisible = false;
                userNameError.Text = "";
                return true;
            }
        }

        private bool validatePassword()
        {
            if (inputPassword.Text == "")
            {
                passwordError.Text = "Must insert password!";
                passwordError.IsVisible = true;
                return false;
            }
            if (inputPassword.Text == inputUserName.Text)
            {
                passwordError.Text = "Password and User name must be differente!";
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
    }
}