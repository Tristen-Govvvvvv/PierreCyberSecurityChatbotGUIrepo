using System.Collections.Generic;

namespace PierreCyberSecurityBotPROG
{
    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswer { get; set; }
        public string Explanation { get; set; }
    }
}//
