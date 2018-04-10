using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Data;
using System.Globalization;
using System.Collections.ObjectModel;

namespace TestTree.ViewModel
{
    public class TabItemHeaderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder ans = new StringBuilder();
            ans.AppendFormat((string)values[0]);
            ans.AppendLine();

            DateTime date = (DateTime)values[1];
            ans.AppendFormat(date.ToString("d MMM yyyy"));

            return ans.ToString();
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // throw new NotSupportedException("Cannot perform reverse-conversion");
            return null;
        }
    }
}
