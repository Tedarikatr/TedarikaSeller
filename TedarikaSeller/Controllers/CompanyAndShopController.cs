using Microsoft.AspNetCore.Mvc;
using TedarikaSeller.Services;
using TedarikaSeller.Models;
using System.Threading.Tasks;
using TedarikaSeller.ServicesAbstract;

namespace TedarikaSeller.Controllers
{
    public class CompanyAndShopController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IShopService _shopService;

        public CompanyAndShopController(ICompanyService companyService, IShopService shopService)
        {
            _companyService = companyService;
            _shopService = shopService;
        }

        public async Task<IActionResult> Index()
        {
            // Kullanıcının şirket ve mağaza bilgileri olup olmadığını kontrol ediyoruz
            bool hasCompany = await _companyService.HasCompanyAsync(User.Identity.Name); // Kullanıcıyı token ile bulabilirsin
            bool hasShop = await _shopService.HasShopAsync(User.Identity.Name);

            if (!hasCompany && !hasShop)
            {
                return RedirectToAction("CompanyForm");
            }

            if (!hasCompany)
            {
                return RedirectToAction("CompanyForm");
            }

            if (!hasShop)
            {
                return RedirectToAction("ShopForm");
            }

            return RedirectToAction("Dashboard", "Home"); // Eğer hem şirket hem de mağaza varsa
        }

        // Şirket formu
        [HttpGet]
        public IActionResult CompanyForm()
        {
            return View(new CompanyFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> CompanyForm(CompanyFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _companyService.AddCompanyAsync(model);
            return RedirectToAction("ShopForm"); // Şirket eklendikten sonra mağaza formuna yönlendiriyoruz
        }

        // Mağaza formu
        [HttpGet]
        public IActionResult ShopForm()
        {
            return View(new ShopFormModel());
        }

        [HttpPost]
        public async Task<IActionResult> ShopForm(ShopFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _shopService.AddShopAsync(model);
            return RedirectToAction("Dashboard", "Home"); // Mağaza eklendikten sonra ana sayfaya yönlendir
        }
    }
}
