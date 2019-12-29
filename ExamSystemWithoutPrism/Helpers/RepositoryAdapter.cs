using ExamSystemRepository.Controllers;
using ExamSystemRepository.Models;
using ExamSystemWithoutPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.Helpers
{
    public class RepositoryAdapter
    {
        private readonly ExamSystemController _examSystemController;
        public RepositoryAdapter()
        {
            _examSystemController = new ExamSystemController();
        }
        internal List<RuleRegulation> GetRules()
        {
            List<RuleRegulation> ruleRegulation = null;
            var getRules = _examSystemController.GetRules();
            if (getRules != null)
            {
                ruleRegulation = getRules.Rules.Select((x, y) => new RuleRegulation()
                {
                    SequenceNumber = $"({y + 1}): ",
                    Rule = x
                }).ToList();

            }
            return ruleRegulation;
        }
        internal List<QuestionsAnswer> GetQuestionAnswers()
        {
            return _examSystemController.GetQuestionsAnswers();
        }
    }
}
