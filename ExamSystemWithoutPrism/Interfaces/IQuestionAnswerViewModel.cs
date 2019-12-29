using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.Interfaces
{
    public interface IQuestionAnswerViewModel
    {
        event EventHandler<string> QuestionAnswerVModelMessagePublished;
    }
}
