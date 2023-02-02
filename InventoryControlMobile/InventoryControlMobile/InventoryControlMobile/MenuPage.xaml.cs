using System;
using System.Threading.Tasks;
using InventoryControlMobile.Models;
using InventoryControlMobile.Services;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace InventoryControlMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();
        }

        private void Logout(object sender, EventArgs e)
        {
            Preferences.Remove("token");
            Navigation.PushAsync(new LoginPage());
        }

        private async void Scan(object sender, EventArgs e)
        {
            var scan = new ZXingScannerPage();
            await Navigation.PushAsync(scan);
            scan.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await NavigateToDeviceInfoAsync(result.Text);
                });
            };
        }

        private async Task NavigateToDeviceInfoAsync(string url)
        {
            var response = await HttpService.GetDeviceInfoAsync(url);

            var result = JsonConvert.DeserializeObject<HttpResponse<DeviceResponse>>(response);

            if (result == null || !result.IsSuccess)
            {
                await DisplayAlert("Error", "Something went wrong", "Ok");
                return;
            }

            await Navigation.PushAsync(new DevicePage(result.Data));
        }
    }
}