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
            StringBuilder ans = new StringBuilder(); //done: appendFormat на Append
            ans.Append((string)values[0]);
            ans.AppendLine();

            DateTime date = (DateTime)values[1];
            ans.Append(date.ToString("d MMM yyyy"));
            
            //done: крутой способ
            return $"{values[0]}\n{date.ToString("d MMM yyyy")}";
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
