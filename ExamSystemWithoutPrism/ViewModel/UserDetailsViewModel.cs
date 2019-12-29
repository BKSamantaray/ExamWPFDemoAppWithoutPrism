using ExamSystemWithoutPrism.Helpers;
using ExamSystemWithoutPrism.Interfaces;
using ExamSystemWithoutPrism.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ExamSystemWithoutPrism.ViewModel
{
    public class UserDetailsViewModel : BaseViewModel, IViewModel
    {
        public ObservableCollection<RuleRegulation> RuleRegulations { get; set; }
        public event EventHandler<string> ViewModelMessagePublished;
        private readonly DelegateCommand _continueCommand;
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

        public DelegateCommand ContinueCommand
        {
            get
            {
                return _continueCommand;
            }
        }

        public UserDetailsViewModel()
        {
            _continueCommand = new DelegateCommand(ContinueCommandExecute, ContinueCommandCanExecute);
            var ruleRegulations = GetRuleRegulations();
            RuleRegulations = new ObservableCollection<RuleRegulation>(ruleRegulations);
        }
        private void ContinueCommandExecute(object param)
        {
            OnViewModelMessagePublished();
        }
        private bool ContinueCommandCanExecute(object param)
        {
            return true;
        }
        private List<RuleRegulation> GetRuleRegulations()
        {
            RepositoryAdapter repositoryAdapter = new RepositoryAdapter();
            return repositoryAdapter.GetRules();
        }
        private void OnViewModelMessagePublished()
        {
            ViewModelMessagePublished?.Invoke(this, UserName);
        }
    }
}
