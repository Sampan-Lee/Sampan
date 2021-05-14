using System.Threading.Tasks;

namespace Sampan.Infrastructure.Sms
{
    public class AliyunSms : ISmsService
    {
        public Task<bool> SendAsync(string phone,string captcha)
        {
            throw new System.NotImplementedException();
        }
    }
}