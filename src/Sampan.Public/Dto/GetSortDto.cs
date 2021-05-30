namespace Sampan.Public.Dto
{
    public class GetSortDto
    {
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