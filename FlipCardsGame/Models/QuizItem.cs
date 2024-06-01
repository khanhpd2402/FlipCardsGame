using System;
using System.Collections.Generic;

namespace FlipCardsGame.Models
{
    public partial class QuizItem
    {
        public int QuestionId { get; set; }
        public string QuestionText { get; set; } = null!;
        public string AnswerA { get; set; } = null!;
        public string AnswerB { get; set; } = null!;
        public string AnswerC { get; set; } = null!;
        public string AnswerD { get; set; } = null!;
        public string AnswerCorrect { get; set; } = null!;
    }
}
