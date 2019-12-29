using ExamSystemRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemRepository.Controllers
{
    public class ExamSystemController
    {
        private readonly QuestionRepository _questionRepository = null;
        private readonly RuleRepository _ruleRepository = null;
        public ExamSystemController()
        {
            _questionRepository = new QuestionRepository();
            _ruleRepository = new RuleRepository();
        }
        public List<QuestionsAnswer> GetQuestionsAnswers()
        {
            return _questionRepository.GetQuestionsAnswers();
        }
        public RulesAndRegulations GetRules()
        {
            return _ruleRepository.GetRules();
        }
    }
}
