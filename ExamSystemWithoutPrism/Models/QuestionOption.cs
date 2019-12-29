using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.Models
{
    public class QuestionOption : BaseModel
    {
        private char _optionSequence;

        public char OptionSequence
        {
            get { return _optionSequence; }
            set
            {
                _optionSequence = value;
                OnPropertyChanged("OptionSequence");
            }
        }

        private OptionType _optionType;

        public OptionType OptionType
        {
            get { return _optionType; }
            set
            {
                _optionType = value;
                OnPropertyChanged("OptionType");
            }
        }
        private string _optionText;

        public string OptionText
        { 
            get { return _optionText; }
            set
            {
                _optionText = value;
                OnPropertyChanged("OptionText");
            }
        }
        private bool _isOptionSelected;

        public bool IsOptionSelected
        {
            get { return _isOptionSelected; }
            set
            {
                _isOptionSelected = value;
                OnPropertyChanged("IsOptionSelected");
            }
        }
    }
}
