namespace xofz.TimeKeeper.Framework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using xofz.Framework;

    public class StatisticsCalculator
    {
        public StatisticsCalculator(MethodWeb web)
        {
            this.web = web;
        }

        public virtual bool ClockedIn()
        {
            return this.allTimes().Count % 2 == 1;
        }

        public virtual TimeSpan TimeWorkedThisWeek()
        {
            var w = this.web;
            var beginning = w.Run<DateCalculator, DateTime>(
                calc => calc.StartOfWeek());

            return this.TimeWorked(beginning, DateTime.Today.AddDays(1));
        }

        public virtual TimeSpan TimeWorked(DateTime beginning, DateTime end)
        {
            var allTimes = this.allTimes();
            var now = DateTime.Now;
            TimeSpan timeWorked = TimeSpan.Zero;
            for (var i = 0; i < allTimes.Count - 1; i += 2)
            {
                if (allTimes[i + 1] < beginning)
                {
                    continue;
                }

                if (allTimes[i + 1] > end)
                {
                    break;
                }

                timeWorked += allTimes[i + 1] - allTimes[i];
            }

            if (allTimes.Count % 2 == 1 && end > DateTime.Today)
            {
                timeWorked += now - allTimes[allTimes.Count - 1];
            }

            return timeWorked;
        }

        public virtual TimeSpan AverageDailyTimeWorked(DateTime beginning, DateTime end)
        {
            var totalTimeWorked = this.TimeWorked(beginning, end);
            var numberOfDays = (end - beginning).Days;

            return new TimeSpan(totalTimeWorked.Ticks / numberOfDays);
        }

        public virtual TimeSpan MinDailyTimeWorked(DateTime beginning, DateTime end)
        {
            if (beginning > end)
            {
                return TimeSpan.Zero;
            }

            var minTimeWorked = TimeSpan.MaxValue;
            var currentDay = beginning;
            while (currentDay <= end)
            {
                var timeWorked = this.TimeWorked(
                    currentDay, 
                    currentDay.AddDays(1));
                if (timeWorked > TimeSpan.Zero && timeWorked < minTimeWorked)
                {
                    minTimeWorked = timeWorked;
                }

                currentDay = currentDay.AddDays(1);
            }

            if (minTimeWorked == TimeSpan.MaxValue)
            {
                return TimeSpan.Zero;
            }

            return minTimeWorked;
        }

        public virtual TimeSpan MaxDailyTimeWorked(DateTime beginning, DateTime end)
        {
            if (beginning > end)
            {
                return TimeSpan.Zero;
            }

            var maxTimeWorked = TimeSpan.Zero;
            var currentDay = beginning;
            while (currentDay <= end)
            {
                var timeWorked = this.TimeWorked(
                    currentDay,
                    currentDay.AddDays(1));
                if (timeWorked > maxTimeWorked)
                {
                    maxTimeWorked = timeWorked;
                }

                currentDay = currentDay.AddDays(1);
            }

            return maxTimeWorked;
        }

        private IList<DateTime> allTimes()
        {
            var w = this.web;
            return w.Run<TimestampReader, IEnumerable<DateTime>>(
                reader => reader.Read()).ToList();
        }

        private readonly MethodWeb web;
    }
}
