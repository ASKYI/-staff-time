using Staff_time.Model;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
using Xceed.Wpf.Toolkit;

namespace Staff_time.View.Dialog
{
    /// <summary>
    /// Interaction logic for TransferTaskView.xaml
    /// </summary>
    public partial class WorkTimeRangesView : Window, INotifyPropertyChanged
    {
        public WorkTimeRangesView(List<WorkTimeRange> _ranges, double _planningTime)
        {
            WorksTimeRanges = new List<WorkTimeRangeSelected>();
            _ranges.ForEach(r => WorksTimeRanges.Add(new WorkTimeRangeSelected() { range = r, Selected = false }));
            PlanningTime = _planningTime;
            IdealBrakeRanges = new List<IdealRange>();
            IdealBrakeRanges.Add(new IdealRange() { StartHours = 10, StartMinutes = 0, EndHours = 10, EndMinutes = 30 });
            IdealBrakeRanges.Add(new IdealRange() { StartHours = 12, StartMinutes = 0, EndHours = 13, EndMinutes = 0 });
            IdealBrakeRanges.Add(new IdealRange() { StartHours = 15, StartMinutes = 0, EndHours = 15, EndMinutes = 30 });
            UpdateStatus();

            InitializeComponent();
            DataContext = this;
        }

        List<IdealRange> IdealBrakeRanges;
       
        private WorkTimeRangeSelected LastSelected { get; set; }
        public List<WorkTimeRangeSelected> WorksTimeRanges { get; set; }

      
        #region helpful classes
        public class WorkTimeRangeSelected : INotifyPropertyChanged
        {
            public WorkTimeRange range { get; set; }
            private bool _selected;
            public bool Selected
            {
                get
                {
                    return _selected;
                }
                set
                {
                    _selected = value;
                    NotifyPropertyChanged("Selected");
                }
            }
            #region INotifyPropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged(String aPropertyName)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
            }

            #endregion

        }
        struct IdealRange
        {
            public int StartHours { get; set; }
            public int StartMinutes { get; set; }
            public int EndHours { get; set; }
            public int EndMinutes { get; set; }
        }
        #endregion

        #region properties

        private int SortDirection = -1;

        public string _reasonText;
        public string ReasonText
        {
            get
            {
                return _reasonText;
            }
            set
            {
                _reasonText = value;
                NotifyPropertyChanged("ReasonText");
            }
        }

        public bool WasEdited { get; set; } = false;

        private int BadRange { get; set; }
        private double PlanningTime { get; set; }

        private bool _isCorrect;
        public bool IsCorrect
        {
            get
            {
                return _isCorrect;
            }
            set
            {
                _isCorrect = value;
                if (_isCorrect)
                {
                    StatusText = "Диапазоны корректны!";
                    NameStatusSource = "../../Resources/validationOK.ico";
                    StatusColor = new SolidColorBrush(Colors.PaleGreen); // (Color)ColorConverter.ConvertFromString("#99D8DF");
                }
                else
                {
                    StatusText = "Диапазоны не корректны!";
                    NameStatusSource = "../../Resources/delete.ico";
                    StatusColor = new SolidColorBrush(Colors.Salmon); //(Color)ColorConverter.ConvertFromString("#FFFA8072");
                }
            }
        }

        private SolidColorBrush _statusColor;
        public SolidColorBrush StatusColor
        {
            get
            {
                return _statusColor;
            }
            set
            {
                _statusColor = value;
                NotifyPropertyChanged("StatusColor");
            }
        }

        private string _nameStatusSource;
        public String NameStatusSource
        {
            get
            {
                return _nameStatusSource;
            }
            set
            {
                _nameStatusSource = value;
                NotifyPropertyChanged("NameStatusSource");
            }
        }
        private string _statusText;
        public string StatusText
        {
            get
            {
                return _statusText;
            }
            set
            {
                _statusText = value;
                NotifyPropertyChanged("StatusText");
            }
        }
        #endregion

        #region events
        private void UpdateStatus()
        {
            Dictionary<DateTime, DateTime> brakes = new Dictionary<DateTime, DateTime>();
            var incorrectRanges = WorksTimeRanges.Where(r => r.range.StartTime.Year != 1899 || r.range.EndTime.Year != 1899).ToList();
            incorrectRanges.ForEach(r => r.range.StartTime = new DateTime(1899, 12, 30, r.range.StartTime.Hour, r.range.StartTime.Minute, 0));
            incorrectRanges.ForEach(r => r.range.EndTime = new DateTime(1899, 12, 30, r.range.EndTime.Hour, r.range.EndTime.Minute, 0));
            WorksTimeRanges = WorksTimeRanges.OrderBy(r => r.range.StartTime).ToList();
            double sumTime = 0;
            if (LastSelected != null)
                LastSelected.Selected = false;
            ReasonText = "";
            DateTime? last = null;
            foreach (var rng in WorksTimeRanges)
            {
                if (last != null)
                {
                    if (rng.range.StartTime < last.Value)
                    {
                        ReasonText = "Диапазоны перекрываются";
                        rng.Selected = true;
                        LastSelected = rng;
                        IsCorrect = false;
                        return;
                    }
                    if ((DateTime)last != rng.range.StartTime)
                        brakes.Add((DateTime)last, rng.range.StartTime);
                }
                last = rng.range.EndTime;
                var duration = (rng.range.EndTime - rng.range.StartTime);
                sumTime += duration.Hours * 60 + duration.Minutes;
            }
            int cnt = 0;
            foreach (var brakeRange in brakes)
            {
                if (brakeRange.Key.Hour != IdealBrakeRanges[cnt].StartHours || brakeRange.Key.Minute != IdealBrakeRanges[cnt].StartMinutes ||
                    brakeRange.Value.Hour != IdealBrakeRanges[cnt].EndHours || brakeRange.Value.Minute != IdealBrakeRanges[cnt].EndMinutes)
                {
                    var badRange = WorksTimeRanges.FirstOrDefault(r => (r.range.StartTime.Hour == brakeRange.Value.Hour &&
                    r.range.StartTime.Minute == brakeRange.Value.Minute) || (r.range.EndTime.Hour == brakeRange.Key.Hour &&
                    r.range.EndTime.Minute == brakeRange.Key.Minute));
                    if (badRange != null)
                    {
                        ReasonText = "Перерывы не совпадают";
                        badRange.Selected = true;
                        LastSelected = badRange;
                        IsCorrect = false;
                    }
                    return;
                }
                cnt++;
            }
            if (sumTime != PlanningTime * 60)
            {
                ReasonText = "Планируемое и фактическое время не совпадают";
                IsCorrect = false;
                return;
            }
            IsCorrect = true;
        }

        public void KeyDownStart(object sender, KeyEventArgs e)
        {
            var textBoxTime = (MaskedTextBox)sender;
            if (e.Key == Key.Tab)
                return;

            var text = textBoxTime.Text;
            var curInsertPos = textBoxTime.CaretIndex;
            switch (curInsertPos)
            {
                case 0:
                    if (!(e.Key >= Key.D0 && e.Key <= Key.D2) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad2))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 1:
                    string hour1 = text[0].ToString();
                    if (hour1 == "2" && (!(e.Key >= Key.D0 && e.Key <= Key.D3) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad3)))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 2:
                    if (!(e.Key >= Key.D0 && e.Key <= Key.D5) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad5))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
                case 3:
                    if (!(e.Key >= Key.D0 && e.Key <= Key.D9) && !(e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
                    {
                        e.Handled = true;
                        return;
                    }
                    break;
            }
            e.Handled = false;
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateStatus_Click(object sender, RoutedEventArgs e)
        {
            UpdateStatus();
        }

        private void SortRanges_Click(object sender, RoutedEventArgs e)
        {
            if (SortDirection == -1)
                SortDirection = 0;
            else
                SortDirection ^= 1;
            if (SortDirection == 0)
                WorksTimeRanges = WorksTimeRanges.OrderBy(r => r.range.StartTime).ToList();
            else
                WorksTimeRanges = WorksTimeRanges.OrderByDescending(r => r.range.StartTime).ToList();

            NotifyPropertyChanged("WorksTimeRanges");
        }
        private void DoubleClickTime(object sender, MouseButtonEventArgs e)
        {
            var curTime = new DateTime(1899, 12, 30, DateTime.Now.Hour, DateTime.Now.Minute, 0);
            var tmControl = (MaskedTextBox)sender;
            tmControl.Text = curTime.ToString("HH:mm");
        }

        private void TimeGotFocus(object sender, RoutedEventArgs e)
        {
            WasEdited = true;
            TextBox text = sender as TextBox;
            text.Dispatcher.BeginInvoke(new Action(() => text.SelectAll()));
        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String aPropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(aPropertyName));
        }
        #endregion
    }
}
