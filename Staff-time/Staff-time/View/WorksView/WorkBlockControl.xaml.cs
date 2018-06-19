using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для WorkBlockControl.xaml
    /// </summary>
    public partial class WorkBlockControl : UserControl
    {
        public WorkBlockControl()
        {
            InitializeComponent();
        }

        private void StartTime_Changed(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            //DateTime old = Work.StartDate;
            //Work.StartDate = new DateTime(old.Year, old.Month, old.Day, //Я не придумала ничего лучше
            //    e.NewTime.Hours, e.NewTime.Minutes, e.NewTime.Seconds);
            //RaisePropertyChanged("StartHours");
        }

        private void EndTime_Changed(object sender, AC.AvalonControlsLibrary.Controls.TimeSelectedChangedRoutedEventArgs e)
        {
            //DateTime old = Work.EndDate;
            //Work.EndDate = new DateTime(old.Year, old.Month, old.Day,
            //    e.NewTime.Hours, e.NewTime.Minutes, e.NewTime.Seconds);
            //RaisePropertyChanged("StartHours");
        }

        private void workBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            ((ViewModel.WorkBlockControlViewModel)DataContext).MouseLeft = true;
        }

        private void workBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            ((ViewModel.WorkBlockControlViewModel)DataContext).MouseLeft = false;
        }
    }
}
