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

            return this.TimeWorked(beginning);
        }

        public virtual TimeSpan TimeWorked(DateTime beginning)
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

                timeWorked += allTimes[i + 1] - allTimes[i];
            }

            if (allTimes.Count % 2 == 1)
            {
                timeWorked += now - allTimes[allTimes.Count - 1];
            }

            return timeWorked;
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
