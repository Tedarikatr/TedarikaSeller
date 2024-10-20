using System.Text;
using System.Text.Json;
using TedarikaSeller.Models;
using TedarikaSeller.ServicesAbstract;

namespace TedarikaSeller.Services
{
    public class AuthService : IAuthService
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

