namespace Sampan.Public.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class GetPageDto : GetSortDto
    {
        /// <summary>
        /// 页数
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int Size { get; set; }
    }
}