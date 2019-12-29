using ExamSystemWithoutPrism.Models;
using ExamSystemWithoutPrism.ViewModel;
using ExamSystemWithoutPrism.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExamSystemWithoutPrism.Helpers
{
    public class ExamViewDataTemplateSelector: DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            if (container is FrameworkElement frameworkElement && item != null)
            {
                if (item is UserDetailsViewModel)
                {
                    dataTemplate = frameworkElement.FindResource("UserDetailsView") as DataTemplate;
                }
                else if (item is QuestionAnswerViewContainerViewModel)
                {
                    dataTemplate = frameworkElement.FindResource("QuestionAnswerViewContainer") as DataTemplate;
                }
            }
            return dataTemplate;
        }
    }
}
