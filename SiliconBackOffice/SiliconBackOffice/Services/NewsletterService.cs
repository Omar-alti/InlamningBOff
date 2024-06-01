using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using SiliconBackOffice.Models;
using System.Text.Json;
using System.Text;

namespace SiliconBackOffice.Services
{
    public class NewsletterService
    {
        private readonly HttpClient _http;

        public NewsletterService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<NewsletterModel>> GetSubscribersAsync()
        {
            try
            {
                var result = await _http.GetAsync("https://newsletterprovider-bmfl.azurewebsites.net/api/getsubscriber?code=tPCFhF1FQuVYEQYn83tYTYUyYadZjTh6C2dhPSRKUFhLAzFuqRMTVw%3D%3D");
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadFromJsonAsync<IEnumerable<NewsletterModel>>() ?? Enumerable.Empty<NewsletterModel>();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to retrieve subscribers", ex);
            }
            return Enumerable.Empty<NewsletterModel>();
        }

        public async Task<NewsletterModel> GetSubscriberAsync(string email)
        {
            try
            {
                var result = await _http.GetAsync($"https://newsletterprovider-bmfl.azurewebsites.net/api/getsubscriber?code=tPCFhF1FQuVYEQYn83tYTYUyYadZjTh6C2dhPSRKUFhLAzFuqRMTVw%3D%3D&email={email}");
                if (result.IsSuccessStatusCode)
                {
                    return await result.Content.ReadFromJsonAsync<NewsletterModel>() ?? throw new InvalidOperationException("Failed to deserialize response.");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to retrieve subscriber: {email}", ex);
            }
            return null!;
        }

        public async Task<bool> UpdateSubscriberAsync(NewsletterModel model)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                var result = await _http.PostAsync("https://newsletterprovider-bmfl.azurewebsites.net/api/Subscribe?code=jYf27X54n1bcCN5_hkPi-bEE5pcSX2kp_Uiplh0QDiUsAzFuZanPRg%3D%3D", content);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to update subscriber", ex);
            }
        }

        public async Task<bool> UnsubscribeAsync(string email)
        {
            try
            {
                var result = await _http.PostAsync($"https://newsletterprovider-bmfl.azurewebsites.net/api/Unsubscribe?code=nf9P6RZL4OXjbD3jR7W5dfIUVFwH4UlJK0Fb3RglfPAbAzFuOdFs1A%3D%3D&email={email}", null);
                return result.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to unsubscribe: {email}", ex);
            }
        }
    }
}
