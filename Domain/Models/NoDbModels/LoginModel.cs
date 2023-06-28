using System.ComponentModel.DataAnnotations;

namespace Domain.Models.NoDbModels
{
    public class LoginModel
    {
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
