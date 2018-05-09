using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Staff_time.ViewModel
{
    public class WorkControlTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WorkTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
          //  if (item is WorkControlViewModel)
                return WorkTemplate;

      //      return null;
        }
    }
}
