using System;
using System.Collections.Generic;

namespace AppTracNghiem.Models
{
    public class MultipleChoiceQuestionModel
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public List<OptionModel> Options { get; set; }
        public string Explanation { get; set; }
        public string Difficulty { get; set; }
        public string CreatedBy { get; set; }
        public string Contest { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public MultipleChoiceQuestionModel()
        {
            Options = new List<OptionModel>();
        }
    }

    public class OptionModel
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
