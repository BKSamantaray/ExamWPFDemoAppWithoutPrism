using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.Models
{
    public class QuestionAnswerModel: BaseModel
    {
        private string _sequenceNumber;

        public string SequenceNumber
        {
            get { return _sequenceNumber; }
            set
            {
                _sequenceNumber = value;
                OnPropertyChanged("SequenceNumber");
            }
        }
        private string _question;

        public string Question
        {
            get { return _question; }
            set
            {
                _question = value;
                OnPropertyChanged("Question");
            }
        }
        private string _answer;

        public string Answer
        {
            get { return _answer; }
            set
            {
                _answer = value;
                OnPropertyChanged("Answer");
            }
        }
        private ObservableCollection<QuestionOption> _questionOptions;

        public ObservableCollection<QuestionOption> QuestionOptions
        {
            get { return _questionOptions; }
            set
            {
                _questionOptions = value;
                OnPropertyChanged("QuestionOptions");
            }
        }
    }
}
