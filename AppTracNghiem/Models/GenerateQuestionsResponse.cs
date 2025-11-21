using System.Collections.Generic;
using Newtonsoft.Json;

namespace AppTracNghiem.Models
{
    public class GenerateQuestionsResponse
    {
        [JsonProperty("success")]

        public bool Success { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("data")]
        public GeneratedQuestionsData? Data { get; set; }
    }

    public class GeneratedQuestionsData
    {
        [JsonProperty("multipleChoice")]
        public List<MultipleChoiceQuestionModel> MultipleChoice { get; set; }
        
        [JsonProperty("essay")]
        public List<EssayQuestionModel> Essay { get; set; }

        public GeneratedQuestionsData()
        {
            MultipleChoice = new List<MultipleChoiceQuestionModel>();
            Essay = new List<EssayQuestionModel>();
        }
    }

    public class QuestionsListResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("data")]
        public QuestionsListData? Data { get; set; }
    }

    public class QuestionsListData
    {
        [JsonProperty("multipleChoice")]
        public List<MultipleChoiceQuestionModel> MultipleChoice { get; set; }
        
        [JsonProperty("essay")]
        public List<EssayQuestionModel> Essay { get; set; }
        
        [JsonProperty("total")]
        public int Total { get; set; }
    
        public QuestionsListData()
        {
            MultipleChoice = new List<MultipleChoiceQuestionModel>();
            Essay = new List<EssayQuestionModel>();
        }
    }
}
