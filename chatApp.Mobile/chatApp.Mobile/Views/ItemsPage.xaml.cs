using System;
using System.ComponentModel;
using Xamarin.Forms;
using chatApp.Mobile.ViewModels;
using chatModel.Requests.Friends;

namespace chatApp.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await viewModel.Init();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as FriendList;

            await Navigation.PushAsync(new ChatPage(item));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (searchBar.Text == "")
                await Application.Current.MainPage.DisplayAlert("Error", "Must insert at least one number!", "OK");
            else await Navigation.PushAsync(new SearchedUsersPage(searchBar.Text));
        }
    }
}