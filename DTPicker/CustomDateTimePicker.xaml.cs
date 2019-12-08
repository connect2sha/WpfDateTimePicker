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
using System.Text.RegularExpressions;

namespace DTPicker
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class CustomDateTimePicker : UserControl
    {
        public CustomDateTimePicker()
        {
            InitializeComponent();
        }

        private DateTime selDateTime;

        public DateTime SelectedDateTime
        {
            get
            {
                return selDateTime;
            }

            set
            {
                selDateTime = value;
                DateDisplay.Text = selDateTime.ToShortDateString() + " " + selDateTime.ToShortTimeString();
            }
        }

        public void OpenPopup()
        {
            PopUpCalendarButton.IsChecked = true;
        }

        public void ClosePopup()
        {
            PopUpCalendarButton.IsChecked = false;
        }

        private void CalTime_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.Key == Key.Up)
            {
                SpinValue(1);
            }
            else if (e.Key == Key.Down)
            {
                SpinValue(-1);
            }
            else if (e.Key == Key.Left || e.Key == Key.Right)
            {
                MoveFocus();
            }
            else
            {
                if (e.Key == Key.D0 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 ||
                    e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 ||
                    e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.NumPad0 || e.Key == Key.NumPad1 ||
                    e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 || e.Key == Key.NumPad5 ||
                    e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 || e.Key == Key.NumPad9)
                {
                    if (CalTime.Text.Length > 5)
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
            }
        }

        private void PlusTime_Click(object sender, RoutedEventArgs e)
        {
            SpinValue(1);
        }

        private void MinusTime_Click(object sender, RoutedEventArgs e)
        {
            SpinValue(-1);
        }

        private void CalTime_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            validateTimeInput();

            string TimeFormat = "HH:MM";
            int selStart = CalTime.SelectionStart;

            if (selStart < 0 || selStart > 4)
                return;

            if (TimeFormat.Substring(selStart, 1) == "H")
            {
                CalTime.Select(0, 2);
            }
            else if (TimeFormat.Substring(selStart, 1) == "M")
            {
                CalTime.Select(3, 2);
            }
        }

        private void MoveFocus()
        {
            validateTimeInput();

            string TimeFormat = "HH:MM";
            int selStart = CalTime.SelectionStart;

            if (selStart < 0 || selStart > 4)
                return;

            if (TimeFormat.Substring(selStart, 1) == "H")
            {
                CalTime.Select(3, 2);
            }
            else if (TimeFormat.Substring(selStart, 1) == "M")
            {
                CalTime.Select(0, 2);
            }
        }

        private void SpinValue(int value)
        {
            validateTimeInput();

            string tempDateTime = "";
            DateTime result;
            string TimeFormat = "HH:MM";
            int selStart = CalTime.SelectionStart;

            tempDateTime = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + " " + CalTime.Text;
            DateTime.TryParse(tempDateTime, out result);

            if (selStart < 0 || selStart > 4)
                return;

            if (TimeFormat.Substring(selStart, 1) == "H")
            {
                result = result.AddHours(value);
                CalTime.Text = result.Hour.ToString("00") + ":" + result.Minute.ToString("00");
                CalTime.Select(0, 2);
            }
            else if (TimeFormat.Substring(selStart, 1) == "M")
            {
                result = result.AddMinutes(value);
                CalTime.Text = result.Hour.ToString("00") + ":" + result.Minute.ToString("00");
                CalTime.Select(3, 2);
            }
        }

        private void validateTimeInput()
        {
            DateTime result;
            string tempDateTime = "";
            tempDateTime = DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Day.ToString() + " " + CalTime.Text;

            try
            {
                result = Convert.ToDateTime(tempDateTime);
            }
            catch (Exception ex)
            {
                CalTime.Undo();
                validateTimeInput();
            }
        }

        private void DateDisplay_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        private void CalendarPopup_Opened(object sender, EventArgs e)
        {
            CalDisplay.SelectedDate = selDateTime;
            CalTime.Text = selDateTime.Hour.ToString("00") + ":" + selDateTime.Minute.ToString("00");
        }

        private void CalendarPopup_Closed(object sender, EventArgs e)
        {
            validateTimeInput();

            string tempDateTime = "";
            DateTime result;

            tempDateTime = CalDisplay.SelectedDate.Value.Year.ToString() + "/" + CalDisplay.SelectedDate.Value.Month.ToString() + "/" + CalDisplay.SelectedDate.Value.Day.ToString() + " " + CalTime.Text;
            DateTime.TryParse(tempDateTime, out result);

            selDateTime = result;
            DateDisplay.Text = selDateTime.ToShortDateString() + " " + selDateTime.ToShortTimeString();
        }

        private void CalDisplay_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            validateTimeInput();
        }

    }
}
