namespace xofz.TimeKeeper.Presentation
{
    using System;
    using System.Threading;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.TimeKeeper.Framework;
    using xofz.TimeKeeper.UI;
    using xofz.UI;

    public sealed class HomePresenter : Presenter
    {
        public HomePresenter(
            HomeUi ui, 
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

            this.ui.InKeyTapped += this.ui_InKeyTapped;
            this.ui.OutKeyTapped += this.ui_OutKeyTapped;
            var w = this.web;
            var currentlyIn = w.Run<StatisticsCalculator, bool>(
                calc => calc.ClockedIn());
            UiHelpers.Write(this.ui, () =>
            {
                this.ui.InKeyVisible = !currentlyIn;
                this.ui.OutKeyVisible = currentlyIn;
            });

            this.timer_Elapsed();
            w.Run<xofz.Framework.Timer>(
                t =>
                {
                    t.Elapsed += this.timer_Elapsed;
                    t.Start(1000);
                },
                "HomeTimer");
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        private void ui_InKeyTapped()
        {
            UiHelpers.Write(
                this.ui,
                () =>
                {
                    this.ui.InKeyVisible = false;
                    this.ui.OutKeyVisible = true;
                });
            this.ui.WriteFinished.WaitOne();
            this.writeTimestamp();
        }

        private void ui_OutKeyTapped()
        {
            UiHelpers.Write(
                this.ui,
                () =>
                {
                    this.ui.InKeyVisible = true;
                    this.ui.OutKeyVisible = false;
                });
            this.ui.WriteFinished.WaitOne();
            this.writeTimestamp();
        }

        private void writeTimestamp()
        {
            var w = this.web;
            w.Run<TimestampWriter>(
                writer => writer.Write());
        }

        private void timer_Elapsed()
        {
            var w = this.web;
            var timeThisWeek = w.Run<StatisticsCalculator, TimeSpan>(
                calc => calc.TimeWorkedThisWeek());
            var readableString = w.Run<TimeSpanViewer, string>(
                viewer => viewer.ReadableString(timeThisWeek));

            UiHelpers.Write(
                this.ui, 
                () => this.ui.TimeWorkedThisWeek = readableString);
            this.ui.WriteFinished.WaitOne();
        }

        private int setupIf1;
        private readonly HomeUi ui;
        private readonly MethodWeb web;
    }
}
