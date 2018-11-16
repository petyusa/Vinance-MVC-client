using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    using Models;
    using Models.Identity;

    public interface IUserApi
    {
        Task<TokenResult> GetToken(LoginModel loginModel);
        Task<VinanceUser> Register(RegisterModel registerModel);
        Task<VinanceUser> GetUser();
    }
}