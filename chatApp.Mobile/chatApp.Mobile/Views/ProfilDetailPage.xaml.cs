using chatApp.Mobile.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace chatApp.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilDetailPage : ContentPage
    {
        ProfilViewModel model = null;
        public ProfilDetailPage()
        {
            InitializeComponent();
            BindingContext = model = new ProfilViewModel {  }; 
        }
        protected  async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditProfilPage());
        }
    }
}