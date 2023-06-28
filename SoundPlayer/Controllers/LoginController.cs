using Domain.Exeptions;
using Domain.Models.NoDbModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace SoundPlayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                await _loginService.SignIn(loginModel);

                return RedirectToAction("HomePage", "Home");
            }
            catch (LoginException)
            {
                ViewBag.ErrorMessage = "User not found";

                return View(loginModel);
            }
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            await _loginService.Register(registerModel);

            return Redirect("Login");
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            _loginService.SignOff();

            return Redirect("Login");
        }
    }
}
