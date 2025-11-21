using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AppTracNghiem.Models
{
    public class SafeDateTimeConverter : IsoDateTimeConverter
    {
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return DateTime.MinValue;

            if (reader.Value is string dateString)
            {
                if (dateString.StartsWith("0000"))
                {
                    return DateTime.UtcNow;
                }
            }

            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                return DateTime.UtcNow;
            }
        }
    }

    public class MultipleChoiceQuestionModel
    {
        [JsonProperty("_id")]
        public string? Id { get; set; }
        
        [JsonProperty("question")]
        public string? Question { get; set; }
        
        [JsonProperty("options")]
        public List<OptionModel> Options { get; set; }
        
        [JsonProperty("explanation")]
        public string? Explanation { get; set; }
        
        [JsonProperty("difficulty")]
        public string? Difficulty { get; set; }
        
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

        public MultipleChoiceQuestionModel()
        {
            Options = new List<OptionModel>();
        }
    }

    public class OptionModel
    {
        [JsonProperty("text")]
        public string? Text { get; set; }
        
        [JsonProperty("isCorrect")]
        public bool IsCorrect { get; set; }
    }
}
