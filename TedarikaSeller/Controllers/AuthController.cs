using Microsoft.AspNetCore.Mvc;
using TedarikaSeller.Models;
using TedarikaSeller.ServicesAbstract;

namespace TedarikaSeller.Controllers
{
    public class AuthController : Controller
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

        public IActionResult Register()
        {
            return View(new AuthRegisterModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(AuthRegisterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _authService.RegisterAsync(model);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Registration failed");
                return View(model);
            }

            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View(new AuthLoginModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthLoginModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // AuthService üzerinden login isteği gönderiliyor
            var token = await _authService.LoginAsync(model);
            if (string.IsNullOrEmpty(token))
            {
                // Eğer token null veya boş dönerse, giriş başarısız demektir.
                ModelState.AddModelError(string.Empty, "Login failed. Please check your credentials.");
                return View(model);
            }

            // Token session'a kaydediliyor
            HttpContext.Session.SetString("Token", token);

            // Giriş başarılıysa Dashboard'a yönlendiriliyor
            return RedirectToAction("Dashboard", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Login");
        }
    }
}
