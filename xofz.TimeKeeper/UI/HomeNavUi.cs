namespace xofz.TimeKeeper.UI
{
    using System;
    using xofz.UI;

    public interface HomeNavUi : Ui
    {
        event Action StatisticsKeyTapped;

        event Action ExitKeyTapped;
    }
}
