using TedarikaSeller.Models;

namespace TedarikaSeller.ServicesAbstract
{
    public interface IShopService
    {
        Task<bool> HasShopAsync(string userName);
        Task AddShopAsync(ShopFormModel model);
    }
}
