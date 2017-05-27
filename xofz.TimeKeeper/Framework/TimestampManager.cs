namespace xofz.TimeKeeper.Framework
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading;
    using xofz.Framework;
    using xofz.Framework.Transformation;

    public sealed class TimestampManager : TimestampReader, TimestampWriter
    {
        public TimestampManager(MethodWeb web)
        {
            this.web = web;
            this.mainDirectory = "Data";
        }

        public IEnumerable<DateTime> Read()
        {
            var w = this.web;

            if (Interlocked.CompareExchange(ref this.firstReadIf0, 1, 0) == 0)
            {
                return w.Run<EnumerableTrapper<DateTime>, IEnumerable<DateTime>>(
                    trapper => trapper.Trap(this.readInternal()));
            }

            if (Interlocked.CompareExchange(ref this.needToTrapIf1, 0, 1) == 1)
            {
                return w.Run<EnumerableTrapper<DateTime>, IEnumerable<DateTime>>(
                    trapper => trapper.Trap(this.readInternal()));
            }

            return w.Run<EnumerableTrapper<DateTime>,
                MaterializedEnumerable<DateTime>>(
                trapper => trapper.TrappedCollection);
        }

        private IEnumerable<DateTime> readInternal()
        {
            var ll = new LinkedList<DateTime>();
            var md = this.mainDirectory;
            if (!Directory.Exists(md))
            {
                Directory.CreateDirectory(md);
            }

            foreach (var filePath in Directory.GetFiles(md))
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
            var md = this.mainDirectory;
            if (!Directory.Exists(md))
            {
                Directory.CreateDirectory(md);
            }

            var w = this.web;
            var now = DateTime.Now;
            var startOfWeek = w.Run<DateCalculator, DateTime>(
                calc => calc.StartOfWeek());
            var fileName = startOfWeek.Year
                           + startOfWeek.Month.ToString().PadLeft(2, '0')
                           + startOfWeek.Day.ToString().PadLeft(2, '0');
            var times = new List<string>();
            var filePath = md + @"\" + fileName;
            if (File.Exists(filePath))
            {
                times.AddRange(File.ReadAllLines(filePath));
            }

            times.Add(now.Ticks.ToString());
            File.WriteAllLines(filePath, times);
            Interlocked.CompareExchange(ref this.needToTrapIf1, 1, 0);
        }

        private int firstReadIf0;
        private int needToTrapIf1;
        private readonly MethodWeb web;
        private readonly string mainDirectory;
    }
}
