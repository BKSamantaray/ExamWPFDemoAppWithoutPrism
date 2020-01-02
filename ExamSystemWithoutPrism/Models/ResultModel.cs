using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.Models
{
    public class ResultModel : BaseModel
    {
        private string _resultMessage;

        public string ResultMessage
        {
            get { return _resultMessage; }
            set
            {
                _resultMessage = value;
                OnPropertyChanged("ResultMessage");
            }
        }
        private int _totalQuestionNumber;

        public int TotalQuestionNumber
        {
            get { return _totalQuestionNumber; }
            set
            {
                _totalQuestionNumber = value;
                OnPropertyChanged("TotalQuestionNumber");
            }
        }
        private int _totalAttemptedQuestion;

        public int TotalAttemptedQuestion
        {
            get { return _totalAttemptedQuestion; }
            set
            {
                _totalAttemptedQuestion = value;
                OnPropertyChanged("TotalAttemptedQuestion");
            }
        }
        private int _totalNotAttemptedQuestion;

        public int TotalNotAttemptedQuestion
        {
            get { return _totalNotAttemptedQuestion; }
            set
            {
                _totalNotAttemptedQuestion = value;
                OnPropertyChanged("TotalNotAttemptedQuestion");
            }
        }
        private int _totalCorrectAnswer;

        public int TotalCorrectAnswer
        {
            get { return _totalCorrectAnswer; }
            set
            {
                _totalCorrectAnswer = value;
                OnPropertyChanged("TotalCorrectAnswer");
            }
        }
    }
}
