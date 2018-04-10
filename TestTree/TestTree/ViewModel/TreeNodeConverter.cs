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
    public class TreeNodeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            List<object> items = new List<object>();

            for (int i = 0; i < values.Length; ++i)
            {
                if (i == 0)
                {
                    ObservableCollection<TreeNodeCustomer> children = (ObservableCollection<TreeNodeCustomer>)values[i];
                    foreach (var c in children)
                        items.Add(c);
                    continue;
                }
                else if (i == 1)
                {
                    ObservableCollection<TreeNodeSpecialty> children = (ObservableCollection<TreeNodeSpecialty>)values[i];
                    foreach (var c in children)
                        items.Add(c);
                    continue;
                }
                else if (i == 2)
                {
                    ObservableCollection<TreeNodeСompany> children = (ObservableCollection<TreeNodeСompany>)values[i];
                    foreach (var c in children)
                        items.Add(c);
                    continue;
                }
                else if (i == 3)
                {
                    ObservableCollection<TreeNodeСontract> children = (ObservableCollection<TreeNodeСontract>)values[i];
                    foreach (var c in children)
                        items.Add(c);
                    continue;
                }
                else if (i == 4)
                {
                    ObservableCollection<TreeNode> children = (ObservableCollection<TreeNode>)values[i];
                    foreach (var c in children)
                        items.Add(c);
                    continue;
                }
            }
            return items;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // throw new NotSupportedException("Cannot perform reverse-conversion");
            return null;
        }
    }
}
