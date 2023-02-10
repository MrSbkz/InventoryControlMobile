using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace InventoryControlMobile.Services
{
    public static class HttpService
    {
        private const string BaseUrl = "https://inventorycontrol.up.railway.app/";
        private static readonly HttpClient Client;

        static HttpService()
        {
            Client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl),
            };
        }

        public static async Task<string> LoginAsync(string username, string password)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(new { username, password }), Encoding.UTF8, "application/json");
                var response = await Client.PostAsync("auth/login", content);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<string> GetRolesAsync()
        {
            try
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));
                var response = await Client.GetAsync("role/user/list");
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static async Task<string> GetDeviceInfoAsync(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));

                var response = await httpClient.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static async Task<string> DoInventory(int deviceId)
        {
            try
            {
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Preferences.Get("token", null));
                var content = new StringContent(JsonConvert.SerializeObject(new { }), Encoding.UTF8, "application/json");
                var response = await Client.PostAsync($"device/inventory?id={deviceId}", content);
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}