using TedarikaSeller.Models;

namespace TedarikaSeller.ServicesAbstract
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(AuthRegisterModel model);
        Task<string> LoginAsync(AuthLoginModel model);
    }
}
