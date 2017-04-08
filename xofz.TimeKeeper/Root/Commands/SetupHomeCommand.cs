namespace xofz.TimeKeeper.Root.Commands
{
    using System;
    using xofz.Framework;
    using xofz.Framework.Transformation;
    using xofz.Root;
    using xofz.TimeKeeper.Framework;
    using xofz.TimeKeeper.Presentation;
    using xofz.TimeKeeper.UI;
    using xofz.UI;

    public class SetupHomeCommand : Command
    {
        public SetupHomeCommand(
            HomeUi ui,
            HomeNavUi navUi,
            ShellUi mainShell,
            ShellUi navShell,
            MethodWeb web)
        {
            this.ui = ui;
            this.navUi = navUi;
            this.mainShell = mainShell;
            this.navShell = navShell;
            this.web = web;
        }

        public override void Execute()
        {
            this.registerDependencies();

            var w = this.web;
            new HomeNavPresenter(
                    this.navUi,
                    this.navShell,
                    w)
                .Setup();

            new HomePresenter(
                    this.ui,
                    this.mainShell,
                    w)
                .Setup();
        }

        private void registerDependencies()
        {
            var w = this.web;
            w.RegisterDependency(
                new TimestampManager(
                    new EnumerableTrapper<DateTime>()));
            w.RegisterDependency(
                new xofz.Framework.Timer(),
                "HomeTimer");
            w.RegisterDependency(
                new TimeSpanViewer());
            w.RegisterDependency(
                new StatisticsCalculator(w));
        }

        private readonly HomeUi ui;
        private readonly HomeNavUi navUi;
        private readonly ShellUi mainShell;
        private readonly ShellUi navShell;
        private readonly MethodWeb web;
    }
}
