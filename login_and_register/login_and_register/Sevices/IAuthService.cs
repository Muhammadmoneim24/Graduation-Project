using login_and_register.Dtos;
using login_and_register.Models;

namespace login_and_register.Sevices
{
    public interface IAuthService
    {
        Task<AuthModel>RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(LoginModel model);
        //Task<string> AddRoleAsync(AddRoleModel model);
    }
}
