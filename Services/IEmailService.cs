using System.Threading.Tasks;

namespace Lun2Code.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}