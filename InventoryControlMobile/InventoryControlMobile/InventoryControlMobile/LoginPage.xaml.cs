using System;
using System.Collections.Generic;
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
            var loginResponse = await HttpService.LoginAsync(TextUsername.Text, TextPassword.Text);

            var loginResult = JsonConvert.DeserializeObject<HttpResponse<LoginResponse>>(loginResponse);
            
            if (loginResult == null || !loginResult.IsSuccess)
            {
                await DisplayAlert("Error", "Incorrect username or password", "Ok");
                return;
            }

            if (!string.IsNullOrWhiteSpace(loginResult.Data.Token))
            {
                Preferences.Set("token", loginResult.Data.Token);
            }

            var rolesResponse = await HttpService.GetRolesAsync();
            
            var rolesResult = JsonConvert.DeserializeObject<HttpResponse<IList<string>>>(rolesResponse);
            
            if (rolesResult == null || !rolesResult.IsSuccess || !rolesResult.Data.Contains("accountant"))
            {
                await DisplayAlert("Error", "You do not have an access to make inventory", "Ok");
                Preferences.Remove("token");
                return;
            }
            
            await Navigation.PushAsync(new MenuPage());
        }
    }
}