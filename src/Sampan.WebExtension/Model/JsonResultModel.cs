namespace Sampan.WebExtension.Model
{
    /// <summary>
    /// 通用返回信息类
    /// </summary>
    public class JsonResultModel<T>
    {
        /// <summary>
        /// 操作是否成功
        /// </summary>  
        public bool status { get; set; } = true;

        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; } = HttpStatusCode.OK;

        /// <summary>
        /// 返回信息
        /// </summary>
        public string errorMsg { get; set; }

        /// <summary>
        /// 返回数据集合
        /// </summary>
        public T data { get; set; }
    }

    /// <summary>
    /// 扩展返回值
    /// </summary>
    public static class JsonResultModelExtend
    {
        /// <summary>
        /// 成功返回值
        /// </summary>
        public static JsonResultModel<T> ToSuccess<T>(this T data, bool status = true, int code = HttpStatusCode.OK)
        {
            return new JsonResultModel<T>
            {
                status = status,
                code = code,
                data = data ?? default(T)
            };
        }

        /// <summary>
        /// 返回失败结果
        /// </summary>
        public static JsonResultModel<T> ToError<T>(bool status = false, int code = 404,
            string errorMsg = "获取失败")
        {
            return new JsonResultModel<T>()
            {
                status = status,
                code = code,
                errorMsg = errorMsg,
            };
        }
    }
}