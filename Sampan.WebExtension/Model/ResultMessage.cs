namespace Sampan.WebExtension.Model
{
    public class ResultMessage
    {
        /// <summary>
        /// 获取成功
        /// </summary>
        public static readonly string Success = "获取成功";

        /// <summary>
        /// 获取失败
        /// </summary>
        public static readonly string Error = "获取失败";

        /// <summary>
        /// 操作成功
        /// </summary>
        public static readonly string SaveSuccess = "操作成功!";

        /// <summary>
        /// 删除成功
        /// </summary>
        public static readonly string DeleteSuccess = "删除成功!";

        /// <summary>
        /// 登录成功
        /// </summary>
        public static readonly string LoginSuccess = "登录成功!";

        /// <summary>
        /// 登录失败,用户名或密码有误!
        /// </summary>
        public static readonly string LoginFail = "登录失败,用户名或密码有误!";

        /// <summary>
        /// 未找到对应的账号，请绑定账号！
        /// </summary>
        public static readonly string LoginByOpenIdNotFind = "未找到对应的账号，请绑定账号！";

        /// <summary>
        /// 参数校验不通过
        /// </summary>
        public static readonly string ValidationError = "参数校验不通过!";

        /// <summary>
        /// 请选择上传的文件
        /// </summary>
        public static readonly string PleaseSelectFile = "请选择上传的文件!";

        /// <summary>
        /// 上传成功
        /// </summary>
        public static readonly string UploadSuccess = "上传成功!";

        /// <summary>
        /// 图片过大
        /// </summary>
        public static readonly string UploadPictureTooLarge = "图片过大!";

        /// <summary>
        /// 图片格式错误
        /// </summary>
        public static readonly string UploadPictureFormatError = "图片格式错误!";

        /// <summary>
        /// 服务器连接异常，请联系管理员！
        /// </summary>
        public static readonly string ServerConnectionError = "服务器连接异常，请联系管理员！";
    }
}