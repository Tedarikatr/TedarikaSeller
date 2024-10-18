// AuthService.cs
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using TedarikaSeller.Models;

namespace TedarikaSeller.Services
{
	public class AuthService
	{
		private readonly HttpClient _httpClient;

		public AuthService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<bool> RegisterAsync(AuthRegisterModel model)
		{
			var json = JsonSerializer.Serialize(model);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			var response = await _httpClient.PostAsync("https://todayapi.azurewebsites.net/api/SellerUser/register", content);
			return response.IsSuccessStatusCode;
		}

        public async Task<string> LoginAsync(AuthLoginModel model)
        {
            var json = JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://todayapi.azurewebsites.net/api/SellerUser/login", content);

            if (!response.IsSuccessStatusCode)
            {
                return null; // Başarısız giriş denemesi
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            // Yanıtı JSON formatında deserialize edip token'i çıkarıyoruz
            var tokenResponse = JsonSerializer.Deserialize<AuthTokenResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Token isminin büyük/küçük harf duyarlılığı olmasın
            });

            return tokenResponse?.Token;
        }
    }

	public class AuthTokenResponse
	{
		public string Token { get; set; }
	}
}

