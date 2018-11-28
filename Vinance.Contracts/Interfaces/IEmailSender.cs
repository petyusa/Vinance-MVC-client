using System.Threading.Tasks;

namespace Vinance.Contracts.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmail(string email, string message);
    }
}