using Sampan.WebExtension.Model;

namespace Sampan.WebExtension.Authentication
{
    /// <summary>
    /// 中间件异常
    /// </summary>
    public class ApiResponse
    {
        public int code { get; } = 404;
        public string value { get; } = "No Found";
        public JsonResultModel<string> JsonResultModel { get; }

        public ApiResponse(int StatusCode, string msg = null)
        {
            code = StatusCode;

            switch (StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                {
                    value = "很抱歉，您无权访问该接口，请确保已经登录!";
                }
                    break;
                case HttpStatusCode.Forbidden:
                {
                    value = "很抱歉，您无权访问该接口，请联系管理员分配权限!";
                }
                    break;
                case HttpStatusCode.ServerError:
                {
                    value = msg;
                }
                    break;
            }

            JsonResultModel = new JsonResultModel<string>()
            {
                status = false,
                code = code,
                errorMsg = value
            };
        }
    }
}