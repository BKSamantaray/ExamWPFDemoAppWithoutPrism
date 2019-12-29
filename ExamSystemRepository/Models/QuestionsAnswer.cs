using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemRepository.Models
{
    public class QuestionsAnswer
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public OptionType OptionType { get; set; }
        public string Answer { get; set; }
    }

    public class QuestionList
    {
        public List<QuestionsAnswer> Questions { get; set; }
    }
}
