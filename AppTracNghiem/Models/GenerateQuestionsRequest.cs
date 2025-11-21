namespace AppTracNghiem.Models
{
    public class GenerateQuestionsRequest
    {
        public string DocumentContent { get; set; }
        public string AiModel { get; set; }
        public int NumMultipleChoice { get; set; }
        public int NumEssay { get; set; }
        public string ContestId { get; set; }
    }
}
