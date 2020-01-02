using ExamSystemWithoutPrism.Interfaces;
using ExamSystemWithoutPrism.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.ViewModel
{
    public class ResultViewModel : BaseViewModel, IQuestionAnswerViewModel
    {
        public event EventHandler<string> QuestionAnswerVModelMessagePublished;
        private ResultModel _resultData;

        public ResultModel ResultData
        {
            get { return _resultData; }
            set
            {
                _resultData = value;
                OnPropertyChanged("ResultData");
            }
        }
        public void LoadResult(IList<QuestionAnswerModel> questionAnswerModels)
        {
            if (questionAnswerModels != null)
            {
                ResultData = new ResultModel();
                ResultData.TotalQuestionNumber = questionAnswerModels.Count;
                ResultData.TotalAttemptedQuestion = questionAnswerModels.Select(x => x.QuestionOptions.Where(y => y.IsOptionSelected)).SelectMany(x => x).Count();
                ResultData.TotalNotAttemptedQuestion= questionAnswerModels.Select(x => x.QuestionOptions.Where(y => !y.IsOptionSelected)).SelectMany(x => x).Count();
                ResultData.TotalCorrectAnswer = questionAnswerModels.Select(x => new
                {
                    answer = x.Answer,
                    selectedOption = x.QuestionOptions.Where(y => y.IsOptionSelected).Select(y => y.OptionSequence).FirstOrDefault().ToString()
                }).Where(x => x.selectedOption != null).Where(x => x.answer == x.selectedOption).Count();
                var isResultPassed = (ResultData.TotalCorrectAnswer / ResultData.TotalQuestionNumber) * 60;
                var getPassingPercentage = Convert.ToInt32(ConfigurationManager.AppSettings["PassingPercentage"]);
                ResultData.ResultMessage = (isResultPassed >= getPassingPercentage) ? "Passed" : "Failed";
            }
        }
    }
}
