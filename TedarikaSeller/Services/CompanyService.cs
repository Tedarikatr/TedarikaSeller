using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TedarikaSeller.Models;
using TedarikaSeller.ServicesAbstract;

namespace TedarikaSeller.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _httpClient;

        public CompanyService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Kullanıcının şirket bilgisi olup olmadığını kontrol eden metod
        public async Task<bool> HasCompanyAsync(string userName)
        {
            var response = await _httpClient.GetAsync($"https://yourapiurl/api/seller/companies?userName={userName}");

            if (response.IsSuccessStatusCode)
            {
                // Şirket bilgisi varsa true döner
                var companyExists = await response.Content.ReadAsStringAsync();
                return bool.Parse(companyExists); // true ya da false olarak parse ediliyor
            }

            return false; // Yanıt başarısızsa varsayılan olarak false döner
        }

        // Şirket ekleme işlemi
        public async Task AddCompanyAsync(CompanyFormModel model)
        {
            var jsonContent = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://yourapiurl/api/seller/companies", content);

            if (!response.IsSuccessStatusCode)
            {
                // Hata mesajını al ve bir Exception fırlat
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Şirket eklenemedi. API Yanıtı: {errorMessage}");
            }
        }
    }
}
