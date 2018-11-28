using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models;
    using Models.Identity;

    public interface IUserApi
    {
        Task<TokenResult> GetToken(LoginModel loginModel);
        Task<TokenResult> Register(RegisterModel registerModel);
        Task<VinanceUser> GetUser();
        Task<bool> ConfirmEmail(string email, string token);
    }
}