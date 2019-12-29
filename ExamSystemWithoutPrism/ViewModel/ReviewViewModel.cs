using ExamSystemWithoutPrism.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.ViewModel
{
    public class ReviewViewModel : IQuestionAnswerViewModel
    {
        public event EventHandler<string> QuestionAnswerVModelMessagePublished;
    }
}
