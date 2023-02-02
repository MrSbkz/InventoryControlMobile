using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace InventoryControlMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            var token = Preferences.Get("token", null);
            MainPage = !string.IsNullOrWhiteSpace(token)
                ? new NavigationPage(new MenuPage())
                : new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}