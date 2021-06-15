using chatApp.Mobile.Services;
using chatModel;
using chatModel.Requests.UserImages;
using chatModel.Requests.Users;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace chatApp.Mobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {

        private readonly APIService _service = new APIService("users");
        private readonly APIService _addressesService = new APIService("userAddresses");
        private readonly APIService _userImagesService = new APIService("userImages");
        string _firstName = string.Empty;
        public byte[] byteImage { get; set; }
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }
        string _lastName = string.Empty;
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }
        string _email = string.Empty;
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
        string _telephone = string.Empty;
        public string Telephone
        {
            get { return _telephone; }
            set { SetProperty(ref _telephone, value); }
        }
        string _userName = string.Empty;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
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
        string _country = string.Empty;
        public string Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }
        string _city = string.Empty;
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }
        string _address = string.Empty;
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        public async Task Register()
        {
            try
            {
                List<int> userTypes = new List<int>();
                userTypes.Add((int)chatModel.Enums.UserTypes.User);

                //UserAdressesUpsertRequest userAddressesUpserRequest = new UserAddressesUpsertRequest
                //{
                //    AddressName = Address,
                //    City = City,
                //    Country = Country,
                //};
                //var address = await _addressesService.Insert<Model.UserAddresses>(userAddressesUpserRequest);


                UsersInsertRequest request = new UsersInsertRequest
                {
                    Email = Email,
                    FirstName = FirstName,
                    LastName = LastName,
                    Password = Password,
                    PasswordConfirmation = PasswordConf,
                    Status = true,
                    Telephone = Telephone,
                    UserName = UserName,
                    UserTypes = userTypes
                    //UserAddressId = address.Id
                };

                var user = await _service.Insert<Users>(request);
                Global.LoggedUser = user;

                UserImagesSearchRequest imagesSearchRequest = new UserImagesSearchRequest
                {
                    UserId = user.Id
                };
                UserImagesUpsertRequest userImagesUpsertRequest = new UserImagesUpsertRequest
                {
                    Image = byteImage,
                    ImageThumb = byteImage,
                    UserId = user.Id
                };
                Application.Current.MainPage = new MainPage(user);

                await _userImagesService.Insert<UserImages>(userImagesUpsertRequest);
                await Application.Current.MainPage.DisplayAlert("Success", "Welcome new User!", "OK");
            }
            catch
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error", "OK");
            }
        }
    }
}