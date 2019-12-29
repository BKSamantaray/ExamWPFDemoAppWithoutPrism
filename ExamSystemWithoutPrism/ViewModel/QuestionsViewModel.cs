using ExamSystemRepository.Controllers;
using ExamSystemRepository.Models;
using ExamSystemWithoutPrism.Helpers;
using ExamSystemWithoutPrism.Interfaces;
using ExamSystemWithoutPrism.Models;
using ExamSystemWithoutPrism.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.ViewModel
{
    public class QuestionsViewModel : BaseViewModel, IQuestionAnswerViewModel
    {
        private const int EXAM_TOTAL_QUESTION_NUMBER = 5; // This can be configured in App.config
        private readonly string _userName;
        private readonly List<QuestionsAnswer> _allQuestionAnswers;
        private readonly List<int> _randomlyIndexNumber;
        private int _currentSequence = 0;
        private DelegateCommand _previousCommand;
        private DelegateCommand _nextCommand;
        private DelegateCommand _reviewCommand;
        private DelegateCommand _submitCommand;
        private QuestionAnswerModel _selectedQuestionAnswerModel;

        public event EventHandler<string> QuestionAnswerVModelMessagePublished;

        public ObservableCollection<QuestionAnswerModel> QuestionAnswerModels { get; private set; }

        public QuestionAnswerModel SelectedQuestionAnswerModel
        {
            get
            {
                return _selectedQuestionAnswerModel == null ? _selectedQuestionAnswerModel = new QuestionAnswerModel() : _selectedQuestionAnswerModel;
            }
            set
            {
                _selectedQuestionAnswerModel = value;
                OnPropertyChanged("SelectedQuestionAnswerModel");
            }
        }


        #region Constructor
        public QuestionsViewModel(string userName)
        {
            _userName = userName;
            _allQuestionAnswers = ReadAllQuestionAnswers();
            _nextCommand = new DelegateCommand(NextCommandExecute, NextCommandCanExecute);
            _previousCommand = new DelegateCommand(PreviousCommandExecute, PreviousCommandCanExecute);
            _reviewCommand = new DelegateCommand(ReviewCommandExecute, ReviewCommandCanExecute);
            _submitCommand = new DelegateCommand(SubmitCommandExecute, SubmitCommandCanExecute);
            _randomlyIndexNumber = GenerateRandomQuestionList();
            MapToQuestionsAnswer();
            LoadQuestionWithOptions(_currentSequence);
        }
        #endregion Constructor
        #region Command
        public DelegateCommand SubmitCommand
        {
            get
            {
                return _submitCommand;
            }
        }

        public DelegateCommand ReviewCommand
        {
            get
            {
                return _reviewCommand;
            }
        }

        public DelegateCommand NextCommand
        {
            get
            {
                return _nextCommand;
            }
        }

        public DelegateCommand PreviousCommand
        {
            get
            {
                return _previousCommand;
            }
        }
        #endregion Command
        private void OnQuestionAnswerVModelMessagePublished(string  message)
        {
            QuestionAnswerVModelMessagePublished?.Invoke(this, message);
        }
        private List<QuestionsAnswer> ReadAllQuestionAnswers()
        {
            RepositoryAdapter repositoryAdapter = new RepositoryAdapter();
            return repositoryAdapter.GetQuestionAnswers();
        }
        private List<int> GenerateRandomQuestionList()
        {
            Random random = new Random(0);
            List<int> randomlyIndexNumber = new List<int>();
            for (int i = 0; i < EXAM_TOTAL_QUESTION_NUMBER; i++)
            {
                var randomNum = random.Next(0, _allQuestionAnswers.Count);
                if (!randomlyIndexNumber.Any(x => x == randomNum))
                {
                    randomlyIndexNumber.Add(randomNum);
                }
                else
                {
                    i--;
                }                
            }
            return randomlyIndexNumber;
        }

        private void MapToQuestionsAnswer()
        {
            if (_allQuestionAnswers != null && _allQuestionAnswers.Count > 0 && _randomlyIndexNumber != null && _randomlyIndexNumber.Count > 0)
            {
                var questionAnswerModels = from questionModel in (from q in _allQuestionAnswers.Select((value, index) => new { value, index })
                                                                  join i in _randomlyIndexNumber on q.index equals i
                                                                  select q.value).Select((value, index) => new { value, index })
                                           select new QuestionAnswerModel
                                           {
                                               SequenceNumber = $"{questionModel.index + 1}. ",
                                               Answer = questionModel.value.Answer,
                                               Question = questionModel.value.Question,
                                               QuestionOptions = GetQuestionOptions(questionModel.value.Options, questionModel.value.OptionType)
                                           };
                if (questionAnswerModels != null && questionAnswerModels.Count() > 0)
                {
                    QuestionAnswerModels = new ObservableCollection<QuestionAnswerModel>(questionAnswerModels);
                }
            }
        }
        private void LoadQuestionWithOptions(int index)
        {
            if (QuestionAnswerModels != null && QuestionAnswerModels.Count > 0)
            {
                SelectedQuestionAnswerModel = QuestionAnswerModels[index];
            }
        }

        private ObservableCollection<QuestionOption> GetQuestionOptions(List<string> options, ExamSystemRepository.Models.OptionType optionType)
        {
            ObservableCollection<QuestionOption> questionOptions = new ObservableCollection<QuestionOption>();
            var optionsSequenceChars = GetSequenceCharacters(options.Count);
            for (int i = 0; i < options.Count; i++)
            {
                var questionOption = new QuestionOption()
                {
                    OptionSequence = optionsSequenceChars[i],
                    IsOptionSelected = false,
                    OptionText = options[i],
                    OptionType = (optionType == ExamSystemRepository.Models.OptionType.Option) ? Models.OptionType.Radiobutton : Models.OptionType.Checkbox
                };
                questionOption.PropertyChanged += QuestionOption_PropertyChanged;
                questionOptions.Add(questionOption);
            }
            return questionOptions;
        }

        private List<char> GetSequenceCharacters(int totalOptions)
        {
            return Enumerable.Range('A', totalOptions).Select(x => (char)x).ToList<char>();
        }

        private void QuestionOption_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOptionSelected")
            {
                var selectedOption = sender as QuestionOption;
                if (selectedOption.IsOptionSelected == true)
                {
                    foreach (var option in SelectedQuestionAnswerModel.QuestionOptions)
                    {
                        if (option.OptionSequence != selectedOption.OptionSequence)
                        {
                            option.IsOptionSelected = false;
                        }
                    }
                }
            }
        }
        #region Command Execute & CanExecute
        private bool SubmitCommandCanExecute(object obj)
        {
            return true;
        }

        private void SubmitCommandExecute(object obj)
        {
        }

        private bool ReviewCommandCanExecute(object obj)
        {
            return true;
        }

        private void ReviewCommandExecute(object obj)
        {
            OnQuestionAnswerVModelMessagePublished(QuestionAnswerViewType.Review.ToString());
        }

        private bool PreviousCommandCanExecute(object obj)
        {
            return _currentSequence > 0;
        }

        private void PreviousCommandExecute(object obj)
        {
            if (_currentSequence > 0)
            {
                _currentSequence -= 1;
                LoadQuestionWithOptions(_currentSequence);
            }
            RaiseCanExecuteForcily();
        }

        private bool NextCommandCanExecute(object obj)
        {
            return _currentSequence < EXAM_TOTAL_QUESTION_NUMBER - 1;
        }

        private void NextCommandExecute(object obj)
        {
            if (_currentSequence < EXAM_TOTAL_QUESTION_NUMBER - 1)
            {
                _currentSequence += 1;
                LoadQuestionWithOptions(_currentSequence);
            }
            RaiseCanExecuteForcily();
        }
        private void RaiseCanExecuteForcily()
        {
            _previousCommand.RaiseCanExecuteChanged();
            _nextCommand.RaiseCanExecuteChanged();
        }
        #endregion Command Execute & CanExecute
    }
}
