namespace xofz.TimeKeeper.Framework
{
    using System;
    using System.Collections.Generic;

    public interface TimestampReader
    {
        IEnumerable<DateTime> Read();
    }
}
