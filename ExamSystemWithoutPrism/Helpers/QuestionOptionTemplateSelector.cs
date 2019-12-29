using ExamSystemWithoutPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ExamSystemWithoutPrism.Helpers
{
    public class QuestionOptionTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate dataTemplate = null;
            FrameworkElement frameworkElement = container as FrameworkElement;
            if (frameworkElement != null && item != null)
            {
                var questionOption = item as QuestionOption;
                var optionType = (OptionType)Enum.Parse(typeof(OptionType), questionOption.OptionType.ToString());
                switch (optionType)
                {
                    case OptionType.Checkbox:
                        dataTemplate = frameworkElement.FindResource("CheckBoxTemplate") as DataTemplate;
                        break;
                    case OptionType.Radiobutton:
                        dataTemplate = frameworkElement.FindResource("RadioButonTemplate") as DataTemplate;
                        break;
                }
            }
            return dataTemplate;
        }
    }
}
