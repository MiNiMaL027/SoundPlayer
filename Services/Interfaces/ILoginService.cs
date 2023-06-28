using Domain.Models.NoDbModels;
using Domain.Models;

namespace Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> SignIn(LoginModel model);

        void SignOff();

        Task<User> Register(RegisterModel model);
    }
}
