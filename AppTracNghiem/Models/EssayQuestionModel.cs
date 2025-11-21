using System;
using System.Collections.Generic;

namespace AppTracNghiem.Models
{
    public class EssayQuestionModel
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public string SampleAnswer { get; set; }
        public string GradingCriteria { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public string Difficulty { get; set; }
        public List<string> Keywords { get; set; }
        public string CreatedBy { get; set; }
        public string Contest { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public EssayQuestionModel()
        {
            Keywords = new List<string>();
        }
    }
}
