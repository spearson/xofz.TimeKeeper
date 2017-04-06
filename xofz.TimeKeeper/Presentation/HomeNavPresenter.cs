namespace xofz.TimeKeeper.Presentation
{
    using System.Threading;
    using xofz.Framework;
    using xofz.Presentation;
    using xofz.TimeKeeper.UI;
    using xofz.UI;

    public sealed class HomeNavPresenter : Presenter
    {
        public HomeNavPresenter(
            HomeNavUi ui, 
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

            this.ui.StatisticsKeyTapped += this.ui_StatisticsKeyTapped;
            this.ui.ExitKeyTapped += this.ui_ExitKeyTapped;

            var w = this.web;
            w.Run<Navigator>(n => n.RegisterPresenter(this));
        }

        private void ui_StatisticsKeyTapped()
        {
        }

        private void ui_ExitKeyTapped()
        {
            var w = this.web;
            w.Run<Navigator>(n => n.Present<ShutdownPresenter>());
        }

        private int setupIf1;
        private readonly HomeNavUi ui;
        private readonly MethodWeb web;
    }
}
