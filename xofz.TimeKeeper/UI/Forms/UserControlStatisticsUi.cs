namespace xofz.TimeKeeper.UI.Forms
{
    using System;
    using System.Threading;
    using System.Windows.Forms;
    using xofz.UI.Forms;

    public partial class UserControlStatisticsUi : UserControlUi, StatisticsUi
    {
        public UserControlStatisticsUi()
        {
            this.InitializeComponent();
        }

        public event Action DateChanged;

        DateTime StatisticsUi.StartDate
        {
            get => this.startDatePicker.SelectionRange.Start;
            set => this.startDatePicker.SetDate(value);
        }

        DateTime StatisticsUi.EndDate
        {
            get => this.endDatePicker.SelectionRange.Start;
            set => this.endDatePicker.SetDate(value);
        }

        string StatisticsUi.TimeWorked
        {
            get => this.timeWorkedLabel.Text;

            set => this.timeWorkedLabel.Text = value;
        }

        string StatisticsUi.AvgDailyTimeWorked
        {
            get => this.avgDailyLabel.Text;

            set => this.avgDailyLabel.Text = value;
        }

        private void startDatePicker_DateChanged(object sender, DateRangeEventArgs e)
        {
            new Thread(() => this.DateChanged?.Invoke()).Start();
        }

        private void endDatePicker_DateChanged(object sender, DateRangeEventArgs e)
        {
            new Thread(() => this.DateChanged?.Invoke()).Start();
        }
    }
}
