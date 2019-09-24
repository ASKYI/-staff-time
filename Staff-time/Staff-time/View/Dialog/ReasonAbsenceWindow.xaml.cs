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
    public partial class ReasonAbsenceWindow : Window, IDialogView
    {
        public List<Reason> Reasons { get; set; }
        public ReasonAbsenceWindow(int year, int month)
        {
            InitializeComponent();
            Reason emptyReason = new Reason();
            //emptyReason.ReasonText = "";
            //emptyReason.ID = 0;
            Reasons = new List<Reason>();
            Reasons.Add(emptyReason);
            Reasons.AddRange(Context.workWork.GetReasonAbsence());
            SelectedReason = Reasons[0];
            calendar.DisplayDate = new DateTime(year, month, 1);
            calendar.SelectedDate = new DateTime(year, month, 1);
            base.DataContext = this;
        }
        public Reason SelectedReason { get; set; }

        public void Apply_Click(object sender, EventArgs e)
        {
            Mouse.SetCursor(Cursors.Wait);
            if (SelectedReason == null || SelectedReason.ID == 0) //Очистка на эти даты
            {
                Context.workWork.UpdateReasonAbsence(calendar.SelectedDates.ToList(), null);
                MessageBox.Show("Причина отсутствия успешно сброшена!", "Сброс причины отсутствия", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                Context.workWork.UpdateReasonAbsence(calendar.SelectedDates.ToList(), SelectedReason.ID);
                MessageBox.Show($"Причина '{SelectedReason.ReasonText}' успешно установлена!", "Установка причины отсутствия", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Mouse.SetCursor(Cursors.Arrow);
            Close();
        }

        protected override void OnPreviewMouseUp(MouseButtonEventArgs e) //Иначе календать захватывает фокус и приходится на кнопку снаружи нажимать дважды (забрать фокус и собственно нажатие)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is System.Windows.Controls.Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }
    }
}
