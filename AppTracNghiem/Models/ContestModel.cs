using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppTracNghiem.Models
{
    public class ContestModel
    {
        [JsonProperty("_id")]
        public string Id { get; set; }
        
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }
        
        [JsonProperty("duration")]
        public int Duration { get; set; }
        
        [JsonProperty("visibility")]
        public string Visibility { get; set; }
        
        [JsonProperty("isDurationLimited")]
        public bool? IsDurationLimited { get; set; }
        
        [JsonProperty("isEndDateSet")]
        public bool? IsEndDateSet { get; set; }
        
        [JsonProperty("allowReview")]
        public string AllowReview { get; set; }
        
        [JsonProperty("shuffleQuestions")]
        public bool? ShuffleQuestions { get; set; }
        
        [JsonProperty("shuffleAnswers")]
        public bool? ShuffleAnswers { get; set; }
        
        [JsonProperty("hasMultipleChoice")]
        public bool? HasMultipleChoice { get; set; }
        
        [JsonProperty("hasEssay")]
        public bool? HasEssay { get; set; }
        
        [JsonProperty("showScore")]
        public bool? ShowScore { get; set; }
        
        [JsonProperty("showAnswers")]
        public bool? ShowAnswers { get; set; }
        
        [JsonProperty("lockScreen")]
        public bool? LockScreen { get; set; }
        
        [JsonProperty("maxAttempts")]
        public int? MaxAttempts { get; set; }
        
        [JsonProperty("members")]
        public List<ContestMemberModel> Members { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }
        
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        public ContestModel()
        {
            Members = new List<ContestMemberModel>();
            Visibility = "private";
            Status = "draft";
            AllowReview = "afterEnd";
            ShuffleQuestions = false;
            ShuffleAnswers = false;
            IsDurationLimited = false;
            IsEndDateSet = false;
            HasMultipleChoice = false;
            HasEssay = false;
            ShowScore = false;
            ShowAnswers = false;
            LockScreen = false;
            MaxAttempts = 1;
        }
    }

    public class ContestMemberModel
    {
        [JsonProperty("userId")]
        public object? UserId { get; set; }
        
        [JsonProperty("studentId")]
        public string? StudentId { get; set; }
        
        [JsonProperty("fullName")]
        public string? FullName { get; set; }
        
        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonIgnore]
        public string? UserIdString
        {
            get
            {
                return UserId switch
                {
                    null => null,
                    string s => s,
                    JObject obj => obj["_id"]?.ToString() ?? obj["id"]?.ToString(),
                    _ => UserId.ToString()
                };
            }
        }
    }

    public class CreateContestRequest
    {
        [JsonProperty("title")]
        public string? Title { get; set; }
        
        [JsonProperty("description")]
        public string? Description { get; set; }
        
        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }
        
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }
        
        [JsonProperty("duration")]
        public int Duration { get; set; }
        
        [JsonProperty("visibility")]
        public string? Visibility { get; set; }
        
        [JsonProperty("isDurationLimited")]
        public bool IsDurationLimited { get; set; }
        
        [JsonProperty("isEndDateSet")]
        public bool IsEndDateSet { get; set; }
        
        [JsonProperty("allowReview")]
        public string? AllowReview { get; set; }
        
        [JsonProperty("shuffleQuestions")]
        public bool ShuffleQuestions { get; set; }
        
        [JsonProperty("shuffleAnswers")]
        public bool ShuffleAnswers { get; set; }
        
        [JsonProperty("hasMultipleChoice")]
        public bool HasMultipleChoice { get; set; }
        
        [JsonProperty("hasEssay")]
        public bool HasEssay { get; set; }
        
        [JsonProperty("showScore")]
        public bool ShowScore { get; set; }
        
        [JsonProperty("showAnswers")]
        public bool ShowAnswers { get; set; }
        
        [JsonProperty("lockScreen")]
        public bool LockScreen { get; set; }
        
        [JsonProperty("maxAttempts")]
        public int MaxAttempts { get; set; }
        
        [JsonProperty("members")]
        public List<ContestMemberModel>? Members { get; set; }
    }

    public class CreateContestResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public ContestModel? Data { get; set; }
    }

    public class UpdateContestRequest
    {
        [JsonProperty("title")]
        public string? Title { get; set; }
        
        [JsonProperty("description")]
        public string? Description { get; set; }
        
        [JsonProperty("startDate")]
        public DateTime? StartDate { get; set; }
        
        [JsonProperty("endDate")]
        public DateTime? EndDate { get; set; }
        
        [JsonProperty("duration")]
        public int? Duration { get; set; }
        
        [JsonProperty("visibility")]
        public string? Visibility { get; set; }
        
        [JsonProperty("isDurationLimited")]
        public bool? IsDurationLimited { get; set; }
        
        [JsonProperty("isEndDateSet")]
        public bool? IsEndDateSet { get; set; }
        
        [JsonProperty("allowReview")]
        public string? AllowReview { get; set; }
        
        [JsonProperty("shuffleQuestions")]
        public bool? ShuffleQuestions { get; set; }
        
        [JsonProperty("shuffleAnswers")]
        public bool? ShuffleAnswers { get; set; }
        
        [JsonProperty("hasMultipleChoice")]
        public bool? HasMultipleChoice { get; set; }
        
        [JsonProperty("hasEssay")]
        public bool? HasEssay { get; set; }
        
        [JsonProperty("showScore")]
        public bool? ShowScore { get; set; }
        
        [JsonProperty("showAnswers")]
        public bool? ShowAnswers { get; set; }
        
        [JsonProperty("lockScreen")]
        public bool? LockScreen { get; set; }
        
        [JsonProperty("maxAttempts")]
        public int? MaxAttempts { get; set; }
        
        [JsonProperty("status")]
        public string? Status { get; set; }
    }
}
