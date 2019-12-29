using ExamSystemWithoutPrism.Interfaces;
using ExamSystemWithoutPrism.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamSystemWithoutPrism.ViewModel;
using ExamSystemWithoutPrism.Models;
using System.Windows.Threading;

namespace ExamSystemWithoutPrism.ViewModel
{
    public class ViewContainerViewModel : BaseViewModel
    {
        private IViewModel _selectedViewModel;
        private DateTime _currentDateTime;

        public IViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged("SelectedViewModel");
            }
        }
        public DateTime CurrentDateTime
        {
            get
            {
                return _currentDateTime;
            }
            set
            {
                _currentDateTime = value;
                OnPropertyChanged("CurrentDateTime");
            }
        }

        public ViewContainerViewModel()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();
            SelectedViewModel= new UserDetailsViewModel();
            ((UserDetailsViewModel)SelectedViewModel).ViewModelMessagePublished += ViewContainerViewModel_ViewModelMessagePublished;
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            CurrentDateTime = DateTime.Now;
        }

        private void ViewContainerViewModel_ViewModelMessagePublished(object sender, string e)
        {
            if (sender is UserDetailsViewModel && !string.IsNullOrEmpty(e))
            {
                SelectedViewModel = new QuestionAnswerViewContainerViewModel(e);
            }
        }
    }
}
