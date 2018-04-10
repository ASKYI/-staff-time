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
            /*
            //get folder name listing...
            string folder = parameter as string ?? "";
            var folders = folder.Split(',').Select(f => f.Trim()).ToList();
            //...and make sure there are no missing entries
            while (values.Length > folders.Count) folders.Add(String.Empty);

            //this is the collection that gets all top level items


            for (int i = 0; i < values.Length; i++)
            {
                //make sure were working with collections from here...
                IEnumerable childs = values[i] as IEnumerable ?? new List<object> { values[i] };

                string folderName = folders[i];
                if (folderName != String.Empty)
                {
                    //create folder item and assign childs
                    FolderItem folderItem = new FolderItem { Name = folderName, Items = childs };
                    items.Add(folderItem);
                }
                else
                {
                    //if no folder name was specified, move the item directly to the root item
                    foreach (var child in childs) { items.Add(child); }
                }
            }
            */
            return items;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // throw new NotSupportedException("Cannot perform reverse-conversion");
            return null;
        }
    }
}
