using Flurl.Http;
using System;
using System.Threading.Tasks;
using chatModel;

namespace chatApp.WinUI
{
    public class APIService
    {
        private string _route = null;
        public static string userName { get; set; }
        public static string password { get; set; }
        public APIService(string route)
        {
            _route = route;
        }
        public async Task<T> Get<T>(object search)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}";
            if (search != null)
            {
                url += "?";
                url += await search.ToQueryString();
            }
            return await url.WithBasicAuth(userName, password).GetJsonAsync<T>();

        }
        public async Task<T> GetById<T>(object id)
        {
            var url = $"{ Properties.Settings.Default.APIUrl}/{_route}/{id}";
            return await url.WithBasicAuth(userName, password).GetJsonAsync<T>();

        }
        public async Task<T> Insert<T>(object request)
        {
            try
            {
                var url = $"{ Properties.Settings.Default.APIUrl}/{_route}";
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
                var url = $"{Properties.Settings.Default.APIUrl}/{_route}/{id}";

                return await url.WithBasicAuth(userName, password).PutJsonAsync(request).ReceiveJson<T>();
            }
            catch (Exception)
            {
                throw new Exception("Error in update!");
            }
        }
        public async Task<T> isAdmin<T>(int userTypeId)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}/isAdmin/{userTypeId}";

            return await url.GetJsonAsync<T>();
        }
        public async Task<T> Authentication<T>(string username, string password)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}/Authentication/{username},{password}";

            return await url.GetJsonAsync<T>();
        }

        public async Task<T> Delete<T>(int id)
        {
            try
            {
                var url = $"{Properties.Settings.Default.APIUrl}/{_route}/{id}";

                return await url.DeleteAsync().ReceiveJson<T>();
            }
            catch (Exception)
            {
                throw new Exception("Error in delete!");
            }
        }
    }
}
