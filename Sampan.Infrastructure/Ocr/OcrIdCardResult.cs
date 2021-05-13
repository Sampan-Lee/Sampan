namespace Sampan.Infrastructure.Ocr
{
    public class OcrIdCardResult
    {
        public OcrIdCardResult()
        {
            Status = false;
            LogId = "";
            ImageStatus = "";
            Name = "";
            IdCard = "";
        }

        /// <summary>
        /// 
        /// </summary>
        public string error_code { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string error_msg { get; set; }

        public bool Status { get; set; }

        public string LogId { get; set; }

        public string ImageStatus { get; set; }

        public string Name { get; set; }

        public string IdCard { get; set; }

        public string DetailResult { get; set; }
    }
}