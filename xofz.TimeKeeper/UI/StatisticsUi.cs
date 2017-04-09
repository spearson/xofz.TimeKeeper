namespace xofz.TimeKeeper.UI
{
    using System;
    using xofz.UI;

    public interface StatisticsUi : Ui
    {
        event Action DateChanged;

        DateTime StartDate { get; set; }

        DateTime EndDate { get; set; }

        string TimeWorked { get; set; }

        string AvgDailyTimeWorked { get; set; }
    }
}
