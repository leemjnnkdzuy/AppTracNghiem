using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AppTracNghiem.Models
{
    public class EssayQuestionModel
    {
        [JsonProperty("_id")]
        public string? Id { get; set; }
        
        [JsonProperty("question")]
        public string? Question { get; set; }
        
        [JsonProperty("sampleAnswer")]
        public string? SampleAnswer { get; set; }
        
        [JsonProperty("gradingCriteria")]
        public string? GradingCriteria { get; set; }
        
        [JsonProperty("maxLength")]
        public int MaxLength { get; set; }
        
        [JsonProperty("minLength")]
        public int MinLength { get; set; }
        
        [JsonProperty("difficulty")]
        public string? Difficulty { get; set; }
        
        [JsonProperty("keywords")]
        public List<string> Keywords { get; set; }
        
        [JsonProperty("createdBy")]
        public string? CreatedBy { get; set; }
        
        [JsonProperty("contest")]
        public string? Contest { get; set; }
        
        [JsonProperty("createdAt")]
        [JsonConverter(typeof(SafeDateTimeConverter))]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("updatedAt")]
        [JsonConverter(typeof(SafeDateTimeConverter))]
        public DateTime UpdatedAt { get; set; }

        public EssayQuestionModel()
        {
            Keywords = new List<string>();
        }
    }
}
