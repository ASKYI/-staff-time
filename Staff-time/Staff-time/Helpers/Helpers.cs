using Staff_time.Model;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Staff_time.Helpers
{
    enum DatatypeStuff : int
    {
        STRING = 0,         //Строка
        DOUBLE,             //число (дробное)
        DATA,               //дата
        TIME,               //время
        TEXTDOC,            //тестовый документ
        LIST,               //список (type == true (польз. список), false - константный список
        FOLDER,             //папка (применяется для перехода в папку (заказчики)
    }
    public class MessageWorkObject
    {
        public WorkCommandEnum _commandType { get; set; }
        public Work _work { get; set; }
        public DateTime dt { get; set; }
        public MessageWorkObject(WorkCommandEnum type_, Work work_, DateTime dt_)
        {
            _commandType = type_;
            _work = work_;
            dt = dt_;
        }
    }

    public static class TreeHelper
    {
        public static bool IsEqualTreeNodes(TreeNode a, TreeNode b)
        {
            if (a.Task.TaskName.ToLower() != b.Task.TaskName.ToLower())
                return false;
            if (a.Task.ParentTaskID != b.Task.ParentTaskID)
                return false;

            return true;
        }
    }

    public static class FocusExtension
    {
        public static bool GetIsFocused(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject obj, bool value)
        {
            obj.SetValue(IsFocusedProperty, value);
        }

        public static readonly DependencyProperty IsFocusedProperty =
            DependencyProperty.RegisterAttached(
                "IsFocused", typeof(bool), typeof(FocusExtension),
                new UIPropertyMetadata(false, OnIsFocusedPropertyChanged));

        private static void OnIsFocusedPropertyChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var uie = (UIElement)d;
            if (uie != null && (bool)e.NewValue)
            {
                uie.Focus(); // Don't care about false values.
                var obj = (uie as TextBox);
                obj.Dispatcher.BeginInvoke(new Action(() => obj.SelectAll()));
            }
        }
    }

    //public static class CloneClass
    //{
    //    public static T DeepClone<T>(T obj)
    //    {
    //        using (var ms = new MemoryStream())
    //        {
    //            var formatter = new BinaryFormatter();
    //            formatter.Serialize(ms, obj);
    //            ms.Position = 0;

    //            return (T)formatter.Deserialize(ms);
    //        }
    //    }
    //}
}
