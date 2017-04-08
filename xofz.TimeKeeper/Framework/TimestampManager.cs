namespace xofz.TimeKeeper.Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using xofz.Framework.Transformation;

    public sealed class TimestampManager : TimestampReader, TimestampWriter, DateCalculator
    {
        public TimestampManager(EnumerableTrapper<DateTime> trapper)
        {
            this.trapper = trapper;
        }

        public IEnumerable<DateTime> Read()
        {
            if (Interlocked.CompareExchange(ref this.firstReadIf0, 1, 0) == 0)
            {
                return this.trapper.Trap(this.readInternal());
            }

            if (Interlocked.CompareExchange(ref this.needToTrapIf1, 0, 1) == 1)
            {
                return this.trapper.Trap(this.readInternal());
            }

            return this.trapper.TrappedCollection;
        }

        private IEnumerable<DateTime> readInternal()
        {
            var ll = new LinkedList<DateTime>();
            foreach (var filePath in Directory.GetFiles("Data"))
            {
                foreach (var tickCount in File.ReadAllLines(filePath))
                {
                    ll.AddLast(new DateTime(long.Parse(tickCount)));
                }
            }

            return ll;
        }

        void TimestampWriter.Write()
        {
            var mainDirectory = "Data";
            if (!Directory.Exists(mainDirectory))
            {
                Directory.CreateDirectory(mainDirectory);
            }

            var now = DateTime.Now;
            var startOfWeek = this.StartOfWeek();
            var fileName = startOfWeek.Year
                           + startOfWeek.Month.ToString().PadLeft(2, '0')
                           + startOfWeek.Day.ToString().PadLeft(2, '0');
            var times = new List<string>();
            var filePath = mainDirectory + @"\" + fileName;
            if (File.Exists(filePath))
            {
                times.AddRange(File.ReadAllLines(filePath));
            }

            times.Add(now.Ticks.ToString());
            File.WriteAllLines(filePath, times);
            Interlocked.CompareExchange(ref this.needToTrapIf1, 1, 0);
        }

        public DateTime StartOfWeek()
        {
            var now = DateTime.Now;
            int daysToSubtract;
            switch (now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    daysToSubtract = 0;
                    break;
                case DayOfWeek.Tuesday:
                    daysToSubtract = 1;
                    break;
                case DayOfWeek.Wednesday:
                    daysToSubtract = 2;
                    break;
                case DayOfWeek.Thursday:
                    daysToSubtract = 3;
                    break;
                case DayOfWeek.Friday:
                    daysToSubtract = 4;
                    break;
                case DayOfWeek.Saturday:
                    daysToSubtract = 5;
                    break;
                case DayOfWeek.Sunday:
                    daysToSubtract = 6;
                    break;
                default: // switch statements shouldn't need a default case if 
                         //all the enum's values have been covered
                    daysToSubtract = 0;
                    break;
            }

            return now.Date.AddDays(-daysToSubtract);
        }

        private int firstReadIf0;
        private int needToTrapIf1;
        private readonly EnumerableTrapper<DateTime> trapper;
    }
}
