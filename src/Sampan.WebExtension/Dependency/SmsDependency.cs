using Microsoft.Extensions.DependencyInjection;
using Sampan.Common.Util;
using Sampan.Infrastructure.Sms;

namespace Sampan.WebExtension.Dependency
{
    public static class SmsDependency
    {
        public static void AddSms(this IServiceCollection services)
        {
            var smsType = (SmsType) Appsettings.app("AppSettings", "Sms", "SmsType").ToInt();
            switch (smsType)
            {
                case SmsType.AliyunSms:
                    services.AddScoped<ISmsService, AliyunSms>();
                    break;
                case SmsType.TencentSms:
                    services.AddScoped<ISmsService, TencentSms>();
                    break;
                case SmsType.DevSms:
                default:
                    services.AddScoped<ISmsService, FakeSms>();
                    break;
            }
        }
    }
}