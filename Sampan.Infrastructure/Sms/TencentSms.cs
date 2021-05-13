using System.Threading.Tasks;

namespace Sampan.Infrastructure.Sms
{
    public class TencentSms : ISmsService
    {
        public Task<bool> SendAsync(string phone, string captcha)
        {
            throw new System.NotImplementedException();
        }
    }
}