namespace xofz.TimeKeeper.Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public sealed class TimestampManager : TimestampReader, TimestampWriter, DateCalculator
    {
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
        }

        IEnumerable<DateTime> TimestampReader.Read()
        {
            foreach (var filePath in Directory.GetFiles("Data"))
            {
                foreach (var tickCount in File.ReadAllLines(filePath))
                {
                    yield return new DateTime(long.Parse(tickCount));
                }
            }
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
                default:
                    daysToSubtract = 0;
                    break;
            }

            return now.Date.AddDays(-daysToSubtract);
        }
    }
}
