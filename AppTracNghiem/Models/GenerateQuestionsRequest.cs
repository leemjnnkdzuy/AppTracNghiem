using Newtonsoft.Json;

namespace AppTracNghiem.Models
{
    public class GenerateQuestionsRequest
    {
        [JsonProperty("documentContent")]
        public string? DocumentContent { get; set; }
        
        [JsonProperty("aiModel")]
        public string? AiModel { get; set; }
        
        [JsonProperty("numMultipleChoice")]
        public int NumMultipleChoice { get; set; }
        
        [JsonProperty("numEssay")]
        public int NumEssay { get; set; }
        
        [JsonProperty("contestId")]
        public string? ContestId { get; set; }
    }
}
