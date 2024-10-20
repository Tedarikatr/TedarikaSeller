using System.Text;
using System.Text.Json;
using TedarikaSeller.Models;
using TedarikaSeller.ServicesAbstract;

namespace TedarikaSeller.Services
{
    public class ShopService : IShopService
    {
        private readonly HttpClient _httpClient;

        public ShopService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Kullanıcının mağaza bilgisi olup olmadığını kontrol eden metod
        public async Task<bool> HasShopAsync(string userName)
        {
            var response = await _httpClient.GetAsync($"https://yourapiurl/api/SellerShopToday/shop-details?userName={userName}");

            if (response.IsSuccessStatusCode)
            {
                // Mağaza bilgisi varsa true döner
                var shopExists = await response.Content.ReadAsStringAsync();
                return bool.Parse(shopExists); // true ya da false olarak parse ediliyor
            }

            return false; // Yanıt başarısızsa varsayılan olarak false döner
        }

        // Mağaza ekleme işlemi
        public async Task AddShopAsync(ShopFormModel model)
        {
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://yourapiurl/api/SellerShopToday/Add-Shop-FirstStep", content);

            if (!response.IsSuccessStatusCode)
            {
                // Hata mesajını al ve bir Exception fırlat
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Mağaza eklenemedi. API Yanıtı: {errorMessage}");
            }
        }
    }
}
