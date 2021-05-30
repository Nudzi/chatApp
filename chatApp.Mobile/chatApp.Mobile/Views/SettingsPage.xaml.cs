using chatApp.Mobile.ViewModels;
using chatModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace chatApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        SettingsViewModel model = null;
        public SettingsPage(Users user)
        {
            InitializeComponent();
            BindingContext = model = new SettingsViewModel { User = user };
        }
    }
}