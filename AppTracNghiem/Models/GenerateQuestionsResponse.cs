using System.Collections.Generic;

namespace AppTracNghiem.Models
{
    public class GenerateQuestionsResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public GeneratedQuestionsData Data { get; set; }
    }

    public class GeneratedQuestionsData
    {
        public List<MultipleChoiceQuestionModel> MultipleChoice { get; set; }
        public List<EssayQuestionModel> Essay { get; set; }

        public GeneratedQuestionsData()
        {
            MultipleChoice = new List<MultipleChoiceQuestionModel>();
            Essay = new List<EssayQuestionModel>();
        }
    }

    public class QuestionsListResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public QuestionsListData Data { get; set; }
    }

    public class QuestionsListData
    {
        public List<MultipleChoiceQuestionModel> MultipleChoice { get; set; }
        public List<EssayQuestionModel> Essay { get; set; }
        public int Total { get; set; }

        public QuestionsListData()
        {
            MultipleChoice = new List<MultipleChoiceQuestionModel>();
            Essay = new List<EssayQuestionModel>();
        }
    }
}
