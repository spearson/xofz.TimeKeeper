namespace xofz.TimeKeeper.Presentation
{
    using System;
    using System.Threading;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.TimeKeeper.Framework;
    using xofz.TimeKeeper.UI;
    using xofz.UI;

    public sealed class StatisticsPresenter : Presenter
    {
        public StatisticsPresenter(
            StatisticsUi ui, 
            ShellUi shell,
            MethodWeb web) 
            : base(ui, shell)
        {
            this.ui = ui;
            this.web = web;
        }

        public void Setup()
        {
            if (Interlocked.CompareExchange(ref this.setupIf1, 1, 0) == 1)
            {
                return;
            }

            var w = this.web;
            this.ui.DateChanged += this.ui_DateChanged;
            var startOfWeek = w.Run<DateCalculator, DateTime>(
                calc => calc.StartOfWeek());
            UiHelpers.Write(
                this.ui, 
                () => this.ui.StartDate = startOfWeek);
            this.ui.WriteFinished.WaitOne();
            this.timer_Elapsed();

            w.Run<xofz.Framework.Timer>(
                t =>
                {
                    t.Elapsed += this.timer_Elapsed;
                    t.Start(1000);
                },
                "StatisticsTimer");

            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        private void ui_DateChanged()
        {
            this.computeStatistics();
        }

        private void timer_Elapsed()
        {
            var today = DateTime.Now.Date;
            if (Interlocked.CompareExchange(ref this.firstTick, 1, 0) == 0)
            {
                this.currentDate = today;
            }

            var endDate = UiHelpers.Read(
                this.ui, 
                () => this.ui.EndDate);
            if (today == endDate.AddDays(1))
            {
                UiHelpers.Write(
                    this.ui, 
                    () => this.ui.EndDate = today);
                this.currentDate = today;
            }

            this.computeStatistics();
        }

        private void computeStatistics()
        {
            var startDate = UiHelpers.Read(this.ui, () => this.ui.StartDate);
            var endDate = UiHelpers.Read(this.ui, () => this.ui.EndDate).AddDays(1);
            var w = this.web;
            var timeWorked = w.Run<StatisticsCalculator, TimeSpan>(
                calc => calc.TimeWorked(startDate, endDate));
            var readableString = w.Run<TimeSpanViewer, string>(
                viewer => viewer.ReadableString(timeWorked));
            // ReSharper disable once AccessToModifiedClosure
            // because we are waiting on the UI write to finish,
            // this variable will not be overwritten by its second assignment
            // until it is ready
            UiHelpers.Write(this.ui, () => this.ui.TimeWorked = readableString);
            this.ui.WriteFinished.WaitOne();
            var avgDaily = w.Run<StatisticsCalculator, TimeSpan>(
                calc => calc.AverageDailyTimeWorked(startDate, endDate));
            readableString = w.Run<TimeSpanViewer, string>(
                viewer => viewer.ReadableString(avgDaily));
            UiHelpers.Write(this.ui, () => this.ui.AvgDailyTimeWorked = readableString);
            this.ui.WriteFinished.WaitOne();
        }

        private int setupIf1, firstTick;
        private DateTime currentDate;
        private readonly StatisticsUi ui;
        private readonly MethodWeb web;
    }
}
