using ExamSystemRepository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemRepository.Controllers
{
    public class RuleRepository
    {
        internal RulesAndRegulations GetRules()
        {
            RulesAndRegulations rulesAndRegulations = null;
            string rulesJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RepositoryConstant.SUB_FOLDER_NAME, RepositoryConstant.RULES_JSON_FILE_NAME);
            if (!string.IsNullOrEmpty(rulesJsonFilePath))
            {
                JsonToClassConverter jsonToClassConverter = new JsonToClassConverter();
                rulesAndRegulations = jsonToClassConverter.ConvertJsonToClassObject<RulesAndRegulations>(rulesJsonFilePath);
            }
            return rulesAndRegulations;
        }
    }
}
