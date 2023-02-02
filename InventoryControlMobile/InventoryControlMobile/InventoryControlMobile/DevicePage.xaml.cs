using System;
using InventoryControlMobile.Models;
using InventoryControlMobile.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace InventoryControlMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevicePage : ContentPage
    {
        public DevicePage(DeviceResponse device)
        {
            InitializeComponent();
            Id.Text = device.Id.ToString();
            DeviceName.Text = device.Name;
            AssignedTo.Text = device.AssignedTo.FullName;
            Username.Text = device.AssignedTo.UserName;
            RegisterDate.Text = device.RegisterDate.ToString("dd MMMM yyyy HH:mm:ss");
            DecommissionDate.Text = device.DecommissionDate?.ToString("dd MMMM yyyy HH:mm:ss") ?? "--/--";
        }

        private async void Button_OnInventory(object sender, EventArgs e)
        {
            var response = await HttpService.DoInventory(int.Parse(Id.Text));

            var result = JsonConvert.DeserializeObject<HttpResponse<string>>(response);
            
            if (result == null || !result.IsSuccess)
            {
                await DisplayAlert("Error", "Something went wrong", "Ok");
                return;
            }

            await Navigation.PushAsync(new MenuPage());
        }
    }
}