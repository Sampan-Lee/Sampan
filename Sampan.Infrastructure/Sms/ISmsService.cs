using System.Threading.Tasks;

namespace Sampan.Infrastructure.Sms
{
    public interface ISmsService
    {
        Task<bool> SendAsync(string phone, string captcha);
    }
}