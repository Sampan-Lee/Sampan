using Microsoft.Extensions.DependencyInjection;
using Sampan.Common.Util;
using Sampan.Infrastructure.Ocr;

namespace Sampan.WebExtension.Dependency
{
    public static class OcrDependency
    {
        public static void AddOcr(this IServiceCollection services)
        {
            var ocrType = (OcrType) Appsettings.app("AppSettings", "Ocr", "OcrType").ToInt();
            switch (ocrType)
            {
                case OcrType.Baidu:
                    services.AddScoped<IOcr, BaiduOcr>();
                    break;
                case OcrType.Aliyun:
                    services.AddScoped<IOcr, AliyunOcr>();
                    break;
                case OcrType.Tencent:
                    services.AddScoped<IOcr, TencentOcr>();
                    break;
            }
        }
    }
}