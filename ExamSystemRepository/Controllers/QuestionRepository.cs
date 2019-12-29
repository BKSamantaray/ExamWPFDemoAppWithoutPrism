using ExamSystemRepository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ExamSystemRepository.Controllers
{
    public class QuestionRepository
    {
        internal List<QuestionsAnswer> GetQuestionsAnswers()
        {
            List<QuestionsAnswer> qustionAnswers = null;
            string questionsAnswerJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RepositoryConstant.SUB_FOLDER_NAME, RepositoryConstant.QUESTION_ANSWER_JSON_FILE_NAME);
            if (!string.IsNullOrEmpty(questionsAnswerJsonFilePath))
            {
                JsonToClassConverter jsonToClassConverter = new JsonToClassConverter();
                qustionAnswers = jsonToClassConverter.ConvertJsonToClassObject<QuestionList>(questionsAnswerJsonFilePath)?.Questions;
            }
            return qustionAnswers;
        }
    }
}
