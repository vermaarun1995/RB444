using System.Threading.Tasks;

namespace RB444.Core.IServices
{
    public interface IEmailService
    {
        //Task SendEmailAsync(string email, string subject, string message);
        Task<bool> SendEmailAsync(string _to, string _toname, string _sub, string _body, out string _msg);
    }
}
