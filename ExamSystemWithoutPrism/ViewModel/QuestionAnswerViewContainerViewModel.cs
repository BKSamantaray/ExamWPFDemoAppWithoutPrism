using ExamSystemWithoutPrism.Interfaces;
using ExamSystemWithoutPrism.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace ExamSystemWithoutPrism.ViewModel
{
    public class QuestionAnswerViewContainerViewModel : BaseViewModel, IViewModel
    {
        private int _totalTimeInMinutes;
        private DateTime _maxTotalTime;
        DispatcherTimer _dispatcherTimer;

        public event EventHandler<string> ViewModelMessagePublished;

        private string _remainingTime;
        public string RemainingTime
        {
            get { return _remainingTime; }
            set
            {
                _remainingTime = value;
                OnPropertyChanged("RemainingTime");
            }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }
        private IQuestionAnswerViewModel _selectedViewModel;        
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
            SetTimer();
            _userName = userName;
            _questionsViewModel = new QuestionsViewModel();
            _questionsViewModel.QuestionAnswerVModelMessagePublished += QuestionAnswerVModelMessagePublished;
            _resultViewModel = new ResultViewModel();
            _resultViewModel.QuestionAnswerVModelMessagePublished += QuestionAnswerVModelMessagePublished;
            _reviewViewModel = new ReviewViewModel();
            _reviewViewModel.QuestionAnswerVModelMessagePublished += QuestionAnswerVModelMessagePublished;
            _reviewViewModel.ReviewQuestionSelected += ReviewViewModel_ReviewQuestionSelected;
            SelectedViewModel = _questionsViewModel as IQuestionAnswerViewModel;
        }
        private void SetTimer()
        {
            _totalTimeInMinutes = Convert.ToInt32(ConfigurationManager.AppSettings["TotalTimeInMinutes"]);
            if (_totalTimeInMinutes > 0)
            {
                _maxTotalTime = DateTime.Now.AddMinutes(_totalTimeInMinutes);
                var startingTimeSpan = TimeSpan.FromMinutes(_totalTimeInMinutes);
                RemainingTime = $"{startingTimeSpan.Minutes.ToString().PadLeft(2, '0')}:{startingTimeSpan.Seconds.ToString().PadLeft(2, '0')}";
                _dispatcherTimer = new DispatcherTimer
                {
                    Interval = TimeSpan.FromMilliseconds(500)
                };
                _dispatcherTimer.Tick += DispatcherTimer_Tick;
                _dispatcherTimer.Start();
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var remainingTimeSpan = _maxTotalTime.Subtract(DateTime.Now);
            if (remainingTimeSpan.Seconds > 0)
            {
                RemainingTime = $"{remainingTimeSpan.Minutes.ToString().PadLeft(2, '0')}:{remainingTimeSpan.Seconds.ToString().PadLeft(2, '0')}";
            }
            else
            {
                _dispatcherTimer.Stop();
                RemainingTime = $"{remainingTimeSpan.Minutes.ToString().PadLeft(2, '0')}:{remainingTimeSpan.Seconds.ToString().PadLeft(2, '0')}";
                QuestionAnswerVModelMessagePublished(this, QuestionAnswerViewType.Result.ToString());
            }
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
                        _resultViewModel.LoadResult(_questionsViewModel.QuestionAnswerModels);
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
