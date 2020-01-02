using ExamSystemWithoutPrism.Interfaces;
using ExamSystemWithoutPrism.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.ViewModel
{
    public class ReviewViewModel : BaseViewModel, IQuestionAnswerViewModel
    {
        private readonly DelegateCommand _questionNumberCommand;

        public DelegateCommand QuestionNumberCommand
        {
            get
            {
                return _questionNumberCommand;
            }
        }

        private List<QuestionReviewModel> _reviewModel;

        public List<QuestionReviewModel> ReviewModel
        {
            get { return _reviewModel; }
            set
            {
                _reviewModel = value;
                OnPropertyChanged("ReviewModel");
            }
        }


        public ReviewViewModel()
        {
            _questionNumberCommand = new DelegateCommand(QuestionNumberCommandExecute, QuestionNumberCommandCanExecute);
        }

        private bool QuestionNumberCommandCanExecute(object obj)
        {
            return true;
        }

        private void QuestionNumberCommandExecute(object obj)
        {
            if (obj != null)
            {
                OnQuestionAnswerVModelMessagePublished();
                OnReviewQuestionSelected(Convert.ToInt32(obj));
            }
        }

        public event EventHandler<int> ReviewQuestionSelected;
        public event EventHandler<string> QuestionAnswerVModelMessagePublished;
        private void OnQuestionAnswerVModelMessagePublished()
        {
            QuestionAnswerVModelMessagePublished?.Invoke(this, QuestionAnswerViewType.Question.ToString());
        }
        private void OnReviewQuestionSelected(int questionNumber)
        {
            ReviewQuestionSelected?.Invoke(this, questionNumber);
        }

        public void RefreshReviewModel(IList<QuestionAnswerModel> questionAnswerModels)
        {
            if (questionAnswerModels != null && questionAnswerModels.Count > 0)
            {
                _reviewModel = questionAnswerModels.Select((val, index) => new
                {
                    Value = val,
                    Index = index
                }).Select(x =>
                new QuestionReviewModel
                {
                    IsAppeared = x.Value.QuestionOptions.Any(y => y.IsOptionSelected),
                    SequenceNumber = x.Index + 1
                }).ToList();
            }
        }
    }
}
