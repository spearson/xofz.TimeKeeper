namespace xofz.TimeKeeper.Framework
{
    using System;

    public class TimeSpanViewer
    {
        public virtual string ReadableString(TimeSpan timeSpan)
        {
            return timeSpan.Days + "d "
                   + timeSpan.Hours + "h "
                   + timeSpan.Minutes + "m "
                   + timeSpan.Seconds + "s";
        }
    }
}
