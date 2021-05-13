namespace Sampan.Public.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetPageDto
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string Sort { get; set; }

        /// <summary>
        /// 是否正序
        /// </summary>
        public bool Asc { get; set; }
    }
}