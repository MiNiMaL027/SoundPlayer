using Domain.Exeptions;
using Domain.Models;
using Domain.Models.NoDbModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Services.Interfaces;

namespace Services.Services
{
    [AllowAnonymous]
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> SignIn(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new LoginException();

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result == null)
                throw new LoginException();

            if (result.RequiresTwoFactor)
                throw new LoginException();

            if (result.IsLockedOut)
                throw new LoginException();

            if (user.ArchivateDate != null)
                throw new LoginException($"{user.UserName} - Deleted");

            if (result.Succeeded)
                return true;

            throw new LoginException();
        }

        public async void SignOff()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<User> Register(RegisterModel model)
        {
            if (model.Password != model.ConfirmedPassword)
                throw new LoginException();

            var user = new User
            {
                UserName = model.Name,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return user;
            }

            throw new LoginException();
        }
    }
}
