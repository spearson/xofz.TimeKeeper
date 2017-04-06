namespace xofz.TimeKeeper.UI
{
    using System;
    using xofz.UI;

    public interface HomeUi : Ui
    {
        event Action InKeyTapped;

        event Action OutKeyTapped;

        bool InKeyVisible { get; set; }

        bool OutKeyVisible { get; set; }

        string TimeWorkedThisWeek { get; set; }
    }
}
