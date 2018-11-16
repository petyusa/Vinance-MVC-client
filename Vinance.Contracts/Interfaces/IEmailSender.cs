using System.Threading.Tasks;
using Vinance.Contracts.Models.Identity;

namespace Vinance.Contracts.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmail(VinanceUser user);
    }
}