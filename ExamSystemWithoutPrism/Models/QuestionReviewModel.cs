using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystemWithoutPrism.Models
{
    public class QuestionReviewModel : BaseModel
    {
        private int _sequenceNumber;

        public int SequenceNumber
        {
            get { return _sequenceNumber; }
            set
            {
                _sequenceNumber = value;
                OnPropertyChanged("SequenceNumber");
            }
        }
        private bool _isAppeared;

        public bool IsAppeared
        {
            get { return _isAppeared; }
            set
            {
                _isAppeared = value;
                OnPropertyChanged("IsAppeared");
            }
        }
    }
}
