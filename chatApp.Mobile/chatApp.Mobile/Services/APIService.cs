using chatModel;
using Flurl.Http;
using System;
using System.Threading.Tasks;

namespace chatApp.Mobile.Services
{
    public class APIService
    {
        private string _route = null;
        public static string userName { get; set; }
        public static string password { get; set; }

#if DEBUG
        private string _apiUrl = "http://localhost:51598/api";
#endif
#if RELEASE
                private string _apiUrl = "https://natureBeauty";
#endif

        public APIService(string route)
        {
            _route = route;
        }
        //recommednation
        public async Task<T> GetAlikeProducts<T>(int id)
        {
            var url = $"{_apiUrl}/{_route}/GetAlikeProducts/{id}";

            return await url.GetJsonAsync<T>();
        }

        public async Task<T> Get<T>(object search)
        {
            var url = $"{_apiUrl}/{_route}";
            if (search != null)
            {
                url += "?";
                url += await search.ToQueryString();
            }
            return await url.GetJsonAsync<T>();

        }
        public async Task<T> GetById<T>(object id)
        {
            var url = $"{ _apiUrl}/{_route}/{id}";
            return await url.WithBasicAuth(userName, password).GetJsonAsync<T>();

        }

        public async Task<T> Delete<T>(int id)
        {
            try
            {
                var url = $"{_apiUrl}/{_route}/{id}";

                return await url.DeleteAsync().ReceiveJson<T>();
            }
            catch (System.Exception ex)
            {
                await Xamarin.Forms.Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return default;
            }
        }
        public async Task<T> Insert<T>(object request)
        {
            try
            {
                var url = $"{ _apiUrl}/{_route}";
                return await url.WithBasicAuth(userName, password).PostJsonAsync(request).ReceiveJson<T>();
            }
            catch (Exception)
            {
                throw new Exception("Error in adding!");
            }
        }
        public async Task<T> Update<T>(int id, object request)
        {
            try
            {
                var url = $"{_apiUrl}/{_route}/{id}";

                return await url.PutJsonAsync(request).ReceiveJson<T>();
            }
            catch (Exception)
            {
                throw new Exception("Error in update!");
            }
        }
        public async Task<T> isAdmin<T>(int userTypeId)
        {
            var url = $"{_apiUrl}/{_route}/isAdmin/{userTypeId}";

            return await url.GetJsonAsync<T>();
        }
        public async Task<T> Authentication<T>(string username, string password)
        {
            var url = $"{_apiUrl}/{_route}/Authentication/{username},{password}";

            return await url.GetJsonAsync<T>();
        }
    }
}
