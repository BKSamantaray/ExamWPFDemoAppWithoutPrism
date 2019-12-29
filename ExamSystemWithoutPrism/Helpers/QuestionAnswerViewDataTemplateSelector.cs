using ExamSystemWithoutPrism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExamSystemWithoutPrism.Helpers
{
    public class QuestionAnswerViewDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            if (container is FrameworkElement frameworkElement && item != null)
            {
                if (item is QuestionsViewModel)
                {
                    dataTemplate = frameworkElement.FindResource("QuestionsView") as DataTemplate;
                }
                else if (item is ReviewViewModel)
                {
                    dataTemplate = frameworkElement.FindResource("ReviewView") as DataTemplate;
                }
                else if (item is ResultViewModel)
                {
                    dataTemplate = frameworkElement.FindResource("ResultView") as DataTemplate;
                }
            }
            return dataTemplate;
        }
    }
}
