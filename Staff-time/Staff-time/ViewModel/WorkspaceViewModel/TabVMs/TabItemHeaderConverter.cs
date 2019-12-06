using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Data;
using System.Globalization;
using System.Collections.ObjectModel;

namespace Staff_time.ViewModel
{
    public class TabItemHeaderConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            StringBuilder ans = new StringBuilder();
            ans.AppendFormat((string)values[0]); // todo appendFormat подразумевает форматирование, здесь достаточного простого Append
            ans.AppendLine();

            DateTime date = (DateTime)values[1];
            ans.AppendFormat(date.ToString("d MMM yyyy"));

            return ans.ToString();

            // todo здесь было понятнее вот так
            //return $"{values[0]}\n{date.ToString("d MMM yyyy")}";
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // throw new NotSupportedException("Cannot perform reverse-conversion");
            return null;
        }
    }
}
