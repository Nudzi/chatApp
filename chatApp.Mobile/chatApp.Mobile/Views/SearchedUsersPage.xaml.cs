using chatApp.Mobile.ViewModels;
using chatModel.Requests.Users;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace chatApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchedUsersPage : ContentPage
    {
        SearchedUsersViewModel model;
        public SearchedUsersPage(string item)
        {
            InitializeComponent();
            BindingContext = model = new SearchedUsersViewModel { SearchPhone = item };
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as UsersSearchList;
            await model.AddFriend(item);
            await Navigation.PushAsync(new ItemsPage());
        }
    }
}