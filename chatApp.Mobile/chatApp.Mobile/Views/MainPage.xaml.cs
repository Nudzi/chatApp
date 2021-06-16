using chatApp.Mobile.Models;
using chatApp.Mobile.ViewModels;
using chatApp.Mobile.Views;
using chatModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace chatApp.Mobile
{
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainViewModel model = null;
        public MainPage(Users user)
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            BindingContext = model = new MainViewModel() { User = user };

            //MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }
        public async Task NavigateFromMenu(int id, Users user)
        {
            user = model.User;
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.Welcome:
                        MenuPages.Add(id, new NavigationPage(new ItemsPage()));
                        break;
                    case (int)MenuItemType.Profile:
                        MenuPages.Add(id, new NavigationPage(new ProfilDetailPage()));
                        break;
                    case (int)MenuItemType.Settings:
                        MenuPages.Add(id, new NavigationPage(new SettingsPage(user)));
                        break;
                    case (int)MenuItemType.AboutUs:
                        MenuPages.Add(id, new NavigationPage(new AboutPage(user)));
                        break;
                    case (int)MenuItemType.Logout:
                        MenuPages.Add(id, new NavigationPage(new LoginPage()));
                        break;
                    case (int)MenuItemType.Feedback:
                        MenuPages.Add(id, new NavigationPage(new FeedbackPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}
