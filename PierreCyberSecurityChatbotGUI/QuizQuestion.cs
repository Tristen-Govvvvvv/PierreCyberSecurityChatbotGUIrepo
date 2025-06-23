using System.Collections.Generic;

namespace PierreCyberSecurityBotPROG
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectOptionIndex { get; set; }
        public string Explanation { get; set; }
        public bool IsTrueFalse { get; set; } = false;
    }
}//
