using Staff_time.Model;
using Staff_time.Model.UserModel;
using Staff_time.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
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
using System.Windows.Shapes;

namespace Staff_time.View
{
    /// <summary>
    /// Логика взаимодействия для AddDialogWindow.xaml
    /// </summary>
    public partial class TimeStatisticsWindow : Window, IDialogView, INotifyPropertyChanged
    {
        public TimeStatisticsWindow()
        {
            InitializeComponent();
            YearsList = new List<string>() { "2019" };
            MonthList = new List<string>(){"Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь",
            "Ноябрь","Декабрь"};
            SelectedYearIndex = 0;
            SelectedMonthIndex = DateTime.Now.Month - 1;
            base.DataContext = this;
        }

        public void ReloadList(object sender, EventArgs e)
        {
            int yearInt = int.Parse(YearsList[SelectedYearIndex]);
            var periodBegin = new DateTime(yearInt, SelectedMonthIndex + 1, 1);
            var periodEnd = new DateTime(periodBegin.Year, periodBegin.Month, DateTime.DaysInMonth(yearInt, periodBegin.Month));

            var monthPlanTime = Context.timeTableWork.GetTimeForAMonth(periodBegin.Year, periodBegin.Month);

            //WorksList = Context.workWork.GetWorksForAMonth(periodBegin.Year, periodBegin.Month);
            var wrk = WorksVM.Dictionary.Select(p => p.Value.Work).Where(w => w.StartDate >= periodBegin && w.StartDate <= periodEnd &&
           w.UserID == GlobalInfo.CurrentUser.ID);

            WorksList = monthPlanTime.GroupJoin(wrk, p => p.Date, w => w.StartDate, (mpt, w) => new { tt = mpt, work = w })
            .DefaultIfEmpty()
            .Where(q => q.tt.PlanningTime > 0.0 || q.work.Sum(m => m.Minutes) > 0)
            .Select(tmp => new LaborCostsDay
            {
                DateTm = tmp.tt.Date,
                Time = tmp.work.Sum(q => q.Minutes) / 60.0,
                PlanTime = tmp.tt.PlanningTime
            }).ToList();

            SumTime = WorksList.Sum(w => w.Time);
            SumTimePlan = WorksList.Sum(w => w.PlanTime);
            DiffTime = SumTime - SumTimePlan;
        }

        public List<string> YearsList { get; set; }
        public List<string> MonthList { get; set; }
        public int SelectedYearIndex { get; set; }
        public int SelectedMonthIndex { get; set; }

        private double _sumTime;
        public double SumTime
        {
            get
            {
                return _sumTime;
            }
            set
            {
                _sumTime = value;
                NotifyPropertyChanged("SumTime");
            }
        }

        private double _sumTimePlan;
        public double SumTimePlan
        {
            get
            {
                return _sumTimePlan;
            }
            set
            {
                _sumTimePlan = value;
                NotifyPropertyChanged("SumTimePlan");
            }
        }

        private double _diffTime;
        public double DiffTime
        {
            get
            {
                return _diffTime;
            }
            set
            {
                _diffTime = value;
                NotifyPropertyChanged("DiffTime");
            }
        }

        private List<LaborCostsDay> _worksList;
        public List<LaborCostsDay> WorksList
        {
            get
            {
                return _worksList;
            }
            set
            {
                _worksList = value;
                NotifyPropertyChanged("WorksList");
            }
        }

        public class LaborCostsDay
        {
            public double Time { get; set; }
            public double PlanTime { get; set; }
            public DateTime DateTm { get; set; }

            public string Date
            {
                get
                {
                    return DateTm.ToString("dd MMM yyyy");
                }
            }
            public string WeekDay
            {
                get
                {
                    return CultureInfo.GetCultureInfo("ru-RU").DateTimeFormat.GetDayName(DateTm.DayOfWeek);
                }
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
}
