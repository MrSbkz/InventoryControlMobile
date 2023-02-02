using System;
using InventoryControlMobile.Models;
using InventoryControlMobile.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventoryControlMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var response = await HttpService.LoginAsync(TextUsername.Text, TextPassword.Text);

            var result = JsonConvert.DeserializeObject<HttpResponse<LoginResponse>>(response);
            
            if (result == null || !result.IsSuccess)
            {
                await DisplayAlert("Error", "Incorrect username or password", "Ok");
                return;
            }

            if (!string.IsNullOrWhiteSpace(result.Data.Token))
            {
                Preferences.Set("token", result.Data.Token);
                await Navigation.PushAsync(new MenuPage());
            }
        }
    }
}