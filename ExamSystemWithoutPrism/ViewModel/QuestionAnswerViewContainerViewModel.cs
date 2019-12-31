using ExamSystemWithoutPrism.Interfaces;
using ExamSystemWithoutPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.ViewModel
{
    public class QuestionAnswerViewContainerViewModel : BaseViewModel, IViewModel
    {
        public IQuestionAnswerViewModel _selectedViewModel;
        public event EventHandler<string> ViewModelMessagePublished;
        public IQuestionAnswerViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }
        private QuestionsViewModel _questionsViewModel;

        public QuestionsViewModel QuestionsVM
        {
            get { return _questionsViewModel; }
            set { _questionsViewModel = value; }
        }
        private ReviewViewModel _reviewViewModel;

        public ReviewViewModel ReviewVM
        {
            get { return _reviewViewModel; }
            set { _reviewViewModel = value; }
        }
        private ResultViewModel _resultViewModel;

        public ResultViewModel ResultVM
        {
            get { return _resultViewModel; }
            set { _resultViewModel = value; }
        }

        public QuestionAnswerViewContainerViewModel(string userName)
        {
            _questionsViewModel = new QuestionsViewModel(userName);
            _questionsViewModel.QuestionAnswerVModelMessagePublished += QuestionAnswerVModelMessagePublished;
            _resultViewModel = new ResultViewModel();
            _resultViewModel.QuestionAnswerVModelMessagePublished += QuestionAnswerVModelMessagePublished;
            _reviewViewModel = new ReviewViewModel();
            _reviewViewModel.QuestionAnswerVModelMessagePublished += QuestionAnswerVModelMessagePublished;
            _reviewViewModel.ReviewQuestionSelected += ReviewViewModel_ReviewQuestionSelected;
            SelectedViewModel = _questionsViewModel as IQuestionAnswerViewModel;
        }

        private void ReviewViewModel_ReviewQuestionSelected(object sender, int e)
        {
            if (e > 0 && _questionsViewModel != null)
            {
                _questionsViewModel.LoadQuestionWithOptions(e - 1);
            }
        }

        private void QuestionAnswerVModelMessagePublished(object sender, string e)
        {
            if (sender != null && !string.IsNullOrEmpty(e))
            {
                var questionAnswerViewType = (QuestionAnswerViewType)Enum.Parse(typeof(QuestionAnswerViewType), e);
                switch (questionAnswerViewType)
                {
                    case QuestionAnswerViewType.Review:
                        _reviewViewModel.RefreshReviewModel(_questionsViewModel.QuestionAnswerModels);
                        SelectedViewModel = _reviewViewModel as IQuestionAnswerViewModel;
                        break;
                    case QuestionAnswerViewType.Question:
                        SelectedViewModel = _questionsViewModel as IQuestionAnswerViewModel;
                        break;
                    case QuestionAnswerViewType.Result:
                        SelectedViewModel = _resultViewModel as IQuestionAnswerViewModel;
                        break;
                }
            }
        }

        private void OnViewModelMessagePublished()
        {
            ViewModelMessagePublished?.Invoke(this, string.Empty);
        }
    }
}
