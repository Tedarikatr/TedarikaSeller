using TedarikaSeller.Models;

namespace TedarikaSeller.ServicesAbstract
{
    public interface ICompanyService
    {
        Task<bool> HasCompanyAsync(string userName);
        Task AddCompanyAsync(CompanyFormModel model);
    }
}
