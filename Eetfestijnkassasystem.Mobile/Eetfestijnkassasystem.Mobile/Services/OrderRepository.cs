using Eetfestijnkassasystem.Shared.DTO;
using Eetfestijnkassasystem.Shared.Interface;
using Eetfestijnkassasystem.Shared.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Eetfestijnkassasystem.Mobile.Services
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly HttpClient _httpClient;
        private bool _isConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public OrderRepository()
        {
            _httpClient = GetHttpClient();
        }

        public async Task<Order> AddAsync(Order entity)
        {
            try
            {
                if (entity == null || !_isConnected)
                    return null;

                var serializedOrder = JsonConvert.SerializeObject(entity);
                //var serializedOrder = JsonConvert.SerializeObject(entity, Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                HttpResponseMessage response = await _httpClient.PostAsync($"api/Orders", new StringContent(serializedOrder, Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                // response.Headers.Location = "...api/Orders/{new id}
                string responseBody = await response.Content.ReadAsStringAsync();
                Order createdOrder = await DeserializeOrderAsync(responseBody);
                return createdOrder;
            }
            catch (Exception ex)
            {
                ex.ToString();
                throw;
            }
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            try
            {
                if (!_isConnected)
                    return null;

                var jsonResponse = await _httpClient.GetStringAsync($"api/Orders");
                var orders = await DeserializeOrdersAsync(jsonResponse);
                //var orders = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonResponse));
                return orders;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Order> GetAsync(int id)
        {
            if (!_isConnected || id <= 0)
                return null;

            var jsonResponse = await _httpClient.GetStringAsync($"api/Orders/{id}");
            Order order = await DeserializeOrderAsync(jsonResponse);
            return order;

            //return await Task.Run(() => JsonConvert.DeserializeObject<Order>(jsonResponse));
        }

        public async Task RemoveAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        private async Task<Order> DeserializeOrderAsync(string jsonResponse)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<Order>(jsonResponse));
        }

        private async Task<IEnumerable<Order>> DeserializeOrdersAsync(string jsonResponse)
        {
            return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Order>>(jsonResponse));
        }

        private HttpClient GetHttpClient()
        {
            HttpClientHandler httpHandler = new HttpClientHandler();
#if (DEBUG)
            httpHandler.ServerCertificateCustomValidationCallback = (o, cert, chain, errors) => true;
#endif
            return new HttpClient(httpHandler) { BaseAddress = new Uri($"{App.Settings.BackendUrl}/"), };
        }

       
    }
}
