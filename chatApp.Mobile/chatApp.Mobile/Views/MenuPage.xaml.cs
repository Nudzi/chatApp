using chatApp.Mobile.Models;
using chatModel;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace chatApp.Mobile.Views
{
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        public ListView ListView;
        MainPage RootPage { get => Application.Current.MainPage as MainPage; }
        List<HomeMenuItem> menuItems;
        Users user;
        public MenuPage()
        {
            InitializeComponent();
            ListView = MenuItemsListView;

            menuItems = new List<HomeMenuItem>
            {
                new HomeMenuItem {Id = MenuItemType.Welcome, Title="Welcome" },
                new HomeMenuItem {Id = MenuItemType.Profile, Title="Profile" },
                new HomeMenuItem {Id = MenuItemType.Settings, Title="Settings" },
                new HomeMenuItem {Id = MenuItemType.Feedback, Title="Feedback" },
                new HomeMenuItem {Id = MenuItemType.AboutUs, Title="About Us" },
                new HomeMenuItem {Id = MenuItemType.Logout, Title="Logout" },
            };

            MenuItemsListView.ItemsSource = menuItems;

            MenuItemsListView.SelectedItem = menuItems[0];
            MenuItemsListView.ItemSelected += async (sender, e) =>
            {
                if (e.SelectedItem == null)
                    return;

                var id = (int)((HomeMenuItem)e.SelectedItem).Id;
                await RootPage.NavigateFromMenu(id, user);
            };
        }
    }
}
